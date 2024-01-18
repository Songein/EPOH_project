using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Bite : MonoBehaviour
{
    private bool is_attacking = false; //보스가 현재 공격 중인지(임시 변수)
    private GameObject player; //플레이어 위치
    [SerializeField] float reach_distance = 6f; //공격 사정 거리
    private float cur_distance; //현재 플레이어와 보스 간 거리
    [SerializeField] float precursor_time = 1.5f; //전조 시간
    [SerializeField] float cool_time = 5f; //물기 공격 후 쿨타임(임시 변수)

    public GameObject bite_area; //bite 공격 범위 오브젝트

    public AnimationCurve curve; //포물선 이동을 위한 AnimationCurve 선언
    [SerializeField] float duration = 0.5f; //포물선 이동에 걸리는 시간
    
    void Start()
    {
        //플레이어 오브젝트 할당
        player = FindObjectOfType<PlayerController>().gameObject;
        bite_area.SetActive(false);
    }

    void Update()
    {
        //특정 거리 내에 플레이어가 있고 공격 중이지 않을 경우
        if (CheckDistance() && !is_attacking)
        {
            //Bite 코루틴 호출
            StartCoroutine(StartBite());
        }
        
    }
    
    //특정 거리 내에 플레이어가 있는지 체크
    bool CheckDistance()
    {
        //현재 플레이어와 보스 간 거리 측정
        cur_distance = Vector2.Distance(player.transform.position, this.transform.position);
        //Debug.Log("[Bite] 거리 : " + cur_distance);
        
        //현재 플레이어가 사정 거리 내에 있는지에 따라 bool 값 리턴
        if (cur_distance <= reach_distance)
        {
            //Debug.Log("[Bite] : 사정거리 내에 있음.");
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator StartBite()
    {
        Debug.Log("[Bite] : 보스가 이빨을 드러내며 그르렁 거림.");
        is_attacking = true;
        yield return new WaitForSeconds(precursor_time);
        
        //플레이어의 현재 위치 파악
        Vector2 player_pos = player.transform.position;
        //보스의 시작 위치 파악
        Vector2 start = transform.position;
        float bite_distance = 0f; //보스가 이동할 거리
        
        //transform.position.x - player_pos.x 값이 양수면 플레이어는 보스의 왼쪽에 위치함.
        //반대로, 음수면 플레이어는 보스의 오른쪽에 위치함을 알 수 있음.
        if (transform.position.x - player_pos.x >= 0f)
        {
            //포물선 이동을 왼쪽으로 사정거리만큼 하도록 설정
            bite_distance = -1 * reach_distance;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(false);
        }
        else
        {
            //포물선 이동을 오른쪽으로 사정거리만큼 하도록 설정
            bite_distance = 1 * reach_distance;
            //공격 범위를 플레이어를 보는 방향으로 설정
            bite_area.GetComponent<BiteArea>().SetPos(true);
        }
        
        //보스의 도착지점 위치 지정
        Vector2 end = new Vector2(transform.position.x + bite_distance, transform.position.y);
        //bite area 오브젝트 활성화(공격 범위 활성화)
        bite_area.SetActive(true); 
        
        //해당 위치로 아치형을 그리며 돌진
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);
            float height = Mathf.Lerp(0.0f, 10f, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Debug.Log("[Bite] : bite area 비활성화");
        bite_area.SetActive(false);
        
        yield return new WaitForSeconds(cool_time);
        Debug.Log("[Bite] : 쿨타임 끝");
        is_attacking = false;
    }
}
