using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Half : MonoBehaviour, BossSkillInterface
{
    [SerializeField] GameObject hoaPrefab;
    [SerializeField] GameObject lightwarningPrefab;
    [SerializeField] GameObject lightPrefab;

    
    public void Activate() {
        BossData bossData = BossManagerNew.Current.bossData;

        StartCoroutine(ArmComingUp(bossData));
    }

    IEnumerator ArmComingUp(BossData bossData) {
        Vector3 hoaPostion = new Vector3((bossData._leftBottom.x + bossData._rightTop.x) / 2, (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0);
        Vector3 lightleftPostion = new Vector3(((bossData._leftBottom.x + bossData._rightTop.x) / 2 + bossData._leftBottom.x)/2, (bossData._leftBottom.y + bossData._rightTop.y)/2, 0); //x: 중간지점과 left끝의 중간지점에 생성
        Vector3 lightrightPostion = new Vector3(((bossData._leftBottom.x + bossData._rightTop.x) / 2 + bossData._rightTop.x) / 2, (bossData._leftBottom.y + bossData._rightTop.y) / 2, 0); //x: 중간지점과 right끝의 중간지점에 생성
        GameObject hoa = Instantiate(hoaPrefab, hoaPostion, Quaternion.identity);
        yield return new WaitForSeconds(0.5f); //여기에 애니메이션 추가
        Vector3 spawnPosition = Random.Range(0, 2) == 0 ? lightleftPostion : lightrightPostion;
        GameObject lightWarning = Instantiate(lightwarningPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(3f); //3초간 빛난 뒤 light 생성
        Destroy(lightWarning);
        GameObject light= Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);//light나온 뒤 1.5초 뒤에 light 삭제(애니메이션 추가)
        Destroy(light);
        Destroy(hoa);
        BossManagerNew.Current.OnSkillEnd?.Invoke();



    }
}
