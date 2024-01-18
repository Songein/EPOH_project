using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrack : MonoBehaviour
{

    private PlayerHealth player_health; //PlayerHealth 스크립트 참조

    public float boss_speed; // 보스의 이동 속도
    public float boss_skill_cooldown; // 보스 스킬 사용 쿨타임
    public float boss_move_cooldown; // 보스 이동 쿨타임

    public float attack_power = 10; // 보스 공격 세기

    public float range; // 보스가 플레이어를 추적하지 않는 거리

    public GameObject player; // 플레이어 게임 오브젝트

    Coroutine move_routine;
    Rigidbody2D boss_body;
    private bool is_track = true; // 현재 추적중인가
    private bool is_skill = false; // 현재 스킬 사용중인가


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어가 있는지 확인
        if (player != null)
        {
            Debug.Log("플레이어 발견");
        }
        boss_body = GetComponent<Rigidbody2D>();
        move_routine = StartCoroutine(MoveCooldown()); // 무브 코루틴 시작
        StartCoroutine(SkillCooldown()); // 스킬 사용 코루틴 시작
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (is_track && distance > range) // 트랙 중이고 거리가 최소 거리보다 크면
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), boss_speed * Time.deltaTime);
        }
    }

    public IEnumerator MoveCooldown()
    {
        Debug.Log("코루틴 시작");

        yield return new WaitForSeconds(boss_move_cooldown);
        is_track = !is_track; // 추적하는 상태와 그렇지 않은 상태를 번갈아서 반복
        move_routine = StartCoroutine(MoveCooldown());
    }

    public IEnumerator SkillCooldown()
    {
        Debug.Log("스킬 사용");
        yield return new WaitForSeconds(boss_skill_cooldown); // 스킬을 사용하지 않음
        StartCoroutine(SkillCooldown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌");

            //PlayerHealth 스크립트 할당
            player_health = collision.gameObject.GetComponent<PlayerHealth>();
            player_health.Damage(attack_power); //보스의 공격 세기만큼 플레이어의 hp 감소
        }
    }
}

