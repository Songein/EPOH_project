using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour
{
    public GameObject resolutionPanel;
    public Dropdown resolutionDropdown;
    public Dropdown screenModeDropdown;

    void Start()
    {
        PopulateSizeDropdown();
        PopulateModeDropdown();


    }

    public void PopulateSizeDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string> { "1920x1080", "1280x720", "720x480" };
        resolutionDropdown.AddOptions(options);
    }

    public void PopulateModeDropdown() {

        // 화면 모드 옵션 설정
        screenModeDropdown.ClearOptions();
        screenModeDropdown.AddOptions(new List<string> { "전체화면", "창모드" });
        screenModeDropdown.value = 0;
        screenModeDropdown.RefreshShownValue();

        // 초기 해상도 및 화면 모드 설정
        SetResolution(1920, 1080);
        SetScreenMode(0);
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

    //창모드, 전체화면 모드
    public void SetScreenMode(int modeIndex)
    {
        bool isFullscreen = (modeIndex == 0);
        Screen.fullScreen = isFullscreen;
        Debug.Log("스크린모드는: " + modeIndex);
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

