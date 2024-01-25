using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInformation : MonoBehaviour
{
    public int scene_bgm;
    private SoundManager sound_manager;

    private void Start()
    {
        sound_manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        sound_manager.PlayBGM(scene_bgm);
    }

}
