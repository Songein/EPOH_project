using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHoaManager : MonoBehaviour
{
    //보스 HOA 프리팹
    public GameObject bossPrefab;
    //보스 HOA 리스트
    public List<GameObject> bossList = new List<GameObject>();
    //플레이어
    public GameObject _player;
    //플레이어 Rigidbody2D
    public Rigidbody2D _playerRigid;

    //보스 HOA 스폰 포인트ss
    [SerializeField] public Vector3 spawnLeftPoint; //스폰 포인트(왼쪽)
    [SerializeField] public Vector3 spawnMiddlePoint; //스폰 포인트(중앙)
    [SerializeField] public Vector3 spawnRightPoint; //스폰 포인트(오른쪽)

    //보스 HOA 하위 스킬
    private Arm _arm;
    private LightSkill _light;
    private ElectricBall _eletricBall;
    private Half _half;
    private Rain _rain;
    private Snipe _snipe;
    private Cross _cross;


    //보스 페이즈
    [SerializeField] private List<string> phase1List = new List<string>();
    [SerializeField] private List<string> phase2List = new List<string>();
    [SerializeField] private List<string> phase3List = new List<string>();
    private Stack<string> phase1Stack = new Stack<string>();
    private Stack<string> phase2Stack = new Stack<string>();
    private Stack<string> phase3Stack = new Stack<string>();

    [SerializeField] private float skillDuration;


    void Awake()
    {
        //스킬 스크립트 할당
        _arm = GetComponent<Arm>();
        _light = GetComponent<LightSkill>();
        _eletricBall = GetComponent<ElectricBall>();
        _half = GetComponent<Half>();
        _rain = GetComponent<Rain>();
        _snipe = GetComponent<Snipe>();
        _cross = GetComponent<Cross>();

    }

    void Start()
    {
        //플레이어 할당
        _player = GameObject.FindWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();

        ListToStack(phase1List, phase1Stack);
        ListToStack(phase2List, phase2Stack);
        ListToStack(phase3List, phase3Stack);
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
            case "arm":
                _arm.Activate();
                yield break;
            case "light":
                _light.Activate();
                yield break;
                /* case "electricBall":
                     yield return StartCoroutine(_stomping1.StartStomping1());
                     break;
                 case "half":
                     yield return StartCoroutine(_stomping2.StartStomping2());
                     break;
                 case "rain":
                     yield return StartCoroutine(_biting.Biting());
                     break;
                 case "snipe":
                     yield return StartCoroutine(_running.Running());
                     break;
                 case "cross":
                     yield return StartCoroutine(_scratching.Scratching());
                     break;
             }*/
        }

    }



}
