using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronHack : MonoBehaviour
{ private int hitCount = 0;
   

  //  private float timeInTrigger = 0f;
  //  private float triggerDelay = 0.7f;
   // private float triggerDelay2 = 0.9f;
    private bool movingUp = true;
    private Vector3 startPos;
   private Vector3 targetPos;
    private SpriteRenderer sr;
    private HackingForN hacking;


    private void Awake()
    {
        hacking = FindObjectOfType<HackingForN>();
        BossData bossdata = BossManagerNew.Current.bossData;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = bossdata.Nueron;
    }
    void Start()
      {
       
        startPos = transform.position;
          targetPos = startPos + new Vector3(0, 0.00001f, 0); //y축으로 1만큼 이동
        StartCoroutine(MoveLoop());
      }
    private void Update()
    {
        if (hacking._hackingPoint >= hacking.hackingGoal) {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("AttackArea"))
        {
            TakeDamage(5); 
          
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

    
    IEnumerator MoveLoop()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 from = movingUp ? startPos : targetPos;
            Vector3 to = movingUp ? targetPos : startPos;

            while (elapsedTime < 1)
            {
                transform.position = Vector3.Lerp(from, to, elapsedTime / 1);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = to;
            movingUp = !movingUp;
            yield return null;
        }
    }
    


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
