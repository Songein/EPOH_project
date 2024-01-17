using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howling : MonoBehaviour
{
    //[SerializeField] float power = 10f; //하울링 공격 세기
    private bool is_attacking = false; //보스가 현재 공격 중인지(임시 변수)
    private GameObject player; //플레이어 위치
    [SerializeField] float reach_distance = 5f; //공격 사정 거리
    private float cur_distance; //현재 플레이어와 보스 간 거리
    [SerializeField] float precursor_time = 3f; //전조 시간
    [SerializeField] float cool_time = 5f; //하울링 공격 후 쿨타임(임시 변수)
    public GameObject ShockWave; //충격파 오브젝트
    
    
    void Start()
    {
        //플레이어 오브젝트 할당
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    
    void Update()
    {
        //특정 거리 내에 플레이어가 있는지 체크 및 공격 중이지 않을 경우
        //해당 조건을 만족한다면 하울링 코루틴 호출
        if (CheckDistance() && !is_attacking)
        {
            StartCoroutine(Howl());
        }
        
    }
    
    //특정 거리 내에 플레이어가 있는지 체크
    bool CheckDistance()
    {
        //현재 플레이어와 보스 간 거리 측정
        cur_distance = Vector2.Distance(player.transform.position, this.transform.position);
        //Debug.Log("[Howling] 거리 : " + cur_distance);
        
        //현재 플레이어가 사정 거리 내에 있는지에 따라 bool 값 리턴
        if (cur_distance <= reach_distance)
        {
            Debug.Log("[Howling.cs] : 사정거리 내에 있음.");
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator Howl()
    {
        Debug.Log("[Howling.cs] : 보스가 멈추고 고개를 든다.");
        is_attacking = true; //공격 중으로 변경
        yield return new WaitForSeconds(precursor_time); //전조 시간만큼 대기
        
        //전조시간 후 실제 공격 전 다시 한번 플레이어가 사정 거리 내에 있는지 확인
        if (CheckDistance())
        {
            //충격파 오브젝트 활성화
            ShockWave.SetActive(true);
            //충격파 오브젝트 커지도록
            for (int i = 1; i <= 19; i++)
            {
                //아래 시간을 조정함에 따라 충격파 발동 시간이 조절됨
                yield return new WaitForSeconds(0.01f);
                ShockWave.transform.localScale = new Vector2(1f + 0.5f*i, 1f + 0.5f*i);
            }
            yield return new WaitForSeconds(0.01f);
            ShockWave.SetActive(false); //충격파 오브젝트 비활성화
            //충격파 오브젝트 크기 원래대로 초기화
            ShockWave.transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            Debug.Log("[Howling.cs] : 사정거리 벗어남에 따라 공격 중지");
            //전조 행동 후 실제 공격 전 플레이어가 사정 거리 내에 위치하지 않는다면 공격 해제
            is_attacking = false;
            yield break; //공격을 수행하지 않았기에 쿨타임 없이 바로 함수 끝
        }
        //본공격을 수행했을 경우에만 실행되며 쿨타임을 임시로 추가함(협의 필요).ㄴ
        yield return new WaitForSeconds(cool_time);
        Debug.Log("[Howling.cs] : s쿨타임 끝");
        is_attacking = false; //공격 중 해제
    }
}
