using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //수평 값
    private float horizontal;
    //플레이어 이동 속도
    public float playerSpeed = 8f;
    //플레이어 점프 힘
    public float playerJumpForce = 10f;
    //플레이어 리지드바디 컴포넌트
    private Rigidbody2D playerRigidbody;
    //플레이어 애니메이터
    private Animator playerAnimator;
    //플레이어 flip 관련 변수(오른 쪽을 쳐다보고 있는지)
    private bool isFacingRight = true;
    //스프라이트 렌더러 컴포넌트
    private SpriteRenderer sr;
   
    void Start()
    {
        //게임 오브젝트로부터 Rigidbody2D 컴포넌트를 가져와서 할당하기
        playerRigidbody = GetComponent<Rigidbody2D>();
        //게임 오브젝트로부터 Animator 컴포넌트 가져와서 할당하기
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        //점프 버튼을 누르면
        if (Input.GetButtonDown("Jump"))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerJumpForce);
            playerAnimator.SetBool("IsJump", true);
        }
        //점프 도중 점프버튼에서 손을 뗀 경우
        if (Input.GetButtonUp("Jump") && playerRigidbody.velocity.y > 0f)
        {
            //점프 속도 절반으로 감소
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * 0.5f);
            
        }
        
        //뛰는 경우 애니메이션
        if (Mathf.Abs(playerRigidbody.velocity.x) < 0.3f)
        {
            playerAnimator.SetBool("IsRun", false);
        }
        else
        {
            playerAnimator.SetBool("IsRun", true);
        }
    }

    void FixedUpdate()
    {
        //수평값에 따른 이동
        playerRigidbody.velocity = new Vector2(horizontal * playerSpeed, playerRigidbody.velocity.y);
        
        //레이캐스트 디버그
        Debug.DrawRay(playerRigidbody.position, Vector2.down, Color.cyan);
        //플레이어가 떨어지는 경우
        if (playerRigidbody.velocity.y < 0f)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(playerRigidbody.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
            //땅을 감지하고
            if (rayHit.collider != null)
            {
                //거리가 0.3 미만이면
                if (rayHit.distance < 0.3f)
                {
                    //점프 애니메이션 해제
                    playerAnimator.SetBool("IsJump", false);
                }
                Debug.Log(rayHit.collider.name);
            }
        }
    }

    //플레이어 스프라이트 뒤집기
    void Flip()
    {
        //오른쪽을 보고 있는데 왼쪽으로 이동하거나 왼쪽을 보고 있는데 오른쪽으로 이동할 경우
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            //플레이어를 좌우로 뒤집기
            isFacingRight = !isFacingRight;
            sr.flipX = !isFacingRight;
        }
    }
}
