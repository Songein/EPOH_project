using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour
{
    //보스 개 프리팹
    [SerializeField] public GameObject bossPrefab;
    //보스 개 리스트
    public List<GameObject> bossList = new List<GameObject>();
    //플레이어
    public GameObject _player;
    //플레이어 Rigidbody2D
    public Rigidbody2D _playerRigid;
    
    //보스 개 스폰 포인트
    [SerializeField] public Vector3 spawnLeftPoint; //스폰 포인트(왼쪽)
    [SerializeField] public Vector3 spawnMiddlePoint; //스폰 포인트(중앙)
    [SerializeField] public Vector3 spawnRightPoint; //스폰 포인트(오른쪽)
    
    //보스 개 하위 스킬
    private Howling1 _howling1;
    private Howling2 _howling2;
    private Stomping1 _stomping1;
    private Stomping2 _stomping2;
    private BossBiting _biting;
    private BossRunning _running;
    private BossScratching _scratching;
    private BossTracking _tracking;
    
    //보스 페이즈
    [SerializeField] private List<string> phase1List = new List<string>();
    [SerializeField] private List<string> phase2List = new List<string>();
    [SerializeField] private List<string> phase3List = new List<string>();
    private Stack<string> phase1Stack = new Stack<string>();
    private Stack<string> phase2Stack = new Stack<string>();
    private Stack<string> phase3Stack = new Stack<string>();
    
    
    void Awake()
    {
        //플레이어 할당
        _player = GameObject.FindWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
        
        //스킬 스크립트 할당
        _howling1 = GetComponent<Howling1>();
        _howling2 = GetComponent<Howling2>();
        _stomping1 = GetComponent<Stomping1>();
        _stomping2 = GetComponent<Stomping2>();
        _biting = GetComponent<BossBiting>();
        _running = GetComponent<BossRunning>();
        _scratching = GetComponent<BossScratching>();
        _tracking = GetComponent<BossTracking>();
    }

    void Start()
    {
        ListToStack(phase1List,phase1Stack);
        ListToStack(phase2List,phase2Stack);
        ListToStack(phase3List,phase3Stack);
    }
    
    public bool IsPlayerRight(GameObject boss)
    {
        
        if (_playerRigid.transform.position.x - boss.transform.position.x >= 0)
        {
            //Debug.Log("right");
            FlipX(boss, true);
            return true;
        }
        
        //Debug.Log("left");
        FlipX(boss, false);
        return false;
    }

    public void FlipX(GameObject boss, bool value)
    {
        boss.GetComponent<SpriteRenderer>().flipX = value;
    }

    public void GenerateDog(Vector2 spawnPoint)
    {
        GameObject boss = Instantiate(bossPrefab, spawnPoint, Quaternion.identity);
        IsPlayerRight(boss);
        bossList.Add(boss);
    }

    void ListToStack(List<string> list, Stack<string> stack)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            stack.Push(list[i]);
        }
        
    }

    public void StartPhase1()
    {
        StartCoroutine(Phase1());
    }
    public void StartPhase2()
    {
        StartCoroutine(Phase2());
    }
    public void StartPhase3()
    {
        StartCoroutine(Phase3());
    }
    IEnumerator Phase1()
    {
        Debug.Log("Phase1 : ");
        while (phase1Stack.Count > 0)
        {
            string skillName = phase1Stack.Pop();
            yield return StartCoroutine(ActivateSkill(skillName));
        }
    }
    IEnumerator Phase2()
    {
        Debug.Log("Phase2 : ");
        while (phase2Stack.Count > 0)
        {
            string skillName = phase2Stack.Pop();
            yield return StartCoroutine(ActivateSkill(skillName));
        }
    }
    IEnumerator Phase3()
    {
        Debug.Log("Phase3 : ");
        while (phase3Stack.Count > 0)
        {
            string skillName = phase3Stack.Pop();
            yield return StartCoroutine(ActivateSkill(skillName));
        }
    }

    IEnumerator ActivateSkill(string skillName)
    {
        Debug.Log(skillName + "Activate");
        switch (skillName)
        {
            case "howling1" :
                _howling1.Activate();
                yield break;
            case "howling2" :
                yield return StartCoroutine(_howling2.StartHowling2());
                break;
            case "stomping1" :
                yield return StartCoroutine(_stomping1.StartStomping1());
                break;
            case "stomping2" :
                yield return StartCoroutine(_stomping2.StartStomping2());
                break;
            case "biting" :
                yield return StartCoroutine(_biting.Biting());
                break;
            case "running" :
                yield return StartCoroutine(_running.Running());
                break;
            case "scratching" :
                yield return StartCoroutine(_scratching.Scratching());
                break;
            case "tracking" :
                yield return StartCoroutine(_tracking.Tracking());
                break;
        }
    }
    
}
