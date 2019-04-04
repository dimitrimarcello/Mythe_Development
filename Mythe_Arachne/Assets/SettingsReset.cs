using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsReset : MonoBehaviour {

    [SerializeField]
    private List<Slider> volumeSliders;

    [SerializeField]
    private Slider brightnessSlider;

    [SerializeField]
    private SettingsMenu settingsPanel;

    void Start()
    {
        settingsPanel.RestoreBrightnessSettings();
        settingsPanel.RestoreVolumeSettings();

        ResetVolumeSliders();
        ResetBrightnessSlider();
    }

    // Resets volume sliders
    public void ResetVolumeSliders()
    {
        foreach (Slider slider in volumeSliders)
        {
            string[] nameParts = slider.name.Split(' ');

            string sliderName = nameParts[0];

            sliderName = sliderName.ToLower();

            settingsPanel.DoVolumeReflection(field =>
            {
                if (field.Name.Equals(sliderName))
                {
                    float value = (float)field.GetValue(settingsPanel.GetVolumes());

                    slider.value = value;
                }
            });
        }
    }

    // Resets BrightnessSlider
    public void ResetBrightnessSlider()
    {
        brightnessSlider.value = settingsPanel.GetBrightness();
    }


}
