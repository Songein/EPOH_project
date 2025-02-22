using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoaSoundStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager2.instance.PlayBGM((int)SoundManager2.BGMsound.Hoa);
    }

    public void Hoa2Start() {
        SoundManager2.instance.PlayBGM((int)SoundManager2.BGMsound.Hoa2);
    }

  
}
