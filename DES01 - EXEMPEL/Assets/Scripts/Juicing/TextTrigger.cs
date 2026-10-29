using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    private TextEffect textEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textEffect = FindAnyObjectByType<TextEffect>();
            if (textEffect != null)
            {
                textEffect.ShowText();
            }
        }

    }
}
