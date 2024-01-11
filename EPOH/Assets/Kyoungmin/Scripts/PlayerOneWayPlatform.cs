using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject current_platform; //현재 밟고 있는 oneWayPlatform
    private CapsuleCollider2D player_collider; //플레이어의 콜라이더 할당
    
    void Start()
    {
        //플레이어 캡슐 콜라이더 할당하기
        player_collider = GetComponent<CapsuleCollider2D>();
    }
    
    void Update()
    {
        //아래 키를 누르는 동안에(버튼 간 입력 순서가 정해져있기에)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //점프 버튼을 눌렀고 현재 밟고 있는 발판이 null이 아니라면
            if (Input.GetButtonDown("Jump") && (current_platform != null))
            {
                StartCoroutine(DisableCollision());
            }
        }
        
    }

    //발판과 충돌을 시작한 순간
    void OnCollisionEnter2D(Collision2D other)
    {
        //충돌한 물체의 태그가 OneWayPlatform이라면
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            //현재 밟고 있는 플랫폼이라고 나타내기
            current_platform = other.gameObject;
        }
    }

    //발판과 충돌이 끝난 순간
    void OnCollisionExit2D(Collision2D other)
    {
        //충돌한 물체의 태그가 OneWayPlatform이라면
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            //현재 밟고 있는 발판에 Null 값 대입하기
            current_platform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        //발판의 콜라이더 가져오기
        BoxCollider2D platform_collider = current_platform.GetComponent<BoxCollider2D>();
        //플레이어 콜라이더와 발판 콜라이더 간 충돌을 무시하도록 설정
        Physics2D.IgnoreCollision(player_collider,platform_collider);
        
        yield return new WaitForSeconds(3f);
        //플레이어 콜라이더와 발판 콜라이더 간 충돌을 다시 활성화
        Physics2D.IgnoreCollision(player_collider,platform_collider,false);
    }
}
