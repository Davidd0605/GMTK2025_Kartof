using UnityEngine;
using UnityEngine.UI;

public class SliderAudioControll : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private bool controlMusicVolume = true;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("Slider component missing!");
            return;
        }

        if (AudioManager.instance == null)
        {
            Debug.LogError("AudioManager instance not found!");
            return;
        }

        // Set slider to current volume value at start
        slider.value = controlMusicVolume ? AudioManager.instance.currentMusicVolume : AudioManager.instance.currentSFXVolume;

        // Add listener to update volume when slider changes
        slider.onValueChanged.AddListener(controlMusicVolume ? SetVolumeMusic : SetVolumeSFX);
    }

    public void SetVolumeMusic(float value)
    {
        AudioManager.instance.SetVolumeMusic(value);
    }

    public void SetVolumeSFX(float value)
    {
        AudioManager.instance.SetVolumeSFX(value);
    }
}
