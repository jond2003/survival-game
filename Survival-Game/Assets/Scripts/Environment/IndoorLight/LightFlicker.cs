using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light spotlight; // Reference to light component
    public Material bulbMaterial; // Material of the bulb object, used for emission effects
    public Color emissionColor = Color.yellow; // Base color of the bulb's emission
    public float minIntensity = 0.5f; // Minimum light intensity when flickering
    public float maxIntensity = 2.0f; // Maximum light intensity
    public float minFlickerDuration = 0.1f; // Shortest duration of the flicker
    public float maxFlickerDuration = 0.5f; // The longest duration
    public float minOffDuration = 1.0f; // The shortest duration of the off phase
    public float maxOffDuration = 3.0f; // The longest duration

    private float phaseTimer; // Tracks the remaining time in the current phase (flicker or off).
    private bool isFlickering; // Is currently in flickering phase?

    void Start()
    {
        // Automatically assign the Light component if not set in the inspector
        if (spotlight == null)
            spotlight = GetComponent<Light>();

        // Start with the light in the off phase.
        EnterOffPhase();
    }

    void Update()
    {
        // Reduce timer by time elapsed since the last frame
        phaseTimer -= Time.deltaTime;

        // Phase timer has run out?
        if (phaseTimer <= 0f)
        {
            if (isFlickering)
            {
                // Switch to off phase (if flicker is done)
                EnterOffPhase();
            }
            else
            {
                // Switch to flicker phase (if off is done)
                EnterFlickerPhase();
            }
        }

        // If in flicker, update light intensity to create a flickering effect
        if (isFlickering)
        {
            Flicker();
        }
    }

    void EnterOffPhase()
    {
        isFlickering = false;
        spotlight.intensity = 0f; // Turn off light

        // Set bulb material emission colour to black (no glow).
        if (bulbMaterial != null)
        {
            bulbMaterial.SetColor("_EmissionColor", Color.black);
        }

        // Set a random duration for off phase
        phaseTimer = Random.Range(minOffDuration, maxOffDuration);
    }

    void EnterFlickerPhase()
    {
        isFlickering = true;

        // Set a random duration for flicker phase
        phaseTimer = Random.Range(minFlickerDuration, maxFlickerDuration);
    }

    void Flicker()
    {
        // Random intensity for the light
        float newIntensity = Random.Range(minIntensity, maxIntensity);
        spotlight.intensity = newIntensity;

        // Update bulb material's emission to reflect the light intensity
        if (bulbMaterial != null)
        {
            // Normalize intensity to a 0-1 range and apply it to the emission color
            float emissionIntensity = Mathf.Clamp01(newIntensity / maxIntensity);
            bulbMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        }
    }
}
