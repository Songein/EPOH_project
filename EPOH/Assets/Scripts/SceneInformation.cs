using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInformation : MonoBehaviour
{
    public int scene_bgm;

    private void Start()
    {
        SoundManager2.instance.PlayBGM(scene_bgm);
    }

}
