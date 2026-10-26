using UnityEngine;
using System.Collections;

public class ChangeMaterialAndDestroy : MonoBehaviour
{
    [SerializeField] private float startDisolveTimer = 2f;
    [SerializeField] private float dissolveDuration = 2f;
    [SerializeField] private Material disolveMaterial;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = gameObject.GetComponent<Renderer>();
        objectRenderer.material = new Material(disolveMaterial);
        StartCoroutine(DissolveAndDestroy());
    }

    private IEnumerator DissolveAndDestroy()
    {
        yield return new WaitForSeconds(startDisolveTimer);
        float timer = 0f;
        while (timer < dissolveDuration)
        {
            timer += Time.deltaTime;

            // Calculate progress from 0 to 1
            float dissolveAmount = Mathf.Lerp(0f, 1f, timer / dissolveDuration);

            objectRenderer.material.SetFloat("_DissolveAmount", dissolveAmount);

            yield return null;
        }
        Destroy(gameObject);
    }
}