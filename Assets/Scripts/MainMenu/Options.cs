using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Image blackImage;

    [SerializeField] private TMP_Dropdown displayModeDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
    private List<Resolution> uniqueResolutions;

    private void Awake()
    {
        if (gameObject.name == "OptionsPanel")
        {
            // gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        var allResolutions = Screen.resolutions;
        uniqueResolutions = new List<Resolution>();
        if (resolutionDropdown != null)
            resolutionDropdown.ClearOptions();
        
        var resolutionSet = new HashSet<(int, int)>();
        for (var i = allResolutions.Length - 1; i >= 0; i--)
        {
            if (resolutionSet.Add((allResolutions[i].width, allResolutions[i].height)))
            {
                uniqueResolutions.Insert(0, allResolutions[i]);
            }
        }

        var options = new List<string>();
        var currentResolutionIndex = GetCurrentResolutionIndex();
        for (var i = 0; i < uniqueResolutions.Count; i++)
        {
            var option = $"{uniqueResolutions[i].width}x{uniqueResolutions[i].height}";
            options.Add(option);
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        displayModeDropdown.ClearOptions();
        var displayOptions = new List<string> { "Полноэкранный", "В окне", "В окне без рамки" };
        displayModeDropdown.AddOptions(displayOptions);
        displayModeDropdown.value = PlayerPrefs.GetInt("DisplayModeIndex", 0);
        displayModeDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        displayModeDropdown.onValueChanged.AddListener(SetDisplayMode);
        
        ApplyGraphicsSettings();
    }

    private void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        ApplyGraphicsSettings();
    }

    private void SetDisplayMode(int modeIndex)
    {
        PlayerPrefs.SetInt("DisplayModeIndex", modeIndex);
        ApplyGraphicsSettings();
    }

    private void ApplyGraphicsSettings()
    {
        var resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", GetCurrentResolutionIndex());
        var resolution = uniqueResolutions[resolutionIndex];

        var displayModeIndex = PlayerPrefs.GetInt("DisplayModeIndex", 0);
        var screenMode = FullScreenMode.ExclusiveFullScreen;
        switch (displayModeIndex)
        {
            case 0: screenMode = FullScreenMode.ExclusiveFullScreen; break; // Полноэкранный
            case 1: screenMode = FullScreenMode.Windowed; break; // В окне
            case 2: screenMode = FullScreenMode.FullScreenWindow; break; // В окне без рамки
        }

        Screen.SetResolution(resolution.width, resolution.height, screenMode);
    }
    
    private int GetCurrentResolutionIndex()
    {
        for (var i = 0; i < uniqueResolutions.Count; i++)
        {
            if (uniqueResolutions[i].width == Screen.width && uniqueResolutions[i].height == Screen.height)
            {
                return i;
            }
        }
        return uniqueResolutions.Count > 0 ? uniqueResolutions.Count - 1 : 0;
    }


    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
