using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracking : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    /*
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    */

    //추적 변수
    public GameObject tracking_eye_prefab; // 추적 눈동자 프리팹
    public GameObject tracking_effect_prefab; // 추적 이펙트 프리팹
    public GameObject tracking_pop_prefab; // 추적 이펙트 프리팹

    private GameObject tracking_eye; // 추적 눈동자 오브젝트


    private void Awake()
    {
        dog = GetComponent<BossDogController>();
        player = dog._player;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // 추적 눈동자 생성
        Vector3 eyePosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        tracking_eye = Instantiate(tracking_eye_prefab, eyePosition, Quaternion.identity);
        tracking_eye.SetActive(false); // 시작 시 비활성화

    }

    void Update()
    {
        // 매 프레임마다 눈동자가 플레이어의 x축을 따라다니도록 설정
        if (tracking_eye != null && player != null)
        {
            tracking_eye.transform.position = new Vector3(player.transform.position.x, tracking_eye.transform.position.y, tracking_eye.transform.position.z);
        }
    }

    public void Activate()
    {
        tracking_eye.SetActive(true); // 추적 눈동자 활성화
        StartCoroutine(Tracking());
    }

    private IEnumerator Tracking()
    {

        float elapsedTime = 0f;
        float totalDuration = 5.0f; // 전체 추적 지속 시간
        float interval = 1.5f; // 추적 이펙트가 생성되고 폭발할 때까지의 간격

        while (elapsedTime < totalDuration)
        {
            // 플레이어 위치에 추적 이펙트 생성
            Vector3 effectSpawnPosition = player.transform.position;
            GameObject tracking_effect = Instantiate(tracking_effect_prefab, effectSpawnPosition, Quaternion.identity);

            // 1초 후 추적 폭발 이펙트 발생
            yield return new WaitForSeconds(interval);

            // 추적 폭발 이펙트 발생 (추적 이펙트 삭제 및 폭발 이펙트 생성)
            Destroy(tracking_effect); // 기존 이펙트 삭제
            GameObject explosion = Instantiate(tracking_pop_prefab, effectSpawnPosition, Quaternion.identity); // 폭발 이펙트 생성

            // 폭발 이펙트가 점점 커짐
            float explosionDuration = 0.5f; // 폭발이 커지는 시간
            float explosionElapsedTime = 0f;
            Vector3 originalScale = explosion.transform.localScale;

            while (explosionElapsedTime < explosionDuration)
            {
                explosionElapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(1.0f, 6.0f, explosionElapsedTime / explosionDuration); // 1배에서 4배로 크기 증가
                explosion.transform.localScale = originalScale * scale;
                yield return null;
            }

            Destroy(explosion); // 폭발 이펙트 제거

            elapsedTime += interval; // 경과 시간 증가
        }


        // 추적 눈동자 이펙트 비활성화
        tracking_eye.SetActive(false);

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
