using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracking : MonoBehaviour
{
    public GameObject player; // 플레이어 게임 오브젝트
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    [SerializeField] float reach_distance_short; // 포물선 이동 거리
    [SerializeField] float Dog_min_area; // 이동 가능 최소 x 위치
    [SerializeField] float Dog_max_area; // 이동 가능 최대 x 위치
    [SerializeField] float Dog_yposition; // 이동할 y 위치

    public GameObject shadow_prefab; // 그림자 오브젝트 프리팹
    public float shadow_speed = 10.0f; // 그림자 이동 속도

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    //추적 변수
    public GameObject tracking_eye_prefab; // 추적 눈동자 프리팹
    public GameObject tracking_effect_prefab; // 추적 이펙트 프리팹
    public float tracking_effect_speed = 5.0f; // 추적 이펙트의 속도


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어가 있는지 확인

        
        if (player != null)
        {
            Debug.Log("플레이어 발견");
        }

        // 씬의 가장 왼쪽과 오른쪽 좌표를 설정 (카메라 뷰포트를 기준으로 계산)
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        
    }

    public void Track()
    {
        StartCoroutine(Tracking());
    }

    private IEnumerator Tracking()
    {
        // 플레이어 위치의 y축 2에 추적 눈동자 프리팹 생성
        Vector3 eyePosition = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
        GameObject tracking_eye = Instantiate(tracking_eye_prefab, eyePosition, Quaternion.identity);

        // 플레이어가 바라보는 방향 뒤쪽에 추적 이펙트 프리팹 생성
        Vector3 effectSpawnPosition = player.transform.position - player.transform.right * 2.0f; // 플레이어 뒤쪽에 2단위 거리로 생성
        GameObject tracking_effect = Instantiate(tracking_effect_prefab, effectSpawnPosition, Quaternion.identity);

        float elapsedTime = 0f;
        float duration = 5.0f; // 추적 이펙트가 플레이어를 따라다니는 시간

        while (elapsedTime < duration)
        {
            tracking_eye.transform.position = new Vector3(player.transform.position.x, tracking_eye.transform.position.y, tracking_eye.transform.position.z);

            // 추적 이펙트가 플레이어의 방향으로 일정 속도로 따라가도록 설정
            Vector3 directionToPlayer = (player.transform.position - tracking_effect.transform.position).normalized;
            tracking_effect.transform.position += directionToPlayer * tracking_effect_speed * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 5초 후 추적 눈동자와 이펙트 제거
        Destroy(tracking_eye);
        Destroy(tracking_effect);

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
}
