using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagerNew : MonoBehaviour
{
    public static BossManagerNew Current { get; private set; }
    public BossData bossData;
    public Action<float> OnDecreaseHackingPoint;
    public Action<float> OnIncreaseHackingPoint;
    public List<MonoBehaviour> skillList = new List<MonoBehaviour>();
    public List<int> phase1List = new List<int>();
    public List<int> phase2List = new List<int>();
    public List<int> phase3List = new List<int>();
    public float skillTerm = 3f;
    [SerializeField] private bool _isSkillEnd = false;
    public Action OnSkillEnd;
    
    private void Awake()
    {
        Current = this; // 현재 씬의 BossManager를 저장
        OnSkillEnd += EndSkill;
    }

    public void StartBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 시작");
    }
    
    public void EndBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 종료");     
    }

    public void StartPhase1()
    {
        StartCoroutine(ActivateSkill(phase1List));
    }

    public void StartPhase2()
    {
        StartCoroutine(ActivateSkill(phase2List));
    }

    public void StartPhase3()
    {
        StartCoroutine(ActivateSkill(phase3List));
    }

    public IEnumerator ActivateSkill(List<int> phase)
    {
        foreach (var skillNum in phase)
        {
            MonoBehaviour skill = skillList[skillNum];
            BossSkillInterface skillInterface = skill as BossSkillInterface;

            if (skillInterface != null)
            {
                skillInterface.Activate();
                _isSkillEnd = false;
                EPOH.Debug.LogWarning(skill);
                Debug.LogWarning($"Skill{skillNum} 스킬 시작");

                yield return new WaitUntil(() => _isSkillEnd);
                Debug.LogWarning($"Skill{skillNum} 스킬 끝");
            
                yield return new WaitForSeconds(skillTerm);
            }
            else
            {
                EPOH.Debug.LogWarning($"skill interface가 널입니다.");
            }
        }
    }

    private void EndSkill()
    {
        _isSkillEnd = true;
    }
}
