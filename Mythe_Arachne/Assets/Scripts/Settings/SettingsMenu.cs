using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Reflection;

public class SettingsMenu : MonoBehaviour {

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject swingHolder;

    [SerializeField]
    private GameObject swingObject;

    [SerializeField]
    private AudioMixer audioMixer;

    private Volumes volumes;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private SettingsReset settingsReset;

    private bool changed;

    //TODO Make slider mute when he is under certain value, so sound is better scaled.



    /*

    [SerializeField]
    private Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    */

    void OnEnable()
    {

    }
    void Awake()
    {
        volumes = new Volumes();
    }

    void Start()
    {
        gameObject.SetActive(false);
        saveButton.interactable = false;

        /*
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIdex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIdex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIdex;
        resolutionDropdown.RefreshShownValue();

    */
        
    }

    public Volumes GetVolumes()
    {
        return volumes;
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

    void SetVolume(string name, float volume)
    {
        SetChanged(true);

        if (volume <= -35)
        {
            volume = -80;
        }

        audioMixer.SetFloat(name, volume);

        DoVolumeReflection(field =>
        {
            float b = (float)field.GetValue(volumes);

            if (field.Name.Equals(name))
            {
                field.SetValue(volumes, volume);
            }
        });

    }

    void SetChanged(bool _changed)
    {
        changed = _changed;

        saveButton.interactable = changed;
    }

    public void OnSave()
    {
        if (volumes == null) return;
        if (!changed) return;

        SetChanged(false);

        DoVolumeReflection(field =>
        {
            float value = (float)field.GetValue(volumes);

            if (value >= -80)
            {
                PlayerPrefs.SetFloat("settings_" + field.Name, value);
                audioMixer.SetFloat(field.Name, value);

                Debug.Log("Saved");
            }
        });



        PlayerPrefs.Save();


    }

    public void ExitSettings()
    {
        if (changed)
        {

            // Show "Are u sure u want to exit without saving?"

            ResetVolumes();
            //return;
        }


        swingObject.transform.SetParent(swingHolder.transform);

        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ResetVolumes()
    {
        DoVolumeReflection(field =>
        {
            string prefName = "settings_" + field.Name;
            if (PlayerPrefs.HasKey(prefName))
            {
                float value = PlayerPrefs.GetFloat(prefName);
                audioMixer.SetFloat(field.Name, value);

                field.SetValue(volumes, value);


            }
        });

        settingsReset.ResetSliders();
    }

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

    public void RestoreSettings()
    {

        DoVolumeReflection(field =>
        {
            string name = "settings_" + field.Name;
            if (PlayerPrefs.HasKey(name))
            {
                float value = PlayerPrefs.GetFloat(name);
                field.SetValue(volumes, value);
                audioMixer.SetFloat(field.Name, value);
            }
        });


    }

}