using UnityEngine;

public class MiniBullet : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float fadeStartTime = 1.5f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Random Size")]
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float maxSize = 1.5f;

    [Header("Random Color")]
    [SerializeField] private Color[] possibleColors;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float currentLifetime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        RandomizeAppearance();
    }

    public void Launch(Vector2 direction, float speed)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    private void RandomizeAppearance()
    {
        // Random size
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * randomSize;

        // Random color
        if (possibleColors.Length > 0)
        {
            int randomIndex = Random.Range(0, possibleColors.Length);
            spriteRenderer.color = possibleColors[randomIndex];
        }
    }

    private void Update()
    {
        currentLifetime += Time.deltaTime;

        if (currentLifetime >= fadeStartTime)
        {
            Fade();
        }

        if (currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void Fade()
    {
        float fadeTime = currentLifetime - fadeStartTime;

        float alpha = 1f - fadeTime / fadeDuration;

        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp01(alpha);

        spriteRenderer.color = color;
    }
}