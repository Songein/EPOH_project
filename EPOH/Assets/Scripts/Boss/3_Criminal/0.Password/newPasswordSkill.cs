using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPasswordSkill : MonoBehaviour, BossSkillInterface
{
    public GameObject BluePasswordObject;
    public GameObject YellowPasswordObject;
    public Sprite[] passwordSprites;

    private Password passwordScript;
    public void Activate()
    {
        GetthePasswords();
    }


    public void GetthePasswords() {
       
       
        int ranNum = Random.Range(0, 2);
        if (ranNum == 0)
        { //Cat -> Cut 일때
            float _y = Random.Range(BossManagerNew.Current.bossData._leftBottom.y,
                   BossManagerNew.Current.bossData._rightTop.y);
            for (int i = 0; i < 3; i++)
            {
                float _x = Mathf.Lerp(BossManagerNew.Current.bossData._leftBottom.x +4 , BossManagerNew.Current.bossData._rightTop.x -4 , (float) i / 2);
                //float _x = Random.Range(BossManagerNew.Current.bossData._leftBottom.x,
          // BossManagerNew.Current.bossData._rightTop.x);
               
                Vector2 newPos = new Vector2(_x, _y);

                if (i == 1)
                {
                    GameObject PasswordY = Instantiate(YellowPasswordObject, newPos, Quaternion.identity);
                    SpriteRenderer sr = PasswordY.GetComponent<SpriteRenderer>();
                    sr.sprite = passwordSprites[i];
                    passwordScript = PasswordY.GetComponent<Password>();
                    passwordScript._changedSprite = passwordSprites[3];
                    /*
                    GameObject PasswordY = Instantiate(YellowPasswordObject, newPos, Quaternion.identity);
                    Passwords.Add(PasswordY);
                    passwordScript = Passwords[1].GetComponent<Password>();
                    passwordScript._changedSprite = passwordSprites[4];
                    */


                }

                else
                {
                    
                   GameObject PasswordB = Instantiate(BluePasswordObject, newPos, Quaternion.identity);
                   SpriteRenderer sr = PasswordB.GetComponent<SpriteRenderer>();
                   sr.sprite = passwordSprites[i];
                   /*
                   GameObject PasswordB = Instantiate(BluePasswordObject, newPos, Quaternion.identity);
                   Passwords.Add(PasswordB);
                   SpriteRenderer sr = Passwords[i].GetComponent<SpriteRenderer>();
                   sr.sprite = passwordSprites[i];
                   */
                }
            }
        }

        else if (ranNum == 1)
        {
            float _y = Random.Range(BossManagerNew.Current.bossData._leftBottom.y,
                    BossManagerNew.Current.bossData._rightTop.y);
            for (int i = 4; i < 8; i++)
            {
                //float _x = Random.Range(BossManagerNew.Current.bossData._leftBottom.x,
                //BossManagerNew.Current.bossData._rightTop.x);
                float _x = Mathf.Lerp(BossManagerNew.Current.bossData._leftBottom.x+2 , BossManagerNew.Current.bossData._rightTop.x -2, (float)(i -4) / 3);
                Vector2 newPos = new Vector2(_x, _y);
                if (i == 4)
                {
                    GameObject PasswordY = Instantiate(YellowPasswordObject, newPos, Quaternion.identity);
                    SpriteRenderer sr = PasswordY.GetComponent<SpriteRenderer>(); 
                    sr.sprite = passwordSprites[i];
                    passwordScript = PasswordY.GetComponent<Password>();
                    passwordScript._changedSprite = passwordSprites[8];
                    /*
                    GameObject PasswordY = Instantiate(YellowPasswordObject, newPos, Quaternion.identity);
                    Passwords.Add(PasswordY);
                    passwordScript = Passwords[0].GetComponent<Password>();
                    passwordScript._changedSprite = passwordSprites[8];
                    */


                }
                else
                {
                    GameObject PasswordB = Instantiate(BluePasswordObject, newPos, Quaternion.identity);
                    SpriteRenderer sr = PasswordB.GetComponent<SpriteRenderer>(); 
                    sr.sprite = passwordSprites[i];
                    /*
                    GameObject PasswordB = Instantiate(BluePasswordObject, newPos, Quaternion.identity);
                    Passwords.Add(PasswordB);
                    SpriteRenderer sr = Passwords[i - 4].GetComponent<SpriteRenderer>(); // I-4 주의
                    sr.sprite = passwordSprites[i];
                    */
                }
              

            }



        }

        }

    }


