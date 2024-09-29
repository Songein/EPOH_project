using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScratching : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트

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

    public IEnumerator Scratching()
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
            scratch_object.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            // Animator 컴포넌트를 가져와 scratch_scar 애니메이션 재생
            Animator scratchAnimator = scratch_object.GetComponent<Animator>();
            if (scratchAnimator != null)
            {
                scratchAnimator.Play("Scratch_scar"); // scratch_scar 애니메이션 재생
            }

            
            yield return new WaitForSeconds(3.0f); // 애니메이션 길이에 맞춰 대기

            Vector3 currentScale = scratch_object.transform.localScale;
            Vector3 currentPosition = scratch_object.transform.position; // 위치 저장
            Destroy(scratch_object);  // 기존 scratch_prefab 오브젝트 삭제

            GameObject pop_object = Instantiate(scratch_pop, currentPosition, Quaternion.identity); // scratch_pop로 교체
            pop_object.transform.localScale = currentScale; // 이전 오브젝트의 크기 유지

            // Animator 컴포넌트를 가져와 scratching_explode 애니메이션 재생
            Animator popAnimator = pop_object.GetComponent<Animator>();
            if (popAnimator != null)
            {
                popAnimator.Play("Scratching_explode"); // scratching_explode 애니메이션 재생
            }

            // 3초 후 폭발 효과 
            Destroy(pop_object);


            // 다음 할퀸 자국 생성 전 1초 대기
            if (i < scratch_count - 1)
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
}
