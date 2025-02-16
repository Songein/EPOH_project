using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour, BossSkillInterface
{
    public GameObject warningPrefab; // 경고 스프라이트 프리팹
    [SerializeField] private GameObject armPrefab; // 팔 스프라이트 프리팹
    [SerializeField] private float warningTime; // 경고 표시 시간
    [SerializeField] private float armDestroyTime = 1.0f;
    [SerializeField] private float armLength; // 팔 길이 (중심에서 팔까지 거리)


    private void Start()
    { /*
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // 뷰포트를 기준으로 왼쪽과 오른쪽 끝 지점 계산
            leftSpawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.2f, mainCamera.nearClipPlane));
            rightSpawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.2f, mainCamera.nearClipPlane));

            // z값 보정 (2D 환경에서는 0으로 설정)
            leftSpawnPoint.z = 0;
            rightSpawnPoint.z = 0;

          
        } */
    }

    public void Activate()
    {
        if (BossManagerNew.Current == null)
        {
            Debug.LogError("BossManagerNew Current is not found!");
            return;
        }

        BossData bossData = BossManagerNew.Current.bossData;
        if (bossData == null)
        {
            Debug.LogError("BossData is not assigned in BossManagerNew!");
            return;
        }
        int angle = Random.Range(10, 45);

        SetData(bossData, angle);
    }

    public void SetData(BossData bossData, float angle)
    {
        Vector3 leftSpawnPoint = new Vector3(bossData._leftBottom.x +10, bossData._leftBottom.y + 5, 0);
        Vector3 rightSpawnPoint = new Vector3(bossData._rightTop.x -10, bossData._leftBottom.y + 5, 0);

        Vector3 spawnPoint = Random.Range(0, 2) == 0 ? leftSpawnPoint : rightSpawnPoint;

        StartCoroutine(ArmAttackSequence(angle, spawnPoint, bossData));
    }

    private IEnumerator ArmAttackSequence(float angle, Vector3 spawnPoint, BossData bossData)
    {
        // 경고 스프라이트 생성
        Vector3 warningPosition = GetPositionAtAngle(angle, armLength, spawnPoint);
        Debug.Log("각도 " + angle);
        GameObject warning = Instantiate(warningPrefab, warningPosition, Quaternion.identity);

        // 경고 방향 설정
        warning.transform.up = (warningPosition - spawnPoint).normalized;

        // 경고 표시 대기
        yield return new WaitForSeconds(warningTime);

        // 경고 스프라이트 제거
        Destroy(warning);

        // 팔 생성
        GameObject arm = Instantiate(armPrefab, warningPosition, Quaternion.identity);
        arm.transform.up = (warningPosition - spawnPoint).normalized; // 팔 방향 설정

        // 팔 제거 대기
        yield return new WaitForSeconds(armDestroyTime);
        Destroy(arm);
    }

    private Vector3 GetPositionAtAngle(float angle, float radius, Vector3 spawnPoint)
    {
        float radian = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        float x = spawnPoint.x + Mathf.Cos(radian) * radius;
        float y = Random.Range(spawnPoint.y - 5, spawnPoint.y + 5) + Mathf.Sin(radian) * radius;
        return new Vector3(x, y, 0);
    }

}
