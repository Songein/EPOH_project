using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDog : MonoBehaviour
{

    private PlayerHealth player_health; //PlayerHealth 스크립트 참조

    [SerializeField] float boss_speed; // 보스의 이동 속도
    [SerializeField] float boss_skill_cooldown; // 보스 스킬 사용 쿨타임
    [SerializeField] float boss_move_cooldown; // 보스 이동 쿨타임

    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    [SerializeField] float track_range; // 보스가 플레이어를 추적하지 않는 거리
    [SerializeField] float close_range = 6f; // 보스와 플레이어가 어느 정도 거리일 때 가깝다고 간주하는가

    public GameObject player; // 플레이어 게임 오브젝트
    private int skill; // 사용하는 스킬


    //가까운 공격
    [SerializeField] float reach_distance_short = 6f; //공격 사정 거리
    [SerializeField] float precursor_time = 1.5f; //전조 시간

    //물기 변수
    public GameObject bite_area; //bite 공격 범위 오브젝트
    public AnimationCurve curve; //포물선 이동을 위한 AnimationCurve 선언
    [SerializeField] float duration = 0.5f; //포물선 이동에 걸리는 시간

    //하울링 변수
    public GameObject ShockWave; //충격파 오브젝트
    [SerializeField] float howling_radius = 6.0f; //하울링 반경

    //달리기 변수
    public float movement_speed = 12f; // 이동 속도



    private bool is_track = true; // 현재 추적중인가
    private bool is_skill = false; // 현재 스킬 사용중인가

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어가 있는지 확인
        bite_area.SetActive(false);
        if (player != null)
        {
            Debug.Log("플레이어 발견");
        }
        StartCoroutine(MoveCooldown()); // 무브 코루틴 시작
        StartCoroutine(SkillCooldown()); // 스킬 사용 코루틴 시작
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (is_track && distance > track_range && !is_skill) // 트랙 중이고 거리가 최소 거리보다 크며 스킬 사용중이 아닐 때
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), boss_speed * Time.deltaTime);
        }
    }

    public IEnumerator MoveCooldown()
    {
        Debug.Log("코루틴 시작");

        yield return new WaitForSeconds(boss_move_cooldown);
        is_track = !is_track; // 추적하는 상태와 그렇지 않은 상태를 번갈아서 반복
        StartCoroutine(MoveCooldown());
    }

    public IEnumerator SkillCooldown()
    {
        Debug.Log("스킬 사용");
        is_skill = true;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < close_range)
        {
            skill = Random.Range(0, 2); // 가까울 때 2가지 패턴
        }
        else
        {
            skill = Random.Range(0, 2); // 멀 때 2가지 패턴
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
                StartCoroutine(StartHowling());
                break;
            case 2:
                break;
            case 3:
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
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(false);
        }
        else
        {
            //포물선 이동을 오른쪽으로 사정거리만큼 하도록 설정
            bite_distance = 1 * reach_distance_short;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(true);
        }

        //보스의 도착지점 위치 지정
        Vector2 end = new Vector2(transform.position.x + bite_distance, transform.position.y);
        //bite area 오브젝트 활성화(공격 범위 활성화)
        bite_area.SetActive(true);

        //해당 위치로 아치형을 그리며 돌진
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, 10f, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Debug.Log("[Bite] : bite area 비활성화");
        bite_area.SetActive(false);
        Debug.Log("[Bite] : 사용 완료");
        is_skill = false;
    }

    private IEnumerator StartHowling()
    {
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
    }

    private IEnumerator Running()
    {
        Debug.Log("[Running] : 보스가 몸을 웅크리고 으르릉 댄다.");
        yield return new WaitForSeconds(precursor_time); //전조 시간만큼 대기

    }

    private IEnumerator Stomping()
    {
        Debug.Log("[Stomping] : 보스가 앞발을 든다.");
        yield return new WaitForSeconds(precursor_time); //전조 시간만큼 대기

    }
}