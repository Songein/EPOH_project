using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwaping : MonoBehaviour, BossSkillInterface
{
    public GameObject runningPrefab;

    public float shadow_speed = 10.0f; // 그림자 이동 속도

    //달리기 변수
    private SpriteRenderer warning_renderer;
    public float warning_duration = 3.0f; // 전조 영역 지속 시간
    public Color warning_color = Color.red; // 전조 영역 색상

    // 보스 표식 스프라이트
    public GameObject bossIconPrefab;
    
    public void Activate()
    {
        StartCoroutine(Swaping());
    }

    public IEnumerator Swaping()
    {
        // LeftPoint와 RightPoint 중 랜덤으로 시작 위치 설정
        Vector3 startPosition;
        Vector3 targetPosition;

        BossData bossData = BossManagerNew.Current.bossData;
        if (Random.Range(0, 2) == 0) // 0이면 Left, 1이면 Right
        {
            startPosition = bossData._leftBottom;
            targetPosition = new Vector3(bossData._rightTop.x,
                bossData._leftBottom.y, bossData._leftBottom.z);
        }
        else
        {
            startPosition = new Vector3(bossData._rightTop.x,
                bossData._leftBottom.y, bossData._leftBottom.z);
            targetPosition = bossData._leftBottom;
        }
        
        GameObject warningContainer = new GameObject("WarningContainer");

        // 전조 영역 생성 및 유지
        GameObject warning_area = new GameObject("WarningArea");
        warning_area.transform.SetParent(warningContainer.transform); // 부모 오브젝트의 자식으로 설정
        warning_area.transform.position = new Vector3((startPosition.x + targetPosition.x) / 2, startPosition.y, startPosition.z);
        warning_renderer = warning_area.AddComponent<SpriteRenderer>();

        // 전조 영역의 색상 
        Color warningColorWithAlpha = new Color(warning_color.r, warning_color.g, warning_color.b, 0.5f); // 알파값 0.5 설정
        warning_renderer.color = warningColorWithAlpha;
        warning_renderer.sortingOrder = 20; // 오브젝트 위로 표시되도록 순서 설정
        warning_renderer.sprite = CreateWarningSprite(); // 
        
        // 전조 영역의 크기 설정 (오브젝트가 이동할 거리만큼)
        float warning_width = Mathf.Abs(targetPosition.x - startPosition.x);
        warning_renderer.transform.localScale = new Vector3(warning_width, 2, 1);
        
        // 보스 표식 추가
        if (bossIconPrefab != null)
        {
            GameObject bossIcon = Instantiate(bossIconPrefab, warningContainer.transform);

            // 전조 영역의 왼쪽 끝과 오른쪽 끝의 중앙 가로값 계산
            float centerX = (startPosition.x + targetPosition.x) / 2;
            float centerY = startPosition.y; // y 좌표는 전조 영역과 동일하게 유지
            float centerZ = startPosition.z; // z 좌표도 동일하게 유지

            bossIcon.transform.position = new Vector3(centerX, centerY, centerZ); // 보스 표식 위치 설정
            // SpriteRenderer가 있는 경우 Sorting Order 설정
            SpriteRenderer bossIconRenderer = bossIcon.GetComponent<SpriteRenderer>();
            if (bossIconRenderer != null)
            {
                bossIconRenderer.sortingOrder = 21; // 전조 영역보다 높은 값으로 설정
            }
        }
        
        yield return new WaitForSeconds(warning_duration); // 전조 영역 유지

        Destroy(warningContainer); // 전조 영역 삭제

        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Swaping);
        // Animator 컴포넌트 추가 및 설정
        GameObject running_object = Instantiate(runningPrefab, startPosition, Quaternion.identity);

        SpriteRenderer runningSpriteRenderer = running_object.GetComponent<SpriteRenderer>();
        runningSpriteRenderer.sortingOrder = 10; 

        // 크기 조정 
        float scaleFactor = 0.2f; // 원하는 크기 비율 (0.5는 50%)
        running_object.transform.localScale = new Vector3(
            running_object.transform.localScale.x * scaleFactor,
            running_object.transform.localScale.y * scaleFactor,
            running_object.transform.localScale.z * scaleFactor
        );
        
        // Animator 컴포넌트를 가져와 scratch_scar 애니메이션 재생
        Animator runningAnimator = running_object.GetComponent<Animator>();

        // 좌우 반전 처리
        Vector3 originalScale = running_object.transform.localScale;
        if (targetPosition.x < startPosition.x) // 왼쪽으로 이동할 때
        {
            running_object.transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else // 오른쪽으로 이동할 때
        {
            running_object.transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }


        // Running 애니메이션 재생
        if (runningAnimator != null)
        {
            runningAnimator.Play("Running_Animation");
        }

        // 애니메이션 실행 동안 이동 처리
        while (Vector3.Distance(running_object.transform.position, targetPosition) > 0.1f)
        {
            running_object.transform.position = Vector3.MoveTowards(running_object.transform.position, targetPosition, shadow_speed * Time.deltaTime);
            yield return null;
        }

        // 도착 후 반대 방향으로 되돌아가기
        Vector3 reverseTarget = startPosition; // 되돌아갈 목표 지점

        // 방향 전환 처리
        Vector3 reverseScale = running_object.transform.localScale;
        running_object.transform.localScale = new Vector3(
            -reverseScale.x, // x축 방향 반전
            reverseScale.y,
            reverseScale.z
        );

        // 되돌아가는 이동 처리
        while (Vector3.Distance(running_object.transform.position, reverseTarget) > 0.1f)
        {
            running_object.transform.position = Vector3.MoveTowards(running_object.transform.position, reverseTarget, shadow_speed * Time.deltaTime);
            yield return null;
        }

        // 도착 후 애니메이션 종료 처리
        Destroy(running_object);

        yield return new WaitForSeconds(0.2f);
}

    // 기본 사각형 스프라이트 생성 함수
    private Sprite CreateWarningSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();

        Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        Vector2 pivot = new Vector2(0.5f, 0.5f);

        return Sprite.Create(texture, rect, pivot, 1.0f);
    }

}