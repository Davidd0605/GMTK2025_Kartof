using UnityEngine.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Light2D))]
public class Light2DPulse : MonoBehaviour
{
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.5f;

    [SerializeField] private float pulseDuration = 2f;
    [SerializeField] private float pulseOffset = 0f;

    private Light2D light2D;

    void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time / (pulseDuration / 2f + pulseOffset) , 1f);
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}