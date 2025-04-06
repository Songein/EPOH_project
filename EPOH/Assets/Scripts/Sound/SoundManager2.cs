using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager2 : MonoBehaviour
{
    public static SoundManager2 instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    [SerializeField] private AudioClip[] BGM_List;
    [SerializeField] private SfxData[] sfxData;
    //[SerializeField] private AudioClip[] SFX_List;

    public AudioSource[] audioSources;
    private float footstepDelay = 0.6f; // 발자국 소리 간격
    private float lastTime;


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
    Player_Teleport,
    Player_Rebirth,
    Player_Death,
    Player_Jump1,
    Player_Jump2,
    Player_Footstep,
    Player_Attack,
    Dog_Running,
    Dog_Howling,
    Dog_Stamp,
    Dog_Scratch,
    Dog_Trace,
    Dog_Bite,
    PT_Swaping,
    PT_Sauce,
    PT_Throwing,
    PT_Receipt,
    PT_Pepper,
    PT_Bubble,
    PT_Knife,
    FMN_Seed,
    FMN_Ball,
    FMN_Cookie,
    FMN_Doll,
    FMN_Lullaby,
    FMN_Lullaby2,
    FMN_Picture,
    Criminal_Keyboard,
    Criminal_MoneyRain,
    Criminal_Runaway_Laugh,
    Criminal_Runaway,
    Criminal_Catch,
    Criminal_Catch2,
    Hoa_Arm,
    Hoa_Light,
    Hoa_Electric,
    Hoa_Pop,
    Hoa_Rain,
    Hoa_Snipe
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

    public void PlayAudio() {
        BossData bossData = BossManagerNew.Current.bossData;
        if (bossData != null)
        {
            bgmSource.clip = bossData.BGMClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }

    }


    public void SetBGMVolume(float volume)
    {
       
        bgmSource.volume = volume;
        Debug.Log("BGM Volume: " + bgmSource.volume);
    }

    public void SetSFXVolume(float volume)
    {

        audioSources[1].volume = volume;
        Debug.Log("SFX Volume: " + bgmSource.volume);
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
    { //PlayOneShot은 loop 재생이 안돼서...
        if (index == (int)SfXSound.Criminal_Catch)
        {
            float currentVolume = audioSources[1].volume * sfxData[index].volume; // 현재 슬라이더에 설정된 볼륨
            sfxSource.clip = sfxData[index].sfxClip; // 클립 설정
            sfxSource.volume = currentVolume;         // 볼륨 설정
            sfxSource.loop = sfxData[index].loop;     // 루프 설정
            sfxSource.Play();                         // 정상 재생
        }
        else if (Time.time - lastTime >= sfxData[index].delay)
        {
            float currentVolume = audioSources[1].volume * sfxData[index].volume; // 현재 슬라이더에 설정된 볼륨
            sfxSource.PlayOneShot(sfxData[index].sfxClip, currentVolume);
            sfxSource.loop = sfxData[index].loop;
            lastTime = Time.time;
        }
    }

    public void PlayAttack()
    {
        if (Time.time - lastTime >= 0.05)
        {
            float currentVolume = audioSources[1].volume * sfxData[2].volume; // 현재 슬라이더에 설정된 볼륨
            sfxSource.PlayOneShot(sfxData[2].sfxClip, currentVolume);
            sfxSource.loop = sfxData[2].loop;
            lastTime = Time.time;
        }
    }
}
