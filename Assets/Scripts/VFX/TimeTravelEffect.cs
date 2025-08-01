using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeTravelEffect : MonoBehaviour
{
    [Header("VFX")]

    [SerializeField] private Volume volume;
    public float effectDuration = 2f;
    [Range(-1f, 1f)] public float effectDisplacement = -0.1f;

    [SerializeField][Range(0f, 2f)] private float chromaMaxIntensity = 1.5f;
    [SerializeField][Range(-150f, 0f)] private float distortionMaxIntensity = -100f;
    [SerializeField][Range(0f, 1f)] private float vignetteMaxIntensity = 0.5f;
    [SerializeField][Range(0f, 5f)] private float bloomMaxIntensity = 2f;
    [SerializeField][Range(0f, 5f)] private float gain = 2f;

    private ChromaticAberration chroma;
    private LensDistortion distortion;
    private Vignette vignette;
    private Bloom bloom;

    private bool isPlaying = false;
    private float elapsedTime = 0f;

    void Start()
    {
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out chroma);
            volume.profile.TryGet(out distortion);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out bloom);
        }
    }

    public void TriggerTimeTravel()
    {
        if (isPlaying) return;
        elapsedTime = 0f;
        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / effectDuration);

        float curve = Mathf.Pow(Mathf.Sin(t * Mathf.PI), gain);

        if (chroma != null)
            chroma.intensity.value = curve * chromaMaxIntensity;

        if (distortion != null)
            distortion.intensity.value = curve * distortionMaxIntensity;

        if (vignette != null)
            vignette.intensity.value = curve * vignetteMaxIntensity;

        if (bloom != null)
            bloom.intensity.value = curve * bloomMaxIntensity;

        if (t >= 1f)
        {
            if (chroma != null) chroma.intensity.value = 0f;
            if (distortion != null) distortion.intensity.value = 0f;
            if (vignette != null) vignette.intensity.value = 0f;
            if (bloom != null) bloom.intensity.value = 0f;
            isPlaying = false;
        }
    }
}
