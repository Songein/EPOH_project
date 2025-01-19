using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFork : MonoBehaviour, BossSkillInterface
{
    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    //할퀴기 변수
    public GameObject fork; // 할퀸 자국 프리팹을 fork로 변경


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
        StartCoroutine(Fork());
    }

    public IEnumerator Fork()
    {
        int fork_count = 3; // 총 3번의 할퀸 자국을 생성
        int number = 0;

        for (int i = 0; i < fork_count; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);
            
            // 할퀸 자국 프리팹 생성
            Vector3 fork_position = new Vector3(Random.Range(minX, maxX), player.transform.position.y, 0);
            GameObject fork_object = Instantiate(fork, fork_position, Quaternion.identity); // fork로 변경
            if (fork_object != null)
            {
                Debug.Log("Fork prefab instantiated successfully");
            }
            else
            {
                Debug.Log("Failed to instantiate fork prefab");
            }
            fork_object.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            // Animator 컴포넌트를 가져와 fork 애니메이션 재생
            Animator forkAnimator = fork_object.GetComponent<Animator>();
            if (forkAnimator != null)
            {
                Debug.Log("Fork Animator Runtime Controller: " + forkAnimator.runtimeAnimatorController.name);
                forkAnimator.Play("Scratching_scar"); // fork에 맞는 애니메이션 이름으로 설정
            }
            else
            {
                Debug.Log("Fork Animator Not Found");
            }

            yield return new WaitForSeconds(1.1f); // 애니메이션 길이에 맞춰 대기


            Vector3 currentScale = fork_object.transform.localScale;
            Vector3 currentPosition = fork_object.transform.position; // 위치 저장
            Destroy(fork_object);  // 기존 fork 오브젝트 삭제

            number++;
            Debug.Log("fork 개수: " + number);

            // 다음 할퀸 자국 생성 전 1초 대기
            if (i < fork_count - 1)
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
}
