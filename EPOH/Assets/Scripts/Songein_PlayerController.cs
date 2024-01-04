using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Songein_PlayerController : MonoBehaviour
{
    //�÷��̾� �¿� �̵�
    private float horizontal; //���� ��
    public float player_speed = 8f; //�̵� �ӵ�
    private bool is_facing_right = true; //�÷��̾ �������� �Ĵٺ��� �ִ���

    //�÷��̾� ����
    public float playerJumpForce = 7f; //���� ��
    private int player_jump_cnt = 0; //�÷��̾� ���� Ƚ��

    //�÷��̾� ������ٵ� ������Ʈ
    private Rigidbody2D rigid;
    //�÷��̾� �ִϸ�����
    private Animator animator;
    //��������Ʈ ������ ������Ʈ
    private SpriteRenderer sr;

    //�÷��̾� ��ȣ�ۿ�
    private GameObject interact_obj; //�÷��̾ ��ȣ�ۿ��� ������Ʈ
    private bool is_interacting = false; //�÷��̾ ��ȣ�ۿ� ������

    // ****************************************************************************************�÷��̾� ��ȭ
    public bool is_talking = false;
    public TalkAction talkaction;

    //�÷��̾� �뽬
    [SerializeField] private TrailRenderer tr; //�뽬 ȿ��
    private bool can_dash = true; //�÷��̾ �뽬�� �� �� �ִ���
    public float dash_power = 20f; //�뽬 �Ŀ�
    private bool is_dashing = false; //�÷��̾ �뽬 ������
    public float dash_time = 0.3f; //�뽬 ���� Ÿ��
    public float dash_cool_time = 2f; //�뽬 ��Ÿ��

    //�����̵�
    private Vector2 teleport_pos; //�����̵��� ��ġ
    private bool can_teleport = false; //�����̵��� �� �ִ���
    private bool is_teleporting = false; //�����̵� ������
    public float teleport_time = 0.3f; //�����̵� ���� Ÿ��
    public GameObject port_prefab; //�����̵� ��Ʈ ������
    private GameObject port; //�����̵� ��Ʈ

    void Start()
    {
        //Rigidbody2D ������Ʈ �Ҵ�
        rigid = GetComponent<Rigidbody2D>();
        //Animator ������Ʈ �Ҵ�
        animator = GetComponent<Animator>();
        //Sprite Renderer ������Ʈ �Ҵ�
        sr = GetComponent<SpriteRenderer>();
        //Trail Renderer ������Ʈ �Ҵ�
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        //�뽬 ���̰� ��ȣ�ۿ� ���̸� �ٸ� �۾� �̷������ �ʵ���
        if (is_dashing || is_interacting || is_teleporting) //***************************************** ���� is_talking ������ �ȵ�
        {
            return;
        }

        //���� �о����
        horizontal = Input.GetAxisRaw("Horizontal");
        //�÷��̾� Flip �˻�
        Flip();
        //�ٴ� ��� �ִϸ��̼�
        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }

        //���� ��ư�� ������ ���� Ƚ���� 2�̸��� �� ���� ����
        if (Input.GetButtonDown("Jump") && player_jump_cnt < 2)
        {
            switch (player_jump_cnt)
            {
                case 0: //ù ������ ��
                    rigid.velocity = new Vector2(rigid.velocity.x, playerJumpForce);
                    animator.SetBool("IsJump", true);
                    break;
                case 1: //2�� ������ ��
                    rigid.velocity = new Vector2(rigid.velocity.x, playerJumpForce * 1.5f); //2�� ������ �� �� ���� ����
                    animator.SetBool("IsDoubleJump", true);
                    break;

            }
            player_jump_cnt++;
            Debug.Log("���� Ƚ�� : " + player_jump_cnt);

        }

        /*
        //���� ���� ������ư���� ���� �� ���
        if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0f)
        {
            //���� �ӵ� �������� ����
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
            
        }
        */

        //�뽬 ��ư�� ������
        if (Input.GetButtonDown("Dash") && can_dash)
        {
            StartCoroutine(Dash());
        }

        //�����̽��� ���� ��� ********************************************************************************
        if (Input.GetButtonDown("Interact"))
        {
            if (is_talking) // ��ȭ ���̸�
            {
                talkaction.Action(); //���� ��ȭ
            }
            else if (interact_obj != null) // ��ȭ ���� �ƴϰ� ��ȣ�ۿ� �� ������Ʈ�� ���� ���
            {
                Debug.Log(interact_obj.name + "�� ��ȣ�ۿ� ����");
                is_interacting = true;
            }
        }

        //�����̵� ��ư�� ������
        if (Input.GetButtonDown("Teleport"))
        {
            if (can_teleport) //�����̵��� �� �� ������(ǥ���� ��ġ�� ���)
            {
                StartCoroutine(Teleport());
            }
            else //ǥ���� ��ġ���� ���� ���
            {
                animator.SetInteger("IsTeleport", 0); //�����̵� ǥ�� ��ġ �ִϸ��̼� ����
                Invoke("EndPortAni", 0.3f); //0.3�� �� �����̵� ǥ�� ��ġ �ִϸ��̼� ����
                teleport_pos = transform.position; //�÷��̾��� ���� ��ġ �޾ƿ���
                port = Instantiate(port_prefab, new Vector2(teleport_pos.x, teleport_pos.y), Quaternion.identity); //ǥ�� ����
                can_teleport = true; //�����̵� �� �� �ִٰ� ���� ����
            }
        }

    }

    void FixedUpdate()
    {
        //�뽬 ���̰� ��ȣ�ۿ� ���̸� �ٸ� �۾� �̷������ �ʵ���
        if (is_dashing || is_interacting || is_teleporting || is_talking) // ***********************
        {
            return;
        }

        //���򰪿� ���� �̵�
        rigid.velocity = new Vector2(horizontal * player_speed, rigid.velocity.y);

        //�� ���� ����ĳ��Ʈ �����
        //Debug.DrawRay(rigid.position, Vector2.down, Color.cyan);

        //�÷��̾ �������� ���
        if (rigid.velocity.y < 0f)
        {
            RaycastHit2D groundRayHit = Physics2D.Raycast(rigid.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
            //���� �����ϰ�
            if (groundRayHit.collider != null)
            {
                //�Ÿ��� 0.3 �̸��̸�
                if (groundRayHit.distance < 0.3f)
                {
                    //���� �ִϸ��̼� ����
                    animator.SetBool("IsJump", false);
                    animator.SetBool("IsDoubleJump", false);
                    animator.SetInteger("IsTeleport", -1);
                    player_jump_cnt = 0; //�ٴڿ� ������ �÷��̾� ���� Ƚ�� �ʱ�ȭ

                }

                //Debug.Log(groundRayHit.collider.name);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //��ȣ�ۿ� ������ �ʰ�, Ʈ���� �浹�� ������Ʈ�� Interaction �±׸� ���� �ִ� ������Ʈ�� ���
        if (other.CompareTag("Interaction") && !is_interacting)
        {
            //��ȣ�ۿ� �� ������Ʈ�� Ʈ���� �浹 ������Ʈ�� �Ҵ�
            interact_obj = other.gameObject;
            Debug.Log(other.name + "�� ��ȣ�ۿ� ����");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //��ȣ�ۿ� ������ �ʰ�, Ʈ���� �浹 ������Ʈ���Լ� �־��� ��
        if (interact_obj != null && !is_interacting)
        {
            //��ȣ�ۿ� �� ������Ʈ�� null �Ҵ�
            interact_obj = null;
            Debug.Log(other.name + "�� ��ȣ�ۿ� �Ұ���");
        }

    }


    //�÷��̾� ��������Ʈ ������
    void Flip()
    {
        //�������� ���� �ִµ� �������� �̵��ϰų� ������ ���� �ִµ� ���������� �̵��� ���
        if (is_facing_right && horizontal < 0f || !is_facing_right && horizontal > 0f)
        {
            //�÷��̾ �¿�� ������
            is_facing_right = !is_facing_right;
            //sprite renderer flipx �� �����ϱ�
            sr.flipX = !is_facing_right;
        }
    }

    //�뽬
    private IEnumerator Dash()
    {
        //Dash ���� ��
        can_dash = false; //�뽬 �Ұ������� ����
        is_dashing = true; //�뽬 ������ ����
        animator.SetBool("IsDash", true); //�뽬 �ִϸ��̼� ����
        float original_gravity = rigid.gravityScale; //�÷��̾��� ���� �߷� �� ����
        rigid.gravityScale = 0f; //�÷��̾��� �߷� 0���� �����Ͽ� �߷� ����
        //�÷��̾ �ٶ󺸰� �ִ� �������� �뽬
        if (is_facing_right)
        {
            //������ �ٶ󺸰� ������ ���������� �뽬
            rigid.velocity = new Vector2(transform.localScale.x * dash_power, 0f);
        }
        else
        {
            //���� �ٶ󺸰� ������ �������� �뽬
            rigid.velocity = new Vector2(transform.localScale.x * dash_power * (-1), 0f);
        }
        tr.emitting = true; //�뽬 ȿ�� �߻�

        //Dash ��
        yield return new WaitForSeconds(dash_time);
        tr.emitting = false; //�뽬 ȿ�� ��
        rigid.gravityScale = original_gravity; //�÷��̾��� ���� �߷� ȸ��
        animator.SetBool("IsDash", false); //�뽬 �ִϸ��̼� ��
        is_dashing = false; //�뽬 �� ����

        //Dash �� Ÿ��
        yield return new WaitForSeconds(dash_cool_time);
        can_dash = true; //��Ÿ�� �� �뽬 �������� ����
        Debug.Log("Dash ��Ÿ�� ��");
    }

    //�����̵�
    private IEnumerator Teleport()
    {
        //�����̵� ���� ��
        can_teleport = false; //�����̵� �Ұ������� ����
        is_teleporting = true; //�����̵� ������ ����
        Destroy(port); //�����̵� ǥ�� ����
        gameObject.transform.position = new Vector2(teleport_pos.x, teleport_pos.y + 2f); //�����̵� ǥ�ĺ��� y������ 2��ŭ ���� �̵�
        animator.SetInteger("IsTeleport", 1); //�����̵� �� �ִϸ��̼� ����
        //rigid.velocity = new Vector2(rigid.velocity.x, playerJumpForce);

        //�����̵� ��
        yield return new WaitForSeconds(teleport_time);
        is_teleporting = false; //�����̵� �� ����
    }

    void EndPortAni() //�����̵� ǥ�� ���� �ִϸ��̼� ����
    {
        animator.SetInteger("IsTeleport", -1);
    }


}