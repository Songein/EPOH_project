using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sfx Data", menuName = "Scriptable Object/Sfx Data")]
public class SfxData : ScriptableObject
{

    [SerializeField] private string name;
    


    public AudioClip sfxClip;
    public bool loop;
    public float volume;
    public float delay;


}
