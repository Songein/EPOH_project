using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScratching : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트
    [SerializeField] float attack_power = 10f; // 보스 공격 세기

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    //할퀴기 변수
    public GameObject scratch_prefab; // 할퀸 자국 프리팹
    public GameObject scratch_pop; // 폭발 프리팹

    private void Awake()
    {
        dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        player = dog._player;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Scene의 가장 왼쪽과 오른쪽 좌표를 설정
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
    }

    public void Activate()
    {
        StartCoroutine(Scratching());
    }

    private IEnumerator Scratching()
    {
        int scratch_count = 3; // 총 3번의 할퀸 자국을 생성

        for (int i = 0; i < scratch_count; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);
            
            // 할퀸 자국 프리팹 생성
            Vector3 scratch_position = new Vector3(Random.Range(leftEdge.x, rightEdge.x), player.transform.position.y, 0);
            GameObject scratch_object = Instantiate(scratch_prefab, scratch_position, Quaternion.identity); // 프리팹 사용

            // 초기 투명한 빨간색 설정
            SpriteRenderer scratch_renderer = scratch_object.GetComponent<SpriteRenderer>();

            scratch_renderer.sortingOrder = 10;

            Color initialColor = new Color(1, 0, 0, 0); // 투명한 빨간색
            scratch_renderer.color = initialColor;

            // 색상이 점점 진해짐
            float duration = 3.0f; // 3초간 색상 변화
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / duration); // 0에서 1로 알파 값 증가
                scratch_renderer.color = new Color(1, 0, 0, alpha); // 점점 진해지는 빨간색

                yield return null;
            }

            Vector3 currentScale = scratch_object.transform.localScale;
            Vector3 currentPosition = scratch_object.transform.position; // 위치 저장
            Destroy(scratch_object);  // 기존 scratch_prefab 오브젝트 삭제

            GameObject pop_object = Instantiate(scratch_pop, currentPosition, Quaternion.identity); // scratch_pop로 교체
            pop_object.transform.localScale = currentScale; // 이전 오브젝트의 크기 유지

            // scratch_pop의 sortingOrder 설정
            SpriteRenderer pop_renderer = pop_object.GetComponent<SpriteRenderer>();
            pop_renderer.sortingOrder = 10;


            // 크기 변화 시작
            float scaleDuration = 1.5f; // 크기가 커지는 시간
            float scaleElapsedTime = 0f;
            Vector3 originalScale = pop_object.transform.localScale; // 원래 크기 저장

            while (scaleElapsedTime < scaleDuration)
            {
                scaleElapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(1.0f, 4.0f, scaleElapsedTime / scaleDuration); // 1배에서 4배까지
                pop_object.transform.localScale = originalScale * scale; // 원래 크기의 1배에서 4배로 확대

                yield return null; // 프레임을 넘겨 루프가 반복되도록 설정
            }


            // 3초 후 폭발 효과 
            Destroy(pop_object);


            // 다음 할퀸 자국 생성 전 1초 대기
            if (i < scratch_count - 1)
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
