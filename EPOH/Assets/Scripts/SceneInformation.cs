using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInformation : MonoBehaviour
{
    public int scene_bgm;

    private void Start()
    {
        SoundManager.instance.PlayBGM(scene_bgm);
    }

}
