using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; //싱글톤화

    public AudioClip[] BGM_List; // BGM 리스트

    private AudioSource sound_player;
    private int now_playing = 0;


    void Awake()
    {
        if(null == instance)
        {
            instance = this;
        }
        sound_player = gameObject.GetComponent<AudioSource>();
        sound_player.loop = true;
        sound_player.clip = BGM_List[now_playing];
        sound_player.Play();
        PlayBGM(now_playing); // 음악 재생
    }

    public void PlayBGM(int bgm_index)
    {
        if (now_playing == bgm_index)
        {
            Debug.Log("사운드 리턴");
            return; // 만약 현재 재생되고 있는 것과 재생할 음악이 같다면 리턴
        }
        //다르다면 BGM 재생
        else if (sound_player.isPlaying)
        {
            Debug.Log("사운드 중단");
            sound_player.Stop(); //현재 재생중인 음악을 중단
        }
        sound_player.clip = BGM_List[bgm_index]; 
        sound_player.Play(); // 다음 음악을 재생
        now_playing = bgm_index;
    }

}
