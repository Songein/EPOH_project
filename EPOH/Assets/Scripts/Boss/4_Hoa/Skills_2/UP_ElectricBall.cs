using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UP_ElectricBall : MonoBehaviour
{
    //  private BossDogController dog; 
   // public GameObject player; // 플레이어 게임 오브젝트
    private List<GameObject> balls;
   //[SerializeField] private Vector3 leftEdgePoint;
  //  [SerializeField] private Vector3 rightEdgePoint;
    [SerializeField] private int ball_count;


    //ball 변수
    [SerializeField] private GameObject ball_prefab; // ball 프리팹
    [SerializeField] private GameObject ball_Nprefab; // ball 프리팹
    //[SerializeField] private GameObject ball_pop; // 폭발 프리팹


    public void Activate()
    {
        electricBall();
        StartCoroutine(electric_pop());
    }


    

    public void electricBall()
    {
        BossData bossData = BossManagerNew.Current.bossData;
        Vector3 leftEdge = new Vector3(bossData._leftBottom.x + 5, bossData._leftBottom.y +5, 0)  ;
        Vector3 rightEdge = new Vector3(bossData._rightTop.x - 5, bossData._rightTop.y - 5, 0);
         // 총 10의 electricball 을 생성
        int number = 0;
        balls = new List<GameObject>();
        for (int i = 0; i < ball_count -4; i++)
        {
        
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
                

                // Animator 컴포넌트를 가져와 scratch_scar 애니메이션 재생
                Animator ballAnimator = ball_object.GetComponent<Animator>();
                if (ballAnimator != null)
                {
                    Debug.Log("ball Animator Runtime Controller: " + ballAnimator.runtimeAnimatorController.name);
                    ballAnimator.Play("ElectricBall_N");
                }
                else
                {
                    Debug.Log("ball Animator Not Found");
                }
                

                // Destroy(ball_object);
                number++;
                Debug.Log("ball 횟수: " + number);


            }
        }
    }



    public IEnumerator  electric_pop()
    {
        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < ball_count - 4; i++)
        {//기본 볼 한꺼번에 삭제



            Animator ballAnimator = balls[i].GetComponent<Animator>();


            if (ballAnimator != null)
            {
                Debug.Log("ball Animator Found");
                ballAnimator.SetTrigger("Pop");
            }
            else
            {
                Debug.Log("ball Animator Not Found");
            }


        }
        Invoke("DestroyBalls", 1.0f);
        yield return new WaitForSeconds(1.2f);

        for (int i = ball_count - 4; i < ball_count; i++)
            { //Nball, Popball 삭제
            int number = 0;
            Destroy(balls[i]);
            number++;
            Debug.Log("pop 횟수: " + number);

        }
        
        BossManagerNew.Current.OnSkillEnd?.Invoke();

    }

    public void DestroyBalls() {
        for (int i = 0; i < ball_count - 4; i++)
        {
        Destroy(balls[i]); // 기존 ballprefab 삭제
        }
    }
    
}
