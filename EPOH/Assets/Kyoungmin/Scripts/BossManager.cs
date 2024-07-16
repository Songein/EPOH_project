using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public float boss_hp = 1000f;
    public float player_hp = 200f;
    public float hacking_point = 0f;

    public bool phase1_start = false; //페이즈1 시작
    public bool phase2_start = false; //페이즈2 시작

    public bool battle_start; // 배틀 시작
}
