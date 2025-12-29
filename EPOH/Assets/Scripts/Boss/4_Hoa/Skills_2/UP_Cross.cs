using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_Cross : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private Vector3[] hoaArmPositions;
    [SerializeField] private GameObject CrossWarningArms; //1번은 왼쪽 위 2번은 오른쪽 위 3번은 왼쪽 아래 4번은 오른쪽 아래
    [SerializeField] private GameObject CrossArms;

    public void Activate()
    {
        BossData bossData = BossManagerNew.Current.bossData;
        StartCoroutine(LightMoving3(bossData));
    }

    IEnumerator LightMoving3(BossData bossData)
    {
        int plusormultiple = Random.Range(0, 2);
        Vector3 leftBottom = bossData._leftBottom;
        Vector3 rightBottom = new Vector3(bossData._rightTop.x, bossData._leftBottom.y, 0);

        if (plusormultiple == 0) //십자일때
        {
            int Randomnum = Random.Range(0, 4);

            if (Randomnum == 0 || Randomnum == 2)
            {
                Vector3 adjustPosition1 = new Vector3(hoaArmPositions[Randomnum].x + 80, hoaArmPositions[Randomnum].y, 0); //왼쪽 position point들의 prefab 조정
                Vector3 adjustPosition2 = new Vector3(hoaArmPositions[Randomnum].x, hoaArmPositions[Randomnum].y + 20, 0); //왼쪽 position point들의 prefab 조정
                Instantiate(CrossWarningArms, adjustPosition1, Quaternion.identity);  //세로
                Instantiate(CrossWarningArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //가로
                yield return new WaitForSeconds(2.0f);
                StartCoroutine(LightMoving2(bossData, Randomnum));
                yield return new WaitForSeconds(0.7f);
                SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Electric); //소리
                yield return new WaitForSeconds(0.3f);  //3초동안 경고
                Instantiate(CrossArms, adjustPosition1, Quaternion.identity);  //가로
                Instantiate(CrossArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //세로

            }
            else
            {
                Vector3 adjustPosition1 = new Vector3(hoaArmPositions[Randomnum].x + 40, hoaArmPositions[Randomnum].y, 0); //왼쪽 position point들의 prefab 조정
                Vector3 adjustPosition2 = new Vector3(hoaArmPositions[Randomnum].x, hoaArmPositions[Randomnum].y + 20, 0); //오른쪽 position point들의 prefab 조정
                Instantiate(CrossWarningArms, adjustPosition1, Quaternion.identity);  //세로
                Instantiate(CrossWarningArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //가로

                yield return new WaitForSeconds(2.0f);
                StartCoroutine(LightMoving2(bossData, Randomnum));
                yield return new WaitForSeconds(0.7f);
                SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Electric); //소리
                yield return new WaitForSeconds(0.3f);  //3초동안 경고
                Instantiate(CrossArms, adjustPosition1, Quaternion.identity);  //가로
                Instantiate(CrossArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //세로
            }
        }

        else
        {
            int Randomnum = Random.Range(0, 4);
            Vector3 adjustPosition1 = new Vector3(hoaArmPositions[Randomnum].x, hoaArmPositions[Randomnum].y + 20, 0); // 세로
            Vector3 adjustPosition2 = new Vector3(hoaArmPositions[Randomnum].x - 50, hoaArmPositions[Randomnum].y + 8.25f, 0); //가로

            // 오른쪽 대각선 생성
            Instantiate(CrossWarningArms, adjustPosition1, Quaternion.Euler(0, 0, 80));


            // 왼쪽 대각선 생성
            Instantiate(CrossWarningArms, adjustPosition2, Quaternion.Euler(0, 0, 170));



            yield return new WaitForSeconds(2.0f);
            StartCoroutine(LightMoving2(bossData, Randomnum));
            yield return new WaitForSeconds(0.7f);
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Electric); //소리
            yield return new WaitForSeconds(0.3f);  //3초동안 경고

            // 오른쪽 대각선 생성 (최종)
            Instantiate(CrossArms, adjustPosition1, Quaternion.Euler(0, 0, 80));


            // 왼쪽 대각선 생성 (최종)
            Instantiate(CrossArms, adjustPosition2, Quaternion.Euler(0, 0, 170));

            //yield return new WaitForSeconds(1.5f);  // 추가적인 대기 시간


        }

    }


    IEnumerator LightMoving2(BossData bossData, int Randomnum3)
    {
        int plusormultiple = Random.Range(0, 2);
        Vector3 leftBottom = bossData._leftBottom;
        Vector3 rightBottom = new Vector3(bossData._rightTop.x, bossData._leftBottom.y, 0);

        if (plusormultiple == 0) //십자일때
        {
            int Randomnum = Random.Range(0, 4);
            while (Randomnum == Randomnum3)
            {
                Randomnum = Random.Range(0, 4);
            }

                if (Randomnum == 0 || Randomnum == 2)
                {
                Vector3 adjustPosition1 = new Vector3(hoaArmPositions[Randomnum].x + 80, hoaArmPositions[Randomnum].y, 0); //왼쪽 position point들의 prefab 조정
                Vector3 adjustPosition2 = new Vector3(hoaArmPositions[Randomnum].x, hoaArmPositions[Randomnum].y + 20, 0); //왼쪽 position point들의 prefab 조정
                Instantiate(CrossWarningArms, adjustPosition1, Quaternion.identity);  //세로
                Instantiate(CrossWarningArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //가로

                yield return new WaitForSeconds(2.7f);
                SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Electric); //소리
                yield return new WaitForSeconds(0.3f);  //3초동안 경고
                Instantiate(CrossArms, adjustPosition1, Quaternion.identity);  //가로
                Instantiate(CrossArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //세로

                yield return new WaitForSeconds(2.5f);
                BossManagerNew.Current.OnSkillEnd?.Invoke();
            }
                else
                {
                Vector3 adjustPosition1 = new Vector3(hoaArmPositions[Randomnum].x + 40, hoaArmPositions[Randomnum].y, 0); //왼쪽 position point들의 prefab 조정
                Vector3 adjustPosition2 = new Vector3(hoaArmPositions[Randomnum].x, hoaArmPositions[Randomnum].y + 20, 0); //오른쪽 position point들의 prefab 조정
                Instantiate(CrossWarningArms, adjustPosition1, Quaternion.identity);  //세로
                Instantiate(CrossWarningArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //가로


                yield return new WaitForSeconds(2.7f);
                SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Electric); //소리
                yield return new WaitForSeconds(0.3f);  //3초동안 경고

                Instantiate(CrossArms, adjustPosition1, Quaternion.identity);  //가로
                Instantiate(CrossArms, adjustPosition2, Quaternion.Euler(0, 0, 90)); //세로
                yield return new WaitForSeconds(2.5f);
                BossManagerNew.Current.OnSkillEnd?.Invoke();
            }
            }

            else
            {
            int Randomnum = Random.Range(0, 4);
            while (Randomnum == Randomnum3)
            {
                Randomnum = Random.Range(0, 4);
            }

            Vector3 adjustPosition1 = new Vector3(hoaArmPositions[Randomnum].x, hoaArmPositions[Randomnum].y + 20, 0); // 세로
            Vector3 adjustPosition2 = new Vector3(hoaArmPositions[Randomnum].x - 50, hoaArmPositions[Randomnum].y + 8.25f, 0); //가로


            // 오른쪽 대각선 생성
            Instantiate(CrossWarningArms, adjustPosition1, Quaternion.Euler(0, 0, 80));


            // 왼쪽 대각선 생성
            Instantiate(CrossWarningArms, adjustPosition2, Quaternion.Euler(0, 0, 170));



            yield return new WaitForSeconds(2.7f);
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Electric); //소리
            yield return new WaitForSeconds(0.3f);  //3초동안 경고


            // 오른쪽 대각선 생성 (최종)
            Instantiate(CrossArms, adjustPosition1, Quaternion.Euler(0, 0, 80));


            // 왼쪽 대각선 생성 (최종)
            Instantiate(CrossArms, adjustPosition2, Quaternion.Euler(0, 0, 170));


            yield return new WaitForSeconds(2.5f);  // 추가적인 대기 시간
            BossManagerNew.Current.OnSkillEnd?.Invoke();


        }
        
        

    }

}
