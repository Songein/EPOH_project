using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss Data", menuName = "Scriptable Object/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("보스 정보")]
    public string name;
    public int bossIndex;
    public int hackingGoal;
    public string clearEventId;
    public string failEventId;
    [SerializeField] private float _maxHealth;

    [Space(5)] [Header("보스맵 정보")] 
    public Vector3 _rightTop;
    public Vector3 _leftBottom;

    [Space(5)] [Header("보스 BGM")]
    public AudioClip BGMClip;

    [Space(5)]
    [Header("뉴런 Sprite")]
    public Sprite Nueron;
}
