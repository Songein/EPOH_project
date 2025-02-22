using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_Half : MonoBehaviour, BossSkillInterface
{
    [SerializeField] GameObject hoaPrefab;
    [SerializeField] GameObject lightwarningPrefab;
    [SerializeField] GameObject lightPrefab;
    private Vector3[] positionSide = new Vector3[2];

    public void Activate()
    {
        BossData bossData = BossManagerNew.Current.bossData;

        StartCoroutine(ArmComingUp(bossData));
    }
    
    IEnumerator ArmComingUp(BossData bossData)
    {
      
        Vector3 hoaPostion = new Vector3((bossData._leftBottom.x + bossData._rightTop.x) / 2, (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0);
        Vector3 lightleftPostion = new Vector3(((bossData._leftBottom.x + bossData._rightTop.x) / 2 + bossData._leftBottom.x) / 2, (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0); //x: 중간지점과 left끝의 중간지점에 생성
        Vector3 lightrightPostion = new Vector3(((bossData._leftBottom.x + bossData._rightTop.x) / 2 + bossData._rightTop.x) / 2, (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0); //x: 중간지점과 right끝의 중간지점에 생성
        this.positionSide[0] = lightleftPostion;
        this.positionSide[1] = lightrightPostion;
        
        GameObject hoa = Instantiate(hoaPrefab, hoaPostion, Quaternion.identity);
        yield return new WaitForSeconds(0.5f); //여기에 애니메이션 추가
        int index = Random.Range(0, 2); //배열에 left,right 넣고 left =0, right =1 후에 1or0 불러오기
        GameObject lightWarning = Instantiate(lightwarningPrefab, positionSide[index], Quaternion.identity);
        yield return new WaitForSeconds(2f); //3초간 빛난 뒤 light 생성
        StartCoroutine(OtherArmComingUp(bossData, index, hoa));
        yield return new WaitForSeconds(1f);
        Destroy(lightWarning);
        GameObject light = Instantiate(lightPrefab, positionSide[index], Quaternion.identity);
        yield return new WaitForSeconds(1.0f);//light나온 뒤 1.5초 뒤에 light 삭제(애니메이션 추가)
        Destroy(light);
        //yield return new WaitForSeconds(2f);

        //StartCoroutine(OtherArmComingUp(bossData, index, hoa));




    }

    IEnumerator OtherArmComingUp(BossData bossData, int index, GameObject hoa)
    {
        
        Vector3 hoaPostion = new Vector3((bossData._leftBottom.x + bossData._rightTop.x) / 2, (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0);
        Vector3 lightleftPostion = new Vector3(((bossData._leftBottom.x + bossData._rightTop.x) / 2 + bossData._leftBottom.x) / 2, 
            (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0); //x: 중간지점과 left끝의 중간지점에 생성
        Vector3 lightrightPostion = new Vector3(((bossData._leftBottom.x + bossData._rightTop.x) / 2 + bossData._rightTop.x) / 2, 
            (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0); //x: 중간지점과 right끝의 중간지점에 생성
        Vector3 spawnPosition = positionSide[1 - index];
        GameObject lightWarning = Instantiate(lightwarningPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(3f); //3초간 빛난 뒤 light 생성
        Destroy(lightWarning);
        GameObject light = Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);//light나온 뒤 1.0초 뒤에 light 삭제(애니메이션 추가)
        Destroy(light);
        Destroy(hoa);
        BossManagerNew.Current.OnSkillEnd?.Invoke();




    }
}
