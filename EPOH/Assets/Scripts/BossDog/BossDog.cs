using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDog : MonoBehaviour
{

    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    private SpriteRenderer sr; //Boss의 SpriteRenderer 참조

    [SerializeField] float boss_speed; // 보스의 이동 속도
    [SerializeField] float boss_skill_cooldown; // 보스 스킬 사용 쿨타임
    [SerializeField] float boss_move_cooldown; // 보스 이동 쿨타임

    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    [SerializeField] float track_range; // 보스가 플레이어를 추적하지 않는 거리
    [SerializeField] float close_range = 6f; // 보스와 플레이어가 어느 정도 거리일 때 가깝다고 간주하는가

    public GameObject player; // 플레이어 게임 오브젝트
    private int skill; // 사용하는 스킬
    private BossDogScene scene; //BossDogScene 스크립트 참조


    //가까운 공격
    [SerializeField] float reach_distance_short = 6f; //공격 사정 거리
    [SerializeField] float precursor_time = 1.5f; //전조 시간

    //물기 변수
    public GameObject bite_area; //bite 공격 범위 오브젝트
    public AnimationCurve curve; //포물선 이동을 위한 AnimationCurve 선언
    [SerializeField] float bite_duration = 0.5f; //포물선 이동에 걸리는 시간

    //하울링 변수
    public GameObject ShockWave; //충격파 오브젝트
    [SerializeField] float howling_radius = 6.0f; //하울링 반경

    //달리기 변수
    public float movement_speed = 12f; // 이동 속도
    [SerializeField] float reach_distance_long = 18f; //공격 사정 거리
    [SerializeField] float run_duration = 1.0f; //달리기 이동에 걸리는 시간

    //충격파 변수
    public GameObject[] LeftGroundShock; //왼쪽 충격파 오브젝트
    public GameObject[] RightGroundShock; //오른쪽 충격파 오브젝트
    [SerializeField] int shock_num = 3; // 충격파 개수

    //애니메이터 변수
    private Animator animator;
    
    private bool is_track = true; // 현재 추적중인가
    private bool is_skill = false; // 현재 스킬 사용중인가
    private bool start_attack = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어가 있는지 확인
        sr = GetComponent<SpriteRenderer>(); //SpriteRenderer 할당
        scene = FindObjectOfType<BossDogScene>(); //BossDogScene 스크립트 할당
        animator = GetComponent<Animator>(); //Animator 할당
        
        //스킬의 공격 범위 비활성화
        bite_area.SetActive(false); //Bite의 공격 범위 비활성화
        ShockWave.SetActive(false); //Howling의 공격 범위 비활성화
        
        if (player != null)
        {
            Debug.Log("플레이어 발견");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //배틀이 시작되기 전까지는 보스는 움직이면 안됨.
        if (scene.battle_start)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (is_track && distance > track_range && !is_skill) // 트랙 중이고 거리가 최소 거리보다 크며 스킬 사용중이 아닐 때
            {
                CheckFlip();
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), boss_speed * Time.deltaTime);
            }
        }

        if (scene.battle_start && !start_attack)
        {
            start_attack = true;
            StartCoroutine(MoveCooldown()); // 무브 코루틴 시작
            StartCoroutine(SkillCooldown()); // 스킬 사용 코루틴 시작
        }
    }

    public IEnumerator MoveCooldown()
    {
        CheckFlip();
        Debug.Log("코루틴 시작");

        yield return new WaitForSeconds(boss_move_cooldown);
        is_track = !is_track; // 추적하는 상태와 그렇지 않은 상태를 번갈아서 반복
        StartCoroutine(MoveCooldown());
        
    }

    public IEnumerator SkillCooldown()
    {
        CheckFlip();
        Debug.Log("스킬 사용");
        is_skill = true;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < close_range)
        {
            skill = Random.Range(0, 2); // 가까울 때 모든 패턴 발생
        }
        else
        {
            skill = Random.Range(2, 4); // 멀 때 2가지 패턴만 발생
        }
        BossSkill(skill);
        yield return new WaitForSeconds(boss_skill_cooldown); // 스킬을 사용하지 않음
        StartCoroutine(SkillCooldown());
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌시 데미지
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌");

            //PlayerHealth 스크립트 할당
            player_health = collision.gameObject.GetComponent<PlayerHealth>();
            player_health.Damage(attack_power); //보스의 공격 세기만큼 플레이어의 hp 감소
        }
    }

    private void BossSkill(int skill) //보스가 어떤 스킬을 사용하는가?
    {
        Debug.Log(skill + "번 스킬 사용");
        switch (skill) {
            case 0: 
                StartCoroutine(Bite());
                break;
            case 1:
                StartCoroutine(Howling());
                break;
            case 2:
                StartCoroutine(Running());
                break;
            case 3:
                StartCoroutine(Stomping());
                break;
            default:
                break;
        }

    }

    private IEnumerator Bite()
    {
        Debug.Log("[Bite] : 보스가 이빨을 드러내며 그르렁 거림.");
        yield return new WaitForSeconds(precursor_time); // 전조시간동안 대기

        //플레이어의 현재 위치 파악
        Vector2 player_pos = player.transform.position;
        //보스의 시작 위치 파악
        Vector2 start = transform.position;
        float bite_distance = 0f; //보스가 이동할 거리

        //transform.position.x - player_pos.x 값이 양수면 플레이어는 보스의 왼쪽에 위치함.
        //반대로, 음수면 플레이어는 보스의 오른쪽에 위치함을 알 수 있음.
        if (transform.position.x - player_pos.x >= 0f)
        {
            //포물선 이동을 왼쪽으로 사정거리만큼 하도록 설정
            bite_distance = -1 * reach_distance_short;
            //보스의 스프라이트를 왼쪽 방향으로 설정
            sr.flipX = false;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(false);
        }
        else
        {
            //포물선 이동을 오른쪽으로 사정거리만큼 하도록 설정
            bite_distance = 1 * reach_distance_short;
            //보스의 스프라이트를 오른쪽 방향으로 설정
            sr.flipX = true;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(true);
        }

        //보스의 도착지점 위치 지정
        Vector2 end = new Vector2(transform.position.x + bite_distance, transform.position.y);
        //bite area 오브젝트 활성화(공격 범위 활성화)
        bite_area.SetActive(true);

        //해당 위치로 아치형을 그리며 돌진
        float time = 0f;
        while (time < bite_duration)
        {
            time += Time.deltaTime;
            float linearT = time / bite_duration;
            float heightT = curve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, 10f, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("[Bite] : bite area 비활성화");
        bite_area.SetActive(false);
        Debug.Log("[Bite] : 사용 완료");
        is_skill = false;
    }

    private IEnumerator Howling()
    {
        CheckFlip();
        Debug.Log("[Howling.cs] : 보스가 멈추고 고개를 든다.");
        yield return new WaitForSeconds(precursor_time); //전조 시간만큼 대기

        //충격파 오브젝트 활성화
        ShockWave.SetActive(true);
        //충격파 오브젝트 커지도록
        for (int i = 1; i <= howling_radius; i++)
        {
            //아래 시간을 조정함에 따라 충격파 발동 시간이 조절됨
            yield return new WaitForSeconds(0.05f);
            ShockWave.transform.localScale = new Vector2(1f + 0.5f * i, 1f + 0.5f * i);
        }
        yield return new WaitForSeconds(0.01f);
        ShockWave.SetActive(false); //충격파 오브젝트 비활성화
        //충격파 오브젝트 크기 원래대로 초기화
        ShockWave.transform.localScale = new Vector2(1f, 1f);
        is_skill = false; //스킬 중 해제
        CheckFlip();
    }

    private IEnumerator Running()
    {
        //플레이어의 현재 위치 파악
        Vector2 player_pos = player.transform.position;

        Debug.Log("[Running] : 보스가 몸을 웅크리고 으르릉 댄다.");
        yield return new WaitForSeconds(precursor_time); //전조 시간만큼 대기


        float run_distance; //보스가 이동할 거리

        //transform.position.x - player_pos.x 값이 양수면 플레이어는 보스의 왼쪽에 위치함.
        //반대로, 음수면 플레이어는 보스의 오른쪽에 위치함을 알 수 있음.
        if (transform.position.x - player_pos.x >= 0f)
        {
            //이동을 왼쪽으로 사정거리만큼 하도록 설정
            run_distance = -1 * reach_distance_long;
            //보스의 스프라이트를 왼쪽방향으로 설정
            sr.flipX = false;
        }
        else
        {
            //이동을 오른쪽으로 사정거리만큼 하도록 설정
            run_distance = 1 * reach_distance_long;
            //보스의 스프라이트를 오른쪽 방향으로 설정
            sr.flipX = true;
        }

        //보스의 도착지점 위치 지정
        Vector2 end = new Vector2(transform.position.x + run_distance, transform.position.y);

        //위치를 향해 돌진
        float time = 0f;
        float running_speed = Vector3.Distance(transform.position, end) / run_duration;
        Debug.Log(running_speed);
        while (time < run_duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, end, running_speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        Debug.Log("[Running] : 사용 완료");
        is_skill = false;
        CheckFlip();
    }

    private IEnumerator Stomping()
    {
        Debug.Log("[Stomping] : 보스가 앞발을 든다.");
        animator.SetTrigger("StompingPrecursor");
        yield return new WaitForSeconds(precursor_time); //전조 시간만큼 대기
        animator.SetTrigger("StompingAttack");
        bool is_right_attack; //오른쪽 공격인지 
        if (transform.position.x - player.transform.position.x >= 0f)
        {
            is_right_attack = false;
        }
        else
        {
            is_right_attack = true;
        }
        //충격파 오브젝트 순차적으로 발동
        for (int i = 0; i < shock_num; i++)
        {
            //아래 시간을 조정함에 따라 충격파 발동 시간이 조절됨
            yield return new WaitForSeconds(0.2f);
            
            //충격파 오브젝트 활성화
            if (!is_right_attack) 
            {
                //플레이어가 보스의 왼쪽에 위치할 경우
                sr.flipX = false;
                LeftGroundShock[i].SetActive(true);
            }
            else
            {
                //플레이어가 보스의 오른쪽에 위치할 경우
                sr.flipX = true;
                RightGroundShock[i].SetActive(true);
            }
            
        }
        //충격파 오브젝트 순차적으로 꺼짐
        for (int i = 0; i < shock_num; i++)
        {
            //아래 시간을 조정함에 따라 충격파 발동 시간이 조절됨
            yield return new WaitForSeconds(0.2f);

            //충격파 오브젝트 비활성화
            if (!is_right_attack) 
            {
                LeftGroundShock[i].SetActive(false);
            }
            else
            {
                RightGroundShock[i].SetActive(false);
            }
        }

        yield return new WaitForSeconds(0.1f);
        Debug.Log("[Stomping] : 사용 완료");
        is_skill = false;
    }

    //보스 스프라이트의 뒤집기 여부 체크하는 함수
    void CheckFlip()
    {
        
        //플레이어가 보스의 왼쪽에 위치한다면
        if (transform.position.x - player.transform.position.x >= 0f)
        {
            sr.flipX = false;
            //Debug.Log("왼쪽 체크");
        }
        else //플레이어가 보스의 오른쪽에 위치한다면
        {
            sr.flipX = true;
            //Debug.Log("오른쪽 체크");
        }
    }
}