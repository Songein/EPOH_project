using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UP_ElectricBall : MonoBehaviour
{
    //  private BossDogController dog; 
    public GameObject player; // 플레이어 게임 오브젝트
    private List<GameObject> balls;
    [SerializeField] private Vector3 leftEdgePoint;
    [SerializeField] private Vector3 rightEdgePoint;
    [SerializeField] private int ball_count;


    //ball 변수
    [SerializeField] private GameObject ball_prefab; // ball 프리팹
    [SerializeField] private GameObject ball_Nprefab; // ball 프리팹
    [SerializeField] private GameObject ball_pop; // 폭발 프리팹


    private void Awake()
    {
        //dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        // player = dog._player;

    }

    void Start()
    {

    }

    public void Activate()
    {
        electricBall();
        StartCoroutine(electric_pop());
    }





    public void electricBall()
    {
        Vector3 leftEdge = leftEdgePoint;
        Vector3 rightEdge = rightEdgePoint;
         // 총 10의 electricball 을 생성
        int number = 0;
        balls = new List<GameObject>();
        for (int i = 0; i < ball_count -4; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            //float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            // float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);

            // 기본 ball 프리팹 생성
            Vector3 ball_position = new Vector3(Random.Range(leftEdge.x, rightEdge.x), Random.Range(leftEdge.y, rightEdge.y), 0);
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

          
        }
        for (int i = ball_count - 4; i < ball_count; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            //float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            // float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);

            // ball  프리팹 생성
            Vector3 ball_position = new Vector3(Random.Range(leftEdge.x, rightEdge.x), Random.Range(leftEdge.y, rightEdge.y), 0);
          
            bool noSamePosition = balls.All(ball => ball.transform.position != ball_position); //NO SAME POSITION
            if (noSamePosition)
            {//같은 위치가 아니라면 NBALL 생성
                GameObject ball_object = Instantiate(ball_Nprefab, ball_position, Quaternion.identity); // 프리팹 사용
                this.balls.Add(ball_object);
                
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


            }
        }
    }



    public IEnumerator electric_pop()
    {
        yield return new WaitForSeconds(3f); //3초 뒤에 폭발할 수 있게
        int number = 0;
        for (int i = 0; i < ball_count - 4; i++)
        {//기본 볼 한꺼번에 삭제



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

           



            Destroy(balls[i]);  // 기존 ballprefab 삭제


        }
        for (int i = 0; i < ball_count - 4; i++)
        { //pop되는거 한꺼번에 나오게 하기
            Vector3 currentScale = balls[i].transform.localScale; // 위치 저장
            Vector3 currentPosition = balls[i].transform.position;

            GameObject pop_object = Instantiate(ball_pop, currentPosition, Quaternion.identity); // ball_pop로 교체
            this.balls[i] = pop_object;
            if (pop_object != null)
            {
                Debug.Log("ball prefab instantiated successfully");
            }
            else
            {
                Debug.Log("Failed to instantiate ball prefab");
            }
            pop_object.transform.localScale = currentScale; // 이전 오브젝트의 크기 유지
        }


        /*

        Animator popAnimator = pop_object.GetComponent<Animator>();
        if (popAnimator != null)
        {
            Debug.Log("Pop Animator Runtime Controller: " + popAnimator.runtimeAnimatorController.name);
            popAnimator.Play("Scratching_explode"); // scratching_explode 애니메이션 재생
        }

        yield return new WaitForSeconds(popAnimator.GetCurrentAnimatorStateInfo(0).length);
        */
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < ball_count; i++)
            { //Nball, Popball 삭제
            Destroy(balls[i]);
            number++;
            Debug.Log("pop 횟수: " + number);

        }
      

    }

 
    
}
