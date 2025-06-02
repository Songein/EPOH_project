using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundMenu : MonoBehaviour
{
    public static SoundMenu instance;
    public GameObject bgmPanel;
    public GameObject sizePanel;
    public GameObject wholePanel;



    private void Awake()
    {
        // 싱글톤 패턴 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환해도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void FindPanelAndActivate() {
        Debug.Log("Activate");
        GameObject canvas = GameObject.FindWithTag("PanelBGM");
        Transform child = canvas.transform.GetChild(0); //panel 찾기
        child.gameObject.SetActive(true); //panel 켜기
        Time.timeScale = 0;
        SoundManager2.instance.bgmSource.Pause(); // BGM 정지
        Debug.Log("자식 오브젝트가 활성화되었습니다.");
    }

    public void DeActivate()
    {
        Debug.Log("DeActivate");
        bgmPanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager2.instance.bgmSource.Play(); // BGM 재생
    }

    public void screenMove() {
        bgmPanel.SetActive(false);
        sizePanel.SetActive(true);

    }

    public void bgmMove()
    {
        bgmPanel.SetActive(true);
        sizePanel.SetActive(false);

    }

    public void DeActivateWhole() {
        wholePanel.SetActive(false);
        Time.timeScale = 1;
        if (!SoundManager2.instance.bgmSource.isPlaying)
        {
            SoundManager2.instance.bgmSource.Play(); // BGM이 완전히 멈춘 경우 재생
        }
    }

    public void ActivateWhole()
    {
        wholePanel.SetActive(true);
        Time.timeScale = 0;
        SoundManager2.instance.bgmSource.Pause(); // BGM 정지
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Debug.Log("Escape");
            GameObject canvas = GameObject.FindWithTag("PanelBGM");
            Transform child = canvas.transform.GetChild(0); //Wholepanel 찾기
            bool isActive = child.gameObject.activeSelf;
            child.gameObject.SetActive(!isActive);

            if (!isActive)
            {
                Time.timeScale = 0;
                SoundManager2.instance.bgmSource.Pause(); // BGM 정지
            }
            else
            {
                Time.timeScale = 1;
                if (!SoundManager2.instance.bgmSource.isPlaying)
                {
                    SoundManager2.instance.bgmSource.Play(); // BGM이 완전히 멈춘 경우 재생
                }
            }
        }
    }



}
