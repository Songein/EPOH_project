using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowing : MonoBehaviour, BossSkillInterface
{
    public GameObject platePrefab; // 접시 프리팹
    public float throwSpeed = 10f; // 접시 던지는 속도
    public float minThrowAngle = 10f; // 최소 던지기 각도
    public float maxThrowAngle = 45f; // 최대 던지기 각도
    public float throwInterval = 1.0f; // 접시 던지는 간격
    public float attackDuration = 5.0f; // 공격 지속 시간

    private bool isThrowing = false; // 던지기 활성화 상태
    //private Vector3 leftSpawnPoint; // 왼쪽 스폰 위치
    //private Vector3 rightSpawnPoint; // 오른쪽 스폰 위치

    //private void Start()
    //{
    //    // 카메라의 뷰포트를 기준으로 왼쪽과 오른쪽 끝 지점 계산
    //    Camera mainCamera = Camera.main;
    //    if (mainCamera != null)
    //    {
    //        leftSpawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane));
    //        rightSpawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane));

    //        // z값 보정 (2D 환경에서는 0으로 설정)
    //        leftSpawnPoint.z = 0;
    //        rightSpawnPoint.z = 0;
    //    }
    //}

    public void Activate()
    {
        StartCoroutine(ThrowPlates());
    }

    private IEnumerator ThrowPlates()
    {
        float elapsedTime = 0f;
        BossData bossData = BossManagerNew.Current.bossData;
        Vector3 leftSpawnPoint = new Vector3(bossData._leftBottom.x, bossData._leftBottom.y + 3, 0);
        Vector3 rightSpawnPoint = new Vector3(bossData._rightTop.x, bossData._leftBottom.y + 3, 0);

        while (elapsedTime < attackDuration)
        {
            // 왼쪽 또는 오른쪽에서 랜덤으로 스폰
            Vector3 spawnPoint = Random.Range(0, 2) == 0 ? leftSpawnPoint : rightSpawnPoint;

            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Throwing);
            // 접시 생성
            GameObject plate = Instantiate(platePrefab, spawnPoint, Quaternion.identity);
            if (plate == null)
            {
                Debug.LogError("Failed to instantiate platePrefab. Please check the prefab assignment.");
                yield break;
            }

            // 크기 조정
            float scaleFactor = 0.5f; // 접시 크기 비율 (1.0은 원래 크기, 0.5는 50% 크기)
            plate.transform.localScale = new Vector3(
                plate.transform.localScale.x * scaleFactor,
                plate.transform.localScale.y * scaleFactor,
                plate.transform.localScale.z * scaleFactor
            );

            // 애니메이션 방향 설정 (좌 -> 우 또는 우 -> 좌)
            Animator animator = plate.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("LeftToRight", spawnPoint == leftSpawnPoint);
            }

            // 던지기 각도 계산
            float throwAngle = Random.Range(minThrowAngle, maxThrowAngle);
            Vector2 throwDirection;

            if (spawnPoint == leftSpawnPoint)
            {
                // 왼쪽에서 생성 -> 오른쪽 대각선으로 던짐
                throwDirection = new Vector2(Mathf.Cos(throwAngle * Mathf.Deg2Rad), Mathf.Sin(throwAngle * Mathf.Deg2Rad));
            }
            else
            {
                // 오른쪽에서 생성 -> 왼쪽 대각선으로 던짐
                throwDirection = new Vector2(-Mathf.Cos(throwAngle * Mathf.Deg2Rad), Mathf.Sin(throwAngle * Mathf.Deg2Rad));
            }

            // 접시에 힘 적용
            Rigidbody2D rb = plate.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = throwDirection * throwSpeed;
            }
            else
            {
                Debug.LogError("Rigidbody2D component is missing on the platePrefab.");
                yield break;
            }

            // plate가 ground와 충돌하면 사라지도록 설정
            PlateCollisionHandler plateCollision = plate.AddComponent<PlateCollisionHandler>();

            // 다음 접시를 던지기 전에 대기
            yield return new WaitForSeconds(throwInterval);
            elapsedTime += throwInterval;
        }
    }



}

// 접시 충돌 처리를 위한 스크립트
public class PlateCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // 접시 제거
        }
    }
}
