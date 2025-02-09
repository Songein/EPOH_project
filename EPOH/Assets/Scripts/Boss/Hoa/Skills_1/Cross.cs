using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private Vector3[] hoaArmPositions;
    [SerializeField] private GameObject CrossWarningArms; //1번은 왼쪽 위 2번은 오른쪽 위 3번은 왼쪽 아래 4번은 오른쪽 아래
    [SerializeField] private GameObject CrossArms;

    public void Activate() {
        BossData bossData = BossManagerNew.Current.bossData;
        StartCoroutine(LightMoving(bossData));
    }

    IEnumerator LightMoving(BossData bossData)
    {
        int plusormultiple = Random.Range(0, 2);
        Vector3 leftBottom = bossData._leftBottom;
        Vector3 rightBottom = new Vector3(bossData._rightTop.x, bossData._leftBottom.y, 0);

        if (plusormultiple == 0) //십자일때
        {
            int Randomnum = Random.Range(0, 4);

            if (Randomnum == 0 || Randomnum == 2)
            {
                Vector3 adjustPosition = new Vector3(hoaArmPositions[Randomnum].x + 10, hoaArmPositions[Randomnum].y, 0); //왼쪽 position point들의 prefab 조정
                Instantiate(CrossWarningArms, hoaArmPositions[Randomnum], Quaternion.identity);  //세로
                Instantiate(CrossWarningArms, adjustPosition, Quaternion.Euler(0, 0, 90)); //가로

                yield return new WaitForSeconds(3.0f); //3초동안 경고
                Instantiate(CrossArms, hoaArmPositions[Randomnum], Quaternion.identity);  //세로
                Instantiate(CrossArms, adjustPosition, Quaternion.Euler(0, 0, 90)); //가로
            }
            else
            {
                Vector3 adjustPosition = new Vector3(hoaArmPositions[Randomnum].x - 10, hoaArmPositions[Randomnum].y, 0); //오른쪽 position point들의 prefab 조정
                Instantiate(CrossWarningArms, hoaArmPositions[Randomnum], Quaternion.identity);  //세로
                Instantiate(CrossWarningArms, adjustPosition, Quaternion.Euler(0, 0, 90)); //가로

                yield return new WaitForSeconds(3.0f); //3초동안 경고
                Instantiate(CrossArms, hoaArmPositions[Randomnum], Quaternion.identity);  //세로
                Instantiate(CrossArms, adjustPosition, Quaternion.Euler(0, 0, 90)); //가로
            }
        }

        else
        {
            int Randomnum = Random.Range(0, 4);
           
            // 오른쪽 대각선 생성
            Instantiate(CrossWarningArms, hoaArmPositions[Randomnum], Quaternion.Euler(0, 0, 80));
           

            // 왼쪽 대각선 생성
           Instantiate(CrossWarningArms, hoaArmPositions[Randomnum], Quaternion.Euler(0, 0, 170));
            
            
            yield return new WaitForSeconds(3.0f); // 3초 동안 경고

            // 오른쪽 대각선 생성 (최종)
           Instantiate(CrossArms, hoaArmPositions[Randomnum], Quaternion.Euler(0, 0, 80));
           

            // 왼쪽 대각선 생성 (최종)
            Instantiate(CrossArms, hoaArmPositions[Randomnum], Quaternion.Euler(0, 0, 170));
           
            yield return new WaitForSeconds(1.5f);  // 추가적인 대기 시간
            

        }

    }

}
