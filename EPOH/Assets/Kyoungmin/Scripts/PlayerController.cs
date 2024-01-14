using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    //플레이어 좌우 이동
    private float horizontal; //수평 값
    public float player_speed = 8f; //이동 속도
    private bool is_facing_right = true; //플레이어가 오른쪽을 쳐다보고 있는지
    private AttackArea attack_area; //AttackArea 스크립트(AttackArea의 좌우반전을 위해)
    
    //플레이어 점프
    public float playerJumpForce = 7f; //점프 힘
    private int player_jump_cnt = 0; //플레이어 점프 횟수
    
    //플레이어 리지드바디 컴포넌트
    private Rigidbody2D rigid;
    //플레이어 애니메이터
    private Animator animator;
    //스프라이트 렌더러 컴포넌트
    private SpriteRenderer sr;
    
    //플레이어 상호작용
    private GameObject interact_obj; //플레이어가 상호작용할 오브젝트
    public bool is_interacting = false; //플레이어가 상호작용 중인지
    private Interaction interaction; //플레이어가 상호작용할 오브젝트에 부착된 Interact 스크립트

    //플레이어 대화
    public bool is_talking = false;
    public TalkAction talkaction;

    //플레이어 대쉬
    [SerializeField] private TrailRenderer tr; //대쉬 효과
    private bool can_dash = true; //플레이어가 대쉬를 할 수 있는지
    public float dash_power = 20f; //대쉬 파워
    private bool is_dashing = false; //플레이어가 대쉬 중인지
    public float dash_time = 0.3f; //대쉬 지속 타임
    public float dash_cool_time = 2f; //대쉬 쿨타임
    
    //순간이동
    private Vector2 teleport_pos; //순간이동할 위치
    private bool can_teleport = false; //순간이동할 수 있는지
    private bool is_teleporting = false; //순간이동 중인지
    public float teleport_time = 0.3f; //순간이동 지속 타임
    public GameObject port_prefab; //순간이동 포트 프리팹
    private GameObject port; //순간이동 포트
    
    //대화창
    public bool is_talking = false; //플레이어가 대화 중인지
    
    //공격
    public bool is_attacking = false; //공격 중일 때 플레이어 이동 막기 위한 변수
    

    void Start()
    {
        //Rigidbody2D 컴포넌트 할당
        rigid = GetComponent<Rigidbody2D>();
        //Animator 컴포넌트 할당
        animator = GetComponent<Animator>();
        //Sprite Renderer 컴포넌트 할당
        sr = GetComponent<SpriteRenderer>();
        //Trail Renderer 컴포넌트 할당
        tr = GetComponent<TrailRenderer>();
        //AttackArea 오브젝트의 컴포넌트 할당
        attack_area = transform.GetChild(0).gameObject.GetComponent<AttackArea>();
    }
    
    void Update()
    {
        //대쉬 | 상호작용 | 순간이동 | 대화 중이면 다른 작업 이루어지지 않도록
        if (is_dashing || is_interacting || is_teleporting || is_attacking)
        {
            return;
        }
        
        //수평값 읽어오기
        horizontal = Input.GetAxisRaw("Horizontal");
        //플레이어 Flip 검사
        Flip();
        //뛰는 경우 애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }

        //점프 버튼을 누르고 점프 횟수가 2미만일 때 점프 수행
        //아래 화살표 키를 누르고 있지 않은 경우에만 점프 실행(누르고 있는 경우는 점프가 아닌 발판에서 내려오기 위한 입력으로 판단)
        if (Input.GetButtonDown("Jump") && player_jump_cnt < 2 && !Input.GetKey(KeyCode.DownArrow))
        {
            switch (player_jump_cnt)
            {
                case 0 : //첫 점프일 때
                    rigid.velocity = new Vector2(rigid.velocity.x, playerJumpForce);
                    animator.SetBool("IsJump", true);
                    break;
                case 1 : //2단 점프일 때
                    rigid.velocity = new Vector2(rigid.velocity.x, playerJumpForce * 1.5f); //2단 점프는 좀 더 높이 점프
                    animator.SetBool("IsDoubleJump",true);
                    break;
                    
            }
            player_jump_cnt++;
            
        }
        
        /*
        //점프 도중 점프버튼에서 손을 뗀 경우
        if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0f)
        {
            //점프 속도 절반으로 감소
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
            
        }
        */
        
        //대쉬 버튼을 누르면
        if (Input.GetButtonDown("Dash") && can_dash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetButtonDown("Interact")) // 상호작용
        {
            if (is_talking) // 대화 중이면
            {
                talkaction.Action(); //다음 대화
            }
            else if (interact_obj != null) // 대화 중이 아니고 상호작용 할 오브젝트가 있을 경우
            {
                Debug.Log(interact_obj.name + "과 상호작용 시작");
                interaction.Interact();
                is_interacting = true;
            }
        }
        
        //순간이동 버튼을 누르면
        if (Input.GetButtonDown("Teleport"))
        {
            if (can_teleport) //순간이동을 할 수 있으면(표식을 설치한 경우)
            {
                StartCoroutine(Teleport());
            }
            else //표식을 설치하지 않은 경우
            {
                animator.SetInteger("IsTeleport",0); //순간이동 표식 설치 애니메이션 실행
                Invoke("EndPortAni", 0.3f); //0.3초 후 순간이동 표식 설치 애니메이션 종료
                teleport_pos = transform.position; //플레이어의 현재 위치 받아오기
                port = Instantiate(port_prefab, new Vector2(teleport_pos.x, teleport_pos.y), Quaternion.identity); //표식 생성
                can_teleport = true; //순간이동 할 수 있다고 상태 변경
            }
        }
        
    }

    void FixedUpdate()
    {
        //대쉬 | 상호작용 | 순간이동 | 대화 중이면 다른 작업 이루어지지 않도록
        if (is_dashing || is_interacting || is_teleporting || is_talking || is_attacking)
        {
            return;
        }
        
        //수평값에 따른 이동
        rigid.velocity = new Vector2(horizontal * player_speed, rigid.velocity.y);
        
        //땅 감지 레이캐스트 디버그
        //Debug.DrawRay(rigid.position, Vector2.down, Color.cyan);
        
        //플레이어가 떨어지는 경우
        if (rigid.velocity.y < 0f)
        {
            animator.SetBool("IsFall", true);
            RaycastHit2D groundRayHit = Physics2D.Raycast(rigid.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
            //땅을 감지하고
            if (groundRayHit.collider != null)
            {
                //거리가 0.3 미만이면
                if (groundRayHit.distance < 0.3f)
                {
                    //점프 애니메이션 해제
                    animator.SetBool("IsFall", false);
                    animator.SetBool("IsJump",false);
                    animator.SetBool("IsDoubleJump",false);
                    player_jump_cnt = 0; //바닥에 닿으면 플레이어 점프 횟수 초기화

                }

                //Debug.Log(groundRayHit.collider.name);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //트리거 충돌한 오브젝트가 Interaction 태그를 갖고 있는 오브젝트일 경우
        if (other.CompareTag("Interaction"))
        {
            //상호작용 할 오브젝트에 트리거 충돌 오브젝트를 할당
            interact_obj = other.gameObject;
            interaction = interact_obj.GetComponent<Interaction>(); // 충돌한 오브젝트의 Interaction 할당
            Debug.Log("[PlayerController] : " + other.name + "과 상호작용 가능");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        //트리거 충돌 오브젝트에게서 멀어질 때
        if (interact_obj != null)
        {
            //상호작용 할 오브젝트에 null 할당
            interact_obj = null;
            Debug.Log("[PlayerController] : " + other.name + "과 상호작용 불가능");
        }
        
    }


    //플레이어 스프라이트 뒤집기
    void Flip()
    {
        //오른쪽을 보고 있는데 왼쪽으로 이동하거나 왼쪽을 보고 있는데 오른쪽으로 이동할 경우
        if (is_facing_right && horizontal < 0f || !is_facing_right && horizontal > 0f)
        {
            //플레이어를 좌우로 뒤집기
            is_facing_right = !is_facing_right;
            //sprite renderer flipx 값 변경하기
            sr.flipX = !is_facing_right;
            //공격 범위도 뒤집기
            attack_area.Flip(is_facing_right);
        }
    }

    //대쉬
    private IEnumerator Dash()
    {
        //Dash 시작 시
        can_dash = false; //대쉬 불가능으로 설정
        is_dashing = true; //대쉬 중으로 설정
        animator.SetBool("IsDash", true); //대쉬 애니메이션 시작
        float original_gravity = rigid.gravityScale; //플레이어의 원래 중력 값 저장
        rigid.gravityScale = 0f; //플레이어의 중력 0으로 변경하여 중력 무시
        //플레이어가 바라보고 있는 방향으로 대쉬
        if (is_facing_right)
        {
            //오른쪽 바라보고 있으면 오른쪽으로 대쉬
            rigid.velocity = new Vector2(transform.localScale.x * dash_power, 0f);
        }
        else
        {
            //왼쪽 바라보고 있으면 왼쪽으로 대쉬
            rigid.velocity = new Vector2(transform.localScale.x * dash_power * (-1), 0f);
        }
        tr.emitting = true; //대쉬 효과 발산

        //Dash 끝
        yield return new WaitForSeconds(dash_time);
        tr.emitting = false; //대쉬 효과 끝
        rigid.gravityScale = original_gravity; //플레이어의 원래 중력 회복
        animator.SetBool("IsDash", false); //대쉬 애니메이션 끝
        is_dashing = false; //대쉬 중 해제

        //Dash 쿨 타임
        yield return new WaitForSeconds(dash_cool_time);
        can_dash = true; //쿨타임 후 대쉬 가능으로 변경
        Debug.Log("[PlayController] : Dash 쿨타임 끝");
    }
    
    //순간이동
    private IEnumerator Teleport()
    {
        //순간이동 시작 시
        can_teleport = false; //순간이동 불가능으로 설정
        is_teleporting = true; //순간이동 중으로 설정
        Destroy(port); //순간이동 표식 제거
        gameObject.transform.position = new Vector2(teleport_pos.x, teleport_pos.y + 2f); //순간이동 표식보다 y축으로 2만큼 위로 이동
        animator.SetBool("IsFall",true); //순간이동 끝 애니메이션 실행
        //rigid.velocity = new Vector2(rigid.velocity.x, playerJumpForce);

        //순간이동 끝
        yield return new WaitForSeconds(teleport_time);
        is_teleporting = false; //순간이동 중 해제
    }

    void EndPortAni() //순간이동 표식 생성 애니메이션 해제
    {
        animator.SetInteger("IsTeleport",-1);
    }
    
}
