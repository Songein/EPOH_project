using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronHack : MonoBehaviour
{   private int hitCount = 0;
    private Transform playerTransform;
    private Transform attackArea1;
    private Transform attackArea2;
    private float timeInTrigger = 0f;
    private float triggerDelay = 0.7f;
    private float triggerDelay2 = 0.9f;
 
   
  

    public void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        //player의 자식
         attackArea1 = playerTransform.Find("AttackArea/Attack1Area");
          attackArea2 = playerTransform.Find("AttackArea/Attack2Area");
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == attackArea1.gameObject)
        {
            Debug.Log("1 triggerEnter");
            if (PlayerAttack.instance != null && PlayerAttack.instance.is_attacking)
            {
                TakeDamage(5);  // 데미지를 주는 로직
            }
        }
        if (collision.gameObject == attackArea2.gameObject) {
            Debug.Log("2 triggerEnter");
            // PlayerAttack 싱글톤을 사용하여 공격 여부 확인
            if (PlayerAttack.instance != null && PlayerAttack.instance.is_attacking)
            {
                TakeDamage(5);  // 데미지를 주는 로직
            }
        }

    }


    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        timeInTrigger += Time.deltaTime;
        Debug.Log(timeInTrigger);

        if (timeInTrigger >= triggerDelay)
            {
                if (collision.gameObject == attackArea1.gameObject)
                {
                    Debug.Log("1 triggerStay ");
                    TakeDamage(5);
                    timeInTrigger = 0f;
                }

                if (collision.gameObject == attackArea2.gameObject)
                {
                    {
                        Debug.Log("2 triggerStay");
                        TakeDamage(5);
                        timeInTrigger = 0f;

                    }

                }
            }

            if (timeInTrigger >= triggerDelay2)
            {
                if (collision.gameObject == attackArea2.gameObject)
                {
                    {
                        Debug.Log("2 triggerStay");
                        TakeDamage(5);
                        timeInTrigger = 0f;

                    }

                }
            }
        
        
        

    }
    */
    

    public void TakeDamage(float damage)
    {
       
      
        //3번째일때는 -10 이고 count 0으로 초기화
        if (hitCount == 2)
        {
            Debug.Log("hitCount: " + "3");
            BossManagerNew.Current.OnIncreaseHackingPoint(10);
            Destroy(gameObject);
            hitCount = 0;


        }
        else
        {
           
            BossManagerNew.Current.OnIncreaseHackingPoint(damage);
            hitCount++;
            Debug.Log("hitCount: " + hitCount);
        }

    }
   
}
