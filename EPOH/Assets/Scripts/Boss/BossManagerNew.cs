using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagerNew : MonoBehaviour
{
    [System.Serializable]
    public class Phase
    {
        public int skillIndex;
        public float skillTerm;
    }
    
    public static BossManagerNew Current { get; private set; }
    public BossData bossData;
    public Action<float> OnDecreaseHackingPoint;
    public Action<float> OnIncreaseHackingPoint;
    public List<MonoBehaviour> skillList = new List<MonoBehaviour>();
    public List<Phase> phase1List = new List<Phase>();
    public List<Phase> phase2List = new List<Phase>();
    public List<Phase> phase3List = new List<Phase>();
    [SerializeField] private bool _isSkillEnd = false;
    public Action OnSkillEnd;
    
    private void Awake()
    {
        Current = this; // 현재 씬의 BossManager를 저장
        OnSkillEnd += EndSkill;
    }

    private void Start()
    {
        SoundManager2.instance.PlayAudio();
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

    public IEnumerator ActivateSkill(List<Phase> phase)
    {
        foreach (var skill in phase)
        {
            MonoBehaviour skillScript = skillList[skill.skillIndex];
            BossSkillInterface skillInterface = skillScript as BossSkillInterface;

            if (skillInterface != null)
            {
                skillInterface.Activate();
                EPOH.Debug.LogWarning(skill);
                Debug.LogWarning($"Skill{skill.skillIndex} 스킬 시작");
                yield return new WaitForSeconds(skill.skillTerm);
                Debug.LogWarning($"Skill{skill.skillIndex} 스킬 끝");
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
