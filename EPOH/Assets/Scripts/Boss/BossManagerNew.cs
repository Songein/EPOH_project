using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class BossManagerNew : MonoBehaviour
{
    [System.Serializable]
    public class Phase
    {
        public int skillIndex;
        public int skillCnt;
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
    public List<Phase> phase4List = new List<Phase>(); // 호아 스테이지만 사용
    public List<Phase> phase5List = new List<Phase>(); // 호아 스테이지만 사용 
    public List<Phase> phase6List = new List<Phase>(); // 호아 스테이지만 사용
    
    [SerializeField] private bool _isRaidRunning = false;
    private Coroutine _raidCoroutine;
    private Coroutine _skillCoroutine;
    [SerializeField] private bool _isSkillEnd = false;
    [SerializeField] private bool _isPhaseEnd = false;
    public Action OnSkillEnd;
    public bool isGeneralRaid;
    
    // 플레이어 관련
    public PlayerController player;
    private void Awake()
    {
        Current = this; // 현재 씬의 BossManager를 저장
        OnSkillEnd += EndSkill;
    }

    private void Start()
    {
        SoundManager2.instance.PlayAudio();
        player = FindObjectOfType<PlayerController>();
        
        // 뉴런, hp 등 레이드를 위한 기본 세팅 진행

        if (isGeneralRaid)
        {
            // 일반 보스 레이드면 일반 보스 레이드 시작
            StartGeneralBossRaid();
        }
        else
        {
            // 최종 보스 레이드면 최종 보스 레이드 시작
            StartFinalBossRaid();
        }
    }

    // 일반적인 보스 레이드(호아 제외)
    public void StartGeneralBossRaid()
    {
        _isRaidRunning = true;
        _raidCoroutine = StartCoroutine(GeneralRaidFlow());
        Debug.LogWarning($"{bossData.name} 레이드 시작");
    }

    IEnumerator GeneralRaidFlow()
    {
        // 일반적인 보스 레이드는 페이즈 1 > 2 > 3 순으로 진행.
        for (int i = 1; i <= 3; i++)
        {
            Debug.LogWarning($"초기 페이즈{i} 시작");
            yield return StartCoroutine(RunPhase(i));
            yield return new WaitUntil(() => _isPhaseEnd);
            yield return null;
        }
        // 이후 페이즈 1 or 2 or 3 중 랜덤 실행
        while (_isRaidRunning)
        {
            int phaseNum = Random.Range(1, 4);
            Debug.LogWarning($"랜덤 페이즈{phaseNum} 시작");
            yield return StartCoroutine(RunPhase(phaseNum));
            yield return new WaitUntil(() => _isPhaseEnd);
            yield return null;
        }
    }
    
    // 최종 보스 레이드(호아)
    public void StartFinalBossRaid()
    {
        Debug.LogWarning($"{bossData.name} 레이드 시작");
    }

    IEnumerator RunPhase(int num)
    {
        switch (num)
        {
            case 1:
                yield return StartCoroutine(ActivateSkill(phase1List));
                break;
            case 2:
                yield return StartCoroutine(ActivateSkill(phase2List));
                break;
            case 3:
                yield return StartCoroutine(ActivateSkill(phase3List));
                break;
            case 4:
                yield return StartCoroutine(ActivateSkill(phase4List));
                break;
            case 5:
                yield return StartCoroutine(ActivateSkill(phase5List));
                break;
            case 6:
                yield return StartCoroutine(ActivateSkill(phase6List));
                break;
        }
    }
    
    void EndBossRaid()
    {
        _isRaidRunning = false;

        if (_skillCoroutine != null)
        {
            StopCoroutine(_skillCoroutine);
        }
        if (_raidCoroutine != null)
        {
            StopCoroutine(_raidCoroutine);
        }
        Debug.LogWarning($"{bossData.name} 레이드 종료");
    }
    
    // 보스 레이드 클리어
    public void ClearBossRaid()
    {
        EndBossRaid();
        GameManager.instance.bossClearInfo[bossData.bossIndex] = true; //GameManager에 전달
        SaveManager.instance.SaveGameState();  //SaveManager가 GameManager의 값을 받음
        EventManager.Instance.ExecuteEvent(bossData.clearEventId).Forget();
        
        // 메인 룸으로 이동
    }
    // 보스 레이드 실패
    public void FailBossRaid()
    {
        EndBossRaid();
        GameManager.instance.bossClearInfo[bossData.bossIndex] = false;
        EventManager.Instance.ExecuteEvent(bossData.failEventId).Forget();
        
        // 어디로 이동??
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
        _isPhaseEnd = false;
        foreach (var skill in phase)
        {
            MonoBehaviour skillScript = skillList[skill.skillIndex];
            BossSkillInterface skillInterface = skillScript as BossSkillInterface;

            if (skillInterface != null)
            {
                Debug.LogWarning($"Skill{skill.skillIndex} 스킬 시작");
                for (int i = 0; i < skill.skillCnt; i++)
                {
                    skillInterface.Activate();
                }
                yield return new WaitForSeconds(skill.skillTerm);
                Debug.LogWarning($"Skill{skill.skillIndex} 스킬 끝");
            }
            else
            {
                EPOH.Debug.LogWarning($"skill interface가 널입니다.");
                yield return null;
            }
        }
        _isPhaseEnd = true;
    }

    private void EndSkill()
    {
        _isSkillEnd = true;
    }
    
    // 플레이어가 보스 오른쪽에 위치하는지 체크할 수 있는 함수
    public bool IsPlayerRight(Transform boss)
    {
        
        if (player.transform.position.x - boss.position.x >= 0)
        {
            //Debug.Log("right");
            boss.GetComponent<SpriteRenderer>().flipX = true;
            return true;
        }
        
        //Debug.Log("left");
        boss.GetComponent<SpriteRenderer>().flipX = false;
        return false;
    }

    public void SetSkillCoroutine(Coroutine skill)
    {
        _skillCoroutine = skill;
    }
}
