using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour, BossSkillInterface
{
    public GameObject warningPrefab; // 경고 스프라이트 프리팹
    [SerializeField]  private GameObject armPrefab; // 팔 스프라이트 프리팹
    [SerializeField]  public Transform leftSpawnPoint;
    [SerializeField] public Transform rightSpawnPoint;// 기준 고정 위치
    [SerializeField] private float warningTime; // 경고 표시 시간
    [SerializeField] private float armDestroyTime = 1.0f;
    [SerializeField]   private float armLength = 10.0f; // 팔 길이 (중심에서 팔까지 거리)
    private Transform spawnPoint;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // 뷰포트를 기준으로 왼쪽과 오른쪽 끝 지점 계산
            Vector3 leftPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane));
            Vector3 rightPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane));

            // z값 보정 (2D 환경에서는 0으로 설정)
            leftPosition.z = 0;
            rightPosition.z = 0;

            // Transform의 position에 값을 할당
            leftSpawnPoint.position = leftPosition;
            rightSpawnPoint.position = rightPosition;
        }
    }

    public void Activate() {
        int angle = Random.Range(10, 45);
       
        TriggerArmAttack(angle);
}

    public void TriggerArmAttack(float angle)
    {
        Transform spawnPoint = Random.Range(0, 2) == 0 ? leftSpawnPoint : rightSpawnPoint;
        if (spawnPoint == leftSpawnPoint)
        {
            Debug.Log("it's 1");
        }
        else
        {
            Debug.Log("Its 2");
        }
        Debug.Log("Random Angle: " + angle);

        StartCoroutine(ArmAttackSequence(angle, spawnPoint));
    }

    private IEnumerator ArmAttackSequence(float angle, Transform spawnPoint)
    {

        if (spawnPoint == leftSpawnPoint)
        {
            // 1. 경고 스프라이트 생성
            Vector3 warningPosition = GetPositionAtAngle(angle, armLength, spawnPoint);
            GameObject warning = Instantiate(warningPrefab, warningPosition, Quaternion.identity);


            // 2. 경고 표시 대기
            yield return new WaitForSeconds(warningTime);

            // 3. 경고 스프라이트 제거
            Destroy(warning);

            // 4. 팔 생성
            Vector3 armPosition = GetPositionAtAngle(angle, armLength, spawnPoint);
            GameObject arm = Instantiate(armPrefab, armPosition, Quaternion.identity);

            arm.transform.up = (armPosition - leftSpawnPoint.position).normalized; // 팔의 방향 설정

            yield return new WaitForSeconds(armDestroyTime);
            Destroy(arm);
        }
        else {
            // 1. 경고 스프라이트 생성
            Vector3 warningPosition = GetPositionAtAngle(angle, armLength, spawnPoint);
            GameObject warning = Instantiate(warningPrefab, warningPosition, Quaternion.identity);


            // 2. 경고 표시 대기
            yield return new WaitForSeconds(warningTime);

            // 3. 경고 스프라이트 제거
            Destroy(warning);

            // 4. 팔 생성
            Vector3 armPosition = GetPositionAtAngle(angle, armLength, spawnPoint);
            GameObject arm = Instantiate(armPrefab, armPosition, Quaternion.identity);

            arm.transform.up = (armPosition - rightSpawnPoint.position).normalized; // 팔의 방향 설정

            yield return new WaitForSeconds(armDestroyTime);
            Destroy(arm);
        }
    }

    private Vector3 GetPositionAtAngle(float angle, float radius, Transform spawnPoint)
    {
        if (spawnPoint == leftSpawnPoint)
        {
            // 중심 기준으로 특정 각도와 반지름(radius)을 사용해 위치 계산
            float radian = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            float x = leftSpawnPoint.position.x + Mathf.Cos(radian) * radius;
            float y = leftSpawnPoint.position.y + Mathf.Sin(radian) * radius;
            return new Vector3(x, y, 0);
        }
        else {
            // 중심 기준으로 특정 각도와 반지름(radius)을 사용해 위치 계산
            float radian = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            float x = rightSpawnPoint.position.x -(Mathf.Cos(radian) * radius);
            float y = rightSpawnPoint.position.y + Mathf.Sin(radian) * radius;
            return new Vector3(x, y, 0);
        }
    }
}
