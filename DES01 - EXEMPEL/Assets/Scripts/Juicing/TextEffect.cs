
using UnityEngine;
using TMPro;
using System.Collections;

public class TextEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] private float delay = 1f;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float moveDistance = 15f;
    [SerializeField] private float letterDelay = 0.05f;
    [SerializeField] private Color targetColor = Color.red;

    private void Start()
    {
        text.gameObject.SetActive(true);

        if (audioSource != null)
            audioSource.Play();

        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        // Wait before starting the animation.
        yield return new WaitForSeconds(delay);

        text.ForceMeshUpdate();

        TMP_TextInfo textInfo = text.textInfo;

        // Store the original vertex positions.
        Vector3[][] originalVertices = new Vector3[textInfo.meshInfo.Length][];

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] =
                (Vector3[])textInfo.meshInfo[i].vertices.Clone();
        }

        Color32[][] originalColors = new Color32[textInfo.meshInfo.Length][];

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalColors[i] =
                (Color32[])textInfo.meshInfo[i].colors32.Clone();
        }

        // Give every letter a random direction.
        float[] directions = new float[textInfo.characterCount];

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            directions[i] = Random.value < 0.5f ? 1f : -1f;
        }

        float elapsed = 0f;

        // Animate letters one after another.
        while (elapsed < animationDuration +
               (textInfo.characterCount - 1) * letterDelay)
        {
            elapsed += Time.deltaTime;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo character = textInfo.characterInfo[i];

                if (!character.isVisible)
                    continue;

                int materialIndex = character.materialReferenceIndex;
                int vertexIndex = character.vertexIndex;

                float letterTime = Mathf.Clamp01(
                    (elapsed - i * letterDelay) / animationDuration
                );

                // Smooth the animation.
                float t = Mathf.SmoothStep(0f, 1f, letterTime);

                // Calculate vertical movement.
                float offsetY = directions[i] * moveDistance * t;

                // Fade the character to red.
                Color32 startColor = originalColors[materialIndex][vertexIndex];
                Color32 endColor = targetColor;

                Color32 color = Color32.Lerp(startColor, endColor, t);

                // Each character uses four vertices.
                for (int j = 0; j < 4; j++)
                {
                    int vertex = vertexIndex + j;

                    textInfo.meshInfo[materialIndex].vertices[vertex] =
                        originalVertices[materialIndex][vertex]
                        + new Vector3(0f, offsetY, 0f);

                    textInfo.meshInfo[materialIndex].colors32[vertex] =
                        color;
                }
            }

            // Update the text mesh.
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices =
                    textInfo.meshInfo[i].vertices;

                textInfo.meshInfo[i].mesh.colors32 =
                    textInfo.meshInfo[i].colors32;

                text.UpdateGeometry(
                    textInfo.meshInfo[i].mesh,
                    i
                );
            }

            yield return null;
        }
    }
}
