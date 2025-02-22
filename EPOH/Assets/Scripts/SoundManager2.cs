using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager2 : MonoBehaviour
{
    public static SoundManager2 instance;


    [SerializeField] public AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] BGM_List;
    [SerializeField] private AudioClip[] SFX_List;

    private AudioSource[] audioSources;
    private float footstepDelay = 0.6f; // 발자국 소리 간격
    private float lastFootstepTime;



    public enum BGMsound { 
        MainRoom,
        Dog,
        PartTime,
        ForgetMeNot,
        Criminal,
        Hoa,
        Hoa2


    }
    public enum SfXSound { 
    Footstep,
    Teleport,
    Attack
    }
 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
         
        }
    }

    void Start()
    {
        // 모든 AudioSource 가져오기
        audioSources = GetComponents<AudioSource>();

        // 확인용 디버그 로그
        Debug.Log("AudioSource 개수: " + audioSources.Length);
    }



    public void SetBGMVolume(float volume)
    {
       
        bgmSource.volume = volume;
        Debug.Log("BGM Volume: " + bgmSource.volume);
    }

    public void SetSFXVolume(float volume)
    {

        audioSources[1].volume = volume;
        Debug.Log("BGM Volume: " + bgmSource.volume);
    }

 
    public void PlayBGM(int index)
    {
       
        if (bgmSource.clip == BGM_List[index]) return;

        bgmSource.clip = BGM_List[index];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(int index)
    {
        sfxSource.PlayOneShot(SFX_List[index]);
    }

    public void PlayFootstep()
    {
        if (Time.time - lastFootstepTime >= footstepDelay)
        {
            float currentVolume = audioSources[1].volume; // 현재 슬라이더에 설정된 볼륨
            sfxSource.PlayOneShot(SFX_List[(int)SfXSound.Footstep], currentVolume);
            lastFootstepTime = Time.time;
        }
    }

    public void PlayTeleport()
    {
        if (Time.time - lastFootstepTime >= 0.15)
        {
            float currentVolume = audioSources[1].volume; // 현재 슬라이더에 설정된 볼륨
            sfxSource.PlayOneShot(SFX_List[(int)SfXSound.Teleport], currentVolume);
            lastFootstepTime = Time.time;
        }
    }

    public void PlayAttack()
    {
        if (Time.time - lastFootstepTime >= 0.05)
        {
            float currentVolume = audioSources[1].volume; // 현재 슬라이더에 설정된 볼륨
            sfxSource.PlayOneShot(SFX_List[(int)SfXSound.Attack], currentVolume);
            lastFootstepTime = Time.time;
        }
    }
}
