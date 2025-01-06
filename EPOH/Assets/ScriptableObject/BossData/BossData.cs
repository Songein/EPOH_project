using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss Data", menuName = "Scriptable Object/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("보스 정보")]
    public string name;
    [SerializeField] private float _maxHealth;

    [Space(5)] [Header("보스맵 정보")] 
    public Vector3 _rightTop;
    public Vector3 _leftBottom;
}
