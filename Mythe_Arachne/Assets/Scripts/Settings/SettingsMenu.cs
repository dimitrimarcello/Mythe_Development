using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Reflection;

public class SettingsMenu : MonoBehaviour {

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject swingHolder;

    [SerializeField]
    private GameObject swingObject;

    private Volumes volumes;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private SettingsReset settingsReset;

    private bool changed;

    private float brightness;


    void Awake()
    {
        volumes = new Volumes();
    }

    void Start()
    {
        gameObject.SetActive(false);
        saveButton.interactable = false;        
    }

    public Volumes GetVolumes()
    {
        return volumes;
    }

    public float GetBrightness()
    {
        return brightness;
    }

    public void SetBrightness(float _brightness)
    {
        if (!mainPanel.activeSelf)
            SetChanged(true);

        brightness = _brightness;
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume("master", volume);
    }

    public void SetGameVolume(float volume)
    {
        SetVolume("game", volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume("music", volume);
    }

    // Caches slider value's to Volume class
    void SetVolume(string name, float volume)
    {
        if (!mainPanel.activeSelf)
        SetChanged(true);

        if (volume <= -35)
        {
            volume = -80;
        }

        SetFloat(name, volume);

        DoVolumeReflection(field =>
        {
            float b = (float)field.GetValue(volumes);

            if (field.Name.Equals(name))
            {
                field.SetValue(volumes, volume);
            }
        });

    }

    // Changes 'changed' state and updates save button's interactability
    void SetChanged(bool _changed)
    {
        changed = _changed;

        saveButton.interactable = changed;
    }

    // Saves all changes
    public void OnSave()
    {
        if (volumes == null) return;
        if (!changed) return;

        SetChanged(false);
        

        // Saves all volume settings
        DoVolumeReflection(field =>
        {
            float value = (float)field.GetValue(volumes);

            if (value >= -80)
            {
                PlayerPrefs.SetFloat("settings_" + field.Name, value);
                SetFloat(field.Name, value);
            }
        });

        // Saves all brightness settings
        PlayerPrefs.SetFloat("settings_brightness", brightness);

        PlayerPrefs.Save();


    }

    // When exit button is pressed. Goes to main menu and resets all changes that wheren't saved
    public void ExitSettings()
    {
        if (changed)
        {

            //TODO Show "Are u sure u want to exit without saving?"

            if (PlayerPrefs.HasKey("settings_brightness"))
            {
                brightness = PlayerPrefs.GetFloat("settings_brightness");
            } else
            {
                brightness = 1f;
            }

            settingsReset.ResetBrightnessSlider();

            ResetVolumes();

            SetChanged(false);
            //return;
        }


        swingObject.transform.SetParent(swingHolder.transform);

        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    // Resets all volume sliders to PlayPrefs's values
    private void ResetVolumes()
    {
        DoVolumeReflection(field =>
        {
            string prefName = "settings_" + field.Name;
            if (PlayerPrefs.HasKey(prefName))
            {
                float value = PlayerPrefs.GetFloat(prefName);
                SetFloat(field.Name, value);

                field.SetValue(volumes, value);


            }
        });

        settingsReset.ResetVolumeSliders();
    }

    // Reads all fields in 'Volume' class using reflection. And sends action for each field.
    public void DoVolumeReflection(Action<FieldInfo> action)
    {
        Type type = volumes.GetType();

        while (type != null)
        {
            FieldInfo[] myObjectFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in myObjectFields)
            {

                action(field);
            }


            type = type.BaseType;    
        }
    }

    // Restores volume settings, is called on start
    public void RestoreVolumeSettings()
    {

        DoVolumeReflection(field =>
        {
            string name = "settings_" + field.Name;
            if (PlayerPrefs.HasKey(name))
            {
                float value = PlayerPrefs.GetFloat(name);
                field.SetValue(volumes, value);
            }
        });


    }

    // Restores volume settings, is called on start
    public void RestoreBrightnessSettings()
    {
        string name = "settings_brightness";

        if (!PlayerPrefs.HasKey(name)) return;

        float value = PlayerPrefs.GetFloat(name);

        brightness = value;
    }

    private GameMusic gameMusic;

    public void SetFloat(string name, float volume)
    {
        gameMusic = FindObjectOfType<GameMusic>();

        if (gameMusic == null || gameMusic.audioSource == null) return;

        if (name.ToLower().Equals("music"))
        {
            gameMusic.audioSource.volume = volume;
        }
    }

}