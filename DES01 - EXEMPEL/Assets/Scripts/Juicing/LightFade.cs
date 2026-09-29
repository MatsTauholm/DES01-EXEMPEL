using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; //Namespace for light settings

public class LightFade : MonoBehaviour
{
    private Light2D light2D;
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float fadeSpeed = 0.1f;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Update()
    {
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * fadeSpeed, 0)); // Use Perlin noise for smooth fading
    }
}