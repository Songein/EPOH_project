using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    //  private BossDogController dog; 
    public GameObject player; // 플레이어 게임 오브젝트
    private List<GameObject> balls;
    [SerializeField] private Vector3 leftEdgePoint;
    [SerializeField] private Vector3 rightEdgePoint;


    //ball 변수
    [SerializeField] private GameObject ball_prefab; // ball 프리팹
                                                     //[SerializeField] private GameObject ball_pop; // 폭발 프리팹

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
        StartCoroutine(electricBall());
        StartCoroutine(electric_pop());
    }





    public IEnumerator electricBall()
    {
        Vector3 leftEdge = leftEdgePoint;
        Vector3 rightEdge = rightEdgePoint;
        int ball_count = 4; // 총 4번의 electricball 을 생성
        int number = 0;
        balls = new List<GameObject>();
        for (int i = 0; i < ball_count; i++)
        {
            // 할퀸 자국이 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            //float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
            // float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);


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

            number++;
            Debug.Log("ball 횟수: " + number);

            yield return new WaitForSeconds(1f); // 애니메이션 길이에 맞춰 대기
        }
    }


    public IEnumerator electric_pop()
    {
        int ball_count = 4; // 총 4번의 electricball 을 생성
        int number = 0;
        for (int i = 0; i < ball_count; i++)
        {
            yield return new WaitForSeconds(1f);

            Animator ballAnimator = balls[i].GetComponent<Animator>();

         
            if (ballAnimator != null)
            {
                SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Pop);

                ballAnimator.SetTrigger("Pop");
             

            }
            else
            {
                Debug.Log("ball Animator Not Found");
            }

            yield return new WaitForSeconds(1.0f);
            Destroy(balls[i]);

            /*

            Animator popAnimator = pop_object.GetComponent<Animator>();
            if (popAnimator != null)
            {
                Debug.Log("Pop Animator Runtime Controller: " + popAnimator.runtimeAnimatorController.name);
                popAnimator.Play("Scratching_explode"); // scratching_explode 애니메이션 재생
            }

            yield return new WaitForSeconds(popAnimator.GetCurrentAnimatorStateInfo(0).length);
            */

            //Destroy(pop_object);
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
        BossManagerNew.Current.OnSkillEnd?.Invoke();

    }


}
    
     
   



