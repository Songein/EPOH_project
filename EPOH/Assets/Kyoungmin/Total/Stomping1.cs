using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomping1 : MonoBehaviour, BossSkillInterface
{
    //BossDogController 참조
    private BossDogController _dog;
    
    //Stomping1 스킬 변수
    [SerializeField] private float facingTime = 0.5f; //플레이어를 바라보는 시간
    [SerializeField] private float precursorTime = 2f; //발구르기 일반 공격 전조 시간

    private void Awake()
    {
        _dog = GetComponent<BossDogController>();
    }
    
    public void Activate()
    {
        StartCoroutine(StartStomping1());
    }
    
    IEnumerator StartStomping1()
    {
        //보스 리스트 초기화
        _dog.bossList.Clear();
        
        //개 그림자가 플레이어 위치에 생성
        Vector2 playerPos = _dog._player.transform.position;
        _dog.GenerateDog(playerPos);
        
        //facingTime 이후 플레이어가 위치한 방향을 바라본다
        yield return new WaitForSeconds(facingTime);
        
        //stomping wave 오브젝트 활성화
        GameObject stompingWave = _dog.bossList[0].transform.GetChild(2).gameObject;
        yield return new WaitForSeconds(precursorTime);
        
        //플레이어 위치에 따라 보스가 바라보는 방향 변경 및 왼/오 wave 활성화
        stompingWave.SetActive(true);
        stompingWave.GetComponent<Animator>()
            .Play(_dog.IsPlayerRight(_dog.bossList[0]) ? "Right Explosion" : "Left Explosion");
    }
}
