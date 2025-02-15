using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNeuron : MonoBehaviour
{
    [SerializeField] Transform Pos;
    
    public int hitCount = 0;
    private float curTime;
    private float coolTime = 0.8f;
    [SerializeField] private Vector2 BoxSize;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, BoxSize, 0);

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.CompareTag("Neuron"))
                    {
                        Debug.Log("Neuron hit!");
                        TakeDamage(5);
                        curTime = coolTime;

                    }


                }
            }
        }
        else {
            curTime -= Time.deltaTime;
        }
    }


       

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(Pos.position, BoxSize);
        }



        public void TakeDamage(float damage)
        {
        ++hitCount;
        Debug.Log("Damage taken: " + damage);

            //3번째일때는 -10 이고 count 0으로 초기화
            if (hitCount == 3)
            {
                Debug.Log("hitCount: " + "3");
                BossManagerNew.Current.OnIncreaseHackingPoint(10);


            }
            else
            {

                BossManagerNew.Current.OnIncreaseHackingPoint(damage);
                
                Debug.Log("hitCount: " + hitCount);
            }

        }
    }

