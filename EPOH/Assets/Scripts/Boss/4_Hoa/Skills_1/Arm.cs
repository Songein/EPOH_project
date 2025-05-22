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
    [SerializeField] private int armSpeed;


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
       

        BossData bossData = BossManagerNew.Current.bossData;
       
        int angle = Random.Range(10, 45);

        SetData(bossData, angle);
    }

    public void SetData(BossData bossData, float angle)
    {
       

        StartCoroutine(ArmAttackSequence(angle, bossData));
    }

    private IEnumerator ArmAttackSequence(float angle, BossData bossData)
    {

        Vector3 leftSpawnPoint = new Vector3(bossData._leftBottom.x -15 , bossData._leftBottom.y + 5, 0);
        Vector3 rightSpawnPoint = new Vector3(bossData._rightTop.x +15, bossData._leftBottom.y + 5, 0);

        Vector3 spawnPoint = Random.Range(0, 2) == 0 ? leftSpawnPoint : rightSpawnPoint;

        if (spawnPoint == leftSpawnPoint)
        {
            Vector3 waringspawnPoint = new Vector3(bossData._leftBottom.x , bossData._leftBottom.y + 5, 0);
            // 경고 스프라이트 생성
            Vector3 warningPosition = GetPositionAtAngle(angle, armLength, waringspawnPoint);
            Debug.Log("각도 " + angle);
            GameObject warning = Instantiate(warningPrefab, warningPosition, Quaternion.identity);

            Vector3 direction = (warningPosition - spawnPoint).normalized; ///warningposition이 spqwnPoint를 바라보는 방향으로 위치 조정
        

            // 기존 방향을 기준으로 180도 추가 회전
            warning.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.AngleAxis(180, Vector3.forward);

            // 경고 표시 대기
            yield return new WaitForSeconds(warningTime);

            // 경고 스프라이트 제거
            Destroy(warning);

            // 팔 생성
            GameObject arm = Instantiate(armPrefab, leftSpawnPoint, Quaternion.identity);
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Arm); //소리
            Rigidbody2D armRb = arm.AddComponent<Rigidbody2D>();

            arm.transform.right = direction;
            armRb.velocity = arm.transform.right * armSpeed; // 방향에 속도를 곱해서 앞으로 나가도록 설정
            armRb.gravityScale = 0; // 중력 영향 제거


            // 기존 방향을 기준으로 180도 추가 회전
            arm.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.AngleAxis(180, Vector3.forward);

            // 팔 이동 시간
            yield return new WaitForSeconds(0.5f);
            //팔 정지
            armRb.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            //팔 다시 들어가기
            armRb.velocity = direction * -armSpeed;
            //팔 제거 대기
            yield return new WaitForSeconds(armDestroyTime);
            //팔 제거
            Destroy(arm);
        }
        else {

            Vector3 waringspawnPoint = new Vector3(bossData._rightTop.x-10, bossData._leftBottom.y + 5, 0);

            // 경고 스프라이트 생성
            Vector3 warningPosition = GetPositionAtAngle(angle, armLength, waringspawnPoint);
            Debug.Log("각도 " + angle);
            GameObject warning = Instantiate(warningPrefab, warningPosition, Quaternion.identity);

            // 경고 방향 설정
            warning.transform.up = -(warningPosition - spawnPoint).normalized;
            
            /*
            warning.transform.localScale = new Vector3(-warning.transform.localScale.x,
                                       warning.transform.localScale.y,
                                       warning.transform.localScale.z); //대칭
            */
            // 경고 표시 대기
            yield return new WaitForSeconds(warningTime);

            // 경고 스프라이트 제거
            Destroy(warning);

            // 팔 생성
            GameObject arm = Instantiate(armPrefab, rightSpawnPoint, Quaternion.identity);
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Arm); //소리

            //arm.transform.up = (warningPosition - spawnPoint).normalized;

            /*
            arm.transform.localScale = new Vector3(-arm.transform.localScale.x,
                                       arm.transform.localScale.y,
                                       arm.transform.localScale.z); //대칭
            */

            arm.transform.up = -(warningPosition - spawnPoint).normalized;

            Rigidbody2D armRb = arm.AddComponent<Rigidbody2D>();
            armRb.gravityScale = 0; // 중력 영향 제거
            armRb.velocity = arm.transform.up * -armSpeed; // 방향에 속도를 곱해서 앞으로 나가도록 설정


            // 팔 이동 시간
            yield return new WaitForSeconds(0.5f);
            //팔 정지
            armRb.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            //팔 다시 들어가기
            armRb.velocity = arm.transform.up * armSpeed;
            //팔 제거 대기
            yield return new WaitForSeconds(armDestroyTime);
            //팔 제거
            Destroy(arm);
        }



      
        BossManagerNew.Current.OnSkillEnd?.Invoke();
    }

    private Vector3 GetPositionAtAngle(float angle, float radius, Vector3 spawnPoint)
    {
        float radian = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        float x = spawnPoint.x + Mathf.Cos(radian) * radius;
        float y = spawnPoint.y + Mathf.Sin(radian) * radius;
        //float y = Random.Range(spawnPoint.y - 5, spawnPoint.y + 5) + Mathf.Sin(radian) * radius;
        return new Vector3(x, y, 0);
    }

}
