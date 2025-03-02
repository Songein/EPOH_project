using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour
{
    public GameObject resolutionPanel;
    public Dropdown resolutionDropdown;

    void Start()
    {
        PopulateDropdown();
        
    }

    public void PopulateDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string> { "1920x1080", "1280x720", "720x480" };
        resolutionDropdown.AddOptions(options);
    }

    public void OnResolutionChange(int index)
    {
        switch (index)
        {
            case 0:
                SetResolution(1920, 1080);
                break;
            case 1:
                SetResolution(1280, 720);
                break;
            case 2:
                SetResolution(720, 480);
                break;
        }
    }

    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        Debug.Log($"현재 해상도: {Screen.width} x {Screen.height}");
    }

    public void OpenResolutionPanel()
    {
        resolutionPanel.SetActive(true);
    }

    public void CloseResolutionPanel()
    {
        resolutionPanel.SetActive(false);
    }
}

