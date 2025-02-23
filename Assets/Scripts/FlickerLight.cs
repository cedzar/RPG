using UnityEngine;
using UnityEngine.Rendering.Universal; // Pour utiliser Light2D

public class FlickerLight : MonoBehaviour
{
    private Light2D light2D;
    
    // Paramètres du Flicker
    public float minIntensity = 1.6f;
    public float maxIntensity = 2.0f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        // Faire varier l'Intensity entre min et max
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
