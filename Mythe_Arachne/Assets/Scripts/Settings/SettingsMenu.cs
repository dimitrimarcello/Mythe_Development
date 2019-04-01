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
    private GameObject firstObject;

    [SerializeField]
    private AudioMixer audioMixer;

    private Volumes volumes;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private EventSystem eventSystem;

    private bool changed;

    /*

    [SerializeField]
    private Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    */

    void OnEnable()
    {

    }
    void Start()
    {
        eventSystem.SetSelectedGameObject(firstObject);
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
        if (volumes == null) volumes = new Volumes();
        SetChanged(true);

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
                audioMixer.SetFloat(field.Name, value);
            }
        });


        eventSystem.SetSelectedGameObject(exitButton.gameObject);


    }
    
    public void OnExit()
    {
        if (changed)
        {

            // Show "Are u sure u want to exit without saving?"

            return;
        }

        volumes = null;

        panel.SetActive(false);
    }

    private void DoVolumeReflection(Action<FieldInfo> action)
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

}