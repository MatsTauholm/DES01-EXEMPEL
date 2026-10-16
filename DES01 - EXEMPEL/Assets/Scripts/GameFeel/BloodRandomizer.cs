using UnityEngine;

public class BloodRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] bloodSprites;
    [SerializeField] private Color[] bloodColors;
    [SerializeField] private float minScale = 0.5f, maxScale = 1.5f;
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = bloodSprites[Random.Range(0, bloodSprites.Length)];
    }

    void Update()
    {
        
    }
}
