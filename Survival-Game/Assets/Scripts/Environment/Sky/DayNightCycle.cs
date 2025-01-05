using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class DayNightCycle : MonoBehaviour
{

    [Header("Time")]
    [Tooltip("Day Length in Minutes")]
    private float _targetDayLength = GameSettingsManager.gameTimeMinutes; // Length of a day in minutes
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }

    [SerializeField]
    [Range(0f, 1f)] // Ensure timeOfDay value stays between 0 (start of day) and 1 (end of day)
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }

    private int _dayNumber = 0; // Tracks the days passed
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }

    private float _timeScale = 100f; // The progression speed of in-game time.
    public bool pause = false; // Pauses the cycle for debugging

    [Header("Sun Light")]
    [SerializeField]
    private Transform dailyRotation;

    [SerializeField]
    private Light sun;

    private float intensity;
    [SerializeField]
    private float sunBaseIntensity = 1f; // Base intensity of sun

    [SerializeField]
    private float sunVariation = 1.5f; // Maximum variation in sun intensity

    [SerializeField]
    private Gradient sunColor; // Gradient controlling the sun's colour at different times

    // Map sun intensity based on its position
    [SerializeField]
    private AnimationCurve intensityCurve = AnimationCurve.Linear(0, 0.5f, 1, 1.5f);

    [Header("Time Scaling")]
    [Tooltip("Curve to control time scaling based on time of day.")]

    [SerializeField]
    private AnimationCurve timeScaleCurve = new AnimationCurve(
    new Keyframe(0f, 1f, 0f, 0f),     // Time 0, Value 1, flat start (horizontal)
    new Keyframe(0.2f, 1f, 0f, -2f),  // Time 0.2, Value 1, more sharp dip (steep downward slope)
    new Keyframe(0.5f, 0.1f, 2f, 2f), // Time 0.5, Value 0.1 (lowest dip), sharper drop and rise
    new Keyframe(0.8f, 1f, -2f, 0f),  // Time 0.8, Value 1, more gradual return to 1
    new Keyframe(1f, 1f)              // Time 1, Value 1, end horizontal
);

    private void Update()
    {
        if (!pause) // Only update if not paused.
        {
            UpdateTimeScale();
            UpdateTime();
        }

        AdjustSunRotation();
        SunIntensity();
        AdjustSunColor();
    }
    // Progress the time of day
    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400; // Seconds in a day
        if (_timeOfDay > 1) // New day
        {
            _dayNumber++;
            _timeOfDay -= 1; // Go to start of a new day
        }
    }

    // Rotates the sun daily based on time of day
    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f; // Calculate the sun's angle (360 degrees for a full day)
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        // Evaluate intensity curve and assign to sun
        sun.intensity = intensityCurve.Evaluate(intensity);
        sun.intensity = Mathf.Clamp(sun.intensity, 0.1f, 2.0f);
    }

    private void AdjustSunColor()
    {
        // Adjust the sun's color based on the intensity
        sun.color = sunColor.Evaluate(intensity);
    }

    // Adjust time scale based on the time of day
    private void UpdateTimeScale()
    {
        // PreviousCode: _timeScale = 24 / (_targetDayLength / 60);
        // Use the curve to adjust the time scale based on timeOfDay
        // timeOfDay ranges from 0 to 1
        float curveValue = timeScaleCurve.Evaluate(_timeOfDay);
        // Adjust time scale by combining curve value and target day length.
        _timeScale = (24 / (_targetDayLength / 60)) * curveValue;

    }
}
