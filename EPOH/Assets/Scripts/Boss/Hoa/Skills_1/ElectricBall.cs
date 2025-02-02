using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    //  private BossDogController dog; 
    public GameObject player; // 플레이어 게임 오브젝트
    private List<GameObject> balls;
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    
    //ball 변수
    public GameObject ball_prefab; // ball 프리팹
    public GameObject ball_pop; // 폭발 프리팹

    private void Awake()
    {
        //dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        // player = dog._player;

    }

    void Start()
    {
        Camera mainCamera = Camera.main; //Camera.main은 게임 오브젝트가 활성화된 후에야 사용 가능 전역변수 X
        // Scene의 가장 왼쪽과 오른쪽 좌표를 설정
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
    }

    public void Activate()
    {
        StartCoroutine(electricBall());
        StartCoroutine(electric_pop());
    }





    public IEnumerator electricBall()
    {
       
        int ball_count = 4; // 총 4번의 electricball 을 생성
        int number = 0;
        balls = new List<GameObject>();
        for (int i = 0; i < ball_count; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);

            // 할퀸 자국 프리팹 생성
            Vector3 ball_position = new Vector3(Random.Range(minX, maxX), player.transform.position.y, 0);
            GameObject ball_object = Instantiate(ball_prefab, ball_position, Quaternion.identity); // 프리팹 사용
            this.balls.Add(ball_object);
            if (ball_object != null)
            {
                Debug.Log("ball prefab instantiated successfully");
            }
            else
            {
                Debug.Log("Failed to instantiate ball prefab");
            }
            //ball_object.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            /*

            // Animator 컴포넌트를 가져와 scratch_scar 애니메이션 재생
            Animator scratchAnimator = ball_object.GetComponent<Animator>();
            if (scratchAnimator != null)
            {
                Debug.Log("ball Animator Runtime Controller: " + scratchAnimator.runtimeAnimatorController.name);
                scratchAnimator.Play("Scratching_scar");
            }
            else
            {
                Debug.Log("ball Animator Not Found");
            }
            */

           // Destroy(ball_object);
            number++;
            Debug.Log("ball 횟수: " + number);

           yield return new WaitForSeconds(0.5f); // 애니메이션 길이에 맞춰 대기
        }
    }


    public IEnumerator electric_pop()
    {
        int ball_count = 4; // 총 4번의 electricball 을 생성
        int number = 0;
        for (int i = 0; i < ball_count; i++)
        {
            Vector3 currentScale = balls[i].transform.localScale; // 위치 저장
            Vector3 currentPosition = balls[i].transform.position;
            yield return new WaitForSeconds(0.5f);
            //ball_object.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            /*

            // Animator 컴포넌트를 가져와 scratch_scar 애니메이션 재생
            Animator scratchAnimator = ball_object.GetComponent<Animator>();
            if (scratchAnimator != null)
            {
                Debug.Log("ball Animator Runtime Controller: " + scratchAnimator.runtimeAnimatorController.name);
                scratchAnimator.Play("Scratching_scar");
            }
            else
            {
                Debug.Log("ball Animator Not Found");
            }
            */

            // Destroy(ball_object);
            

           
            Destroy(balls[i]);  // 기존 scratch_prefab 오브젝트 삭제

            GameObject pop_object = Instantiate(ball_pop, currentPosition, Quaternion.identity); // ball_pop로 교체
            pop_object.transform.localScale = currentScale; // 이전 오브젝트의 크기 유지
            yield return new WaitForSeconds(1f);

            /*

            Animator popAnimator = pop_object.GetComponent<Animator>();
            if (popAnimator != null)
            {
                Debug.Log("Pop Animator Runtime Controller: " + popAnimator.runtimeAnimatorController.name);
                popAnimator.Play("Scratching_explode"); // scratching_explode 애니메이션 재생
            }

            yield return new WaitForSeconds(popAnimator.GetCurrentAnimatorStateInfo(0).length);
            */

            Destroy(pop_object);
            number++;
            Debug.Log("pop 횟수: " + number);

            /*
                // 다음 ball 자국 생성 전 1초 대기
                if (i < ball_count - 1)
                {
                    yield return new WaitForSeconds(1.0f);
                }
            */
            //yield return new WaitForSeconds(0.2f);
        }
    }

}
    
     
   



