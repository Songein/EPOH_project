using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSeedSound : MonoBehaviour
{
    public void PlaySeedSound()
    {
        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.FMN_Seed);
    }
}
