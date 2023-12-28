using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //플레이어가 상호작용할 오브젝트
    private GameObject interactObj;
    //플레이어가 상호작용 중인지
    private bool isInteracting = false;

    [SerializeField] private TrailRenderer tr;

    private bool can_dash = true; //플레이어가 대쉬를 할 수 있는지
    public float dash_power = 8f; //대쉬 파워
    private bool is_dashing = false; //플레이어가 대쉬 중인지
    public float dash_time = 0.5f; //대쉬 지속 타임
    public float dash_cool_time = 2f; //대쉬 쿨타임
    
    void Start()
    {
        //게임 오브젝트로부터 Rigidbody2D 컴포넌트를 가져와서 할당하기
        playerRigidbody = GetComponent<Rigidbody2D>();
        //게임 오브젝트로부터 Animator 컴포넌트 가져와서 할당하기
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }
    
    void Update()
    {
        if (is_dashing || isInteracting)
        {
            return;
        }
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
        
        
        //대쉬 버튼을 누르면
        if (Input.GetKeyDown(KeyCode.C) && can_dash)
        {
            StartCoroutine(Dash());
        }
        
        //스페이스를 누르고, 상호작용 할 오브젝트가 존재하고, 상호작용 중이지 않을 경우
        if (Input.GetKeyDown(KeyCode.Space) && (interactObj != null) && !isInteracting)
        {
            Debug.Log(interactObj.name + "과 상호작용 시작");
            isInteracting = true;
        }
        
    }

    void FixedUpdate()
    {
        if (is_dashing || isInteracting)
        {
            return;
        }
        //수평값에 따른 이동
        playerRigidbody.velocity = new Vector2(horizontal * playerSpeed, playerRigidbody.velocity.y);
        
        //땅 감지 레이캐스트 디버그
        Debug.DrawRay(playerRigidbody.position, Vector2.down, Color.cyan);
        
        //플레이어가 떨어지는 경우
        if (playerRigidbody.velocity.y < 0f)
        {
            RaycastHit2D groundRayHit = Physics2D.Raycast(playerRigidbody.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
            //땅을 감지하고
            if (groundRayHit.collider != null)
            {
                //거리가 0.3 미만이면
                if (groundRayHit.distance < 0.3f)
                {
                    //점프 애니메이션 해제
                    playerAnimator.SetBool("IsJump", false);
                }

                Debug.Log(groundRayHit.collider.name);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //상호작용 중이지 않고, 트리거 충돌한 오브젝트가 Interaction 태그를 갖고 있는 오브젝트일 경우
        if (other.CompareTag("Interaction") && !isInteracting)
        {
            //상호작용 할 오브젝트에 트리거 충돌 오브젝트를 할당
            interactObj = other.gameObject;
            Debug.Log(other.name + "과 상호작용 가능");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        //상호작용 중이지 않고, 트리거 충돌 오브젝트에게서 멀어질 때
        if (interactObj != null && !isInteracting)
        {
            //상호작용 할 오브젝트에 null 할당
            interactObj = null;
            Debug.Log(other.name + "과 상호작용 불가능");
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

    //대쉬
    private IEnumerator Dash()
    {
        //Dash 시작 시
        can_dash = false;
        is_dashing = true;
        playerAnimator.SetBool("IsDash", true);
        float original_gravity = playerRigidbody.gravityScale;
        playerRigidbody.gravityScale = 0f;
        if (isFacingRight)
        {
            playerRigidbody.velocity = new Vector2(transform.localScale.x * dash_power, 0f);
        }
        else
        {
            playerRigidbody.velocity = new Vector2(transform.localScale.x * dash_power * (-1), 0f);
        }
        tr.emitting = true;

        //Dash 끝
        yield return new WaitForSeconds(dash_time);
        tr.emitting = false;
        playerRigidbody.gravityScale = original_gravity;
        playerAnimator.SetBool("IsDash", false);
        is_dashing = false;

        //Dash 쿨 타임
        yield return new WaitForSeconds(dash_cool_time);
        can_dash = true;
        Debug.Log("Dash 쿨타임 끝");
    }
    
}
