using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDogScene : MonoBehaviour
{
    //애니메이터 변수
    private Animator animator;

    public BossManager boss_manager; // BossManager 스크립트에 대한 참조


    [SerializeField] private GameObject main_camera; //플레이어 메인 카메라
    [SerializeField] private GameObject sub_camera; //보스 서브 카메라
    [SerializeField] private GameObject full_camera; //풀샷 카메라
    [SerializeField] private GameObject tutorial3; //튜토리얼 오브젝트
    [SerializeField] private GameObject tutorial4; //튜토리얼 오브젝트
    
    [SerializeField] bool camera_move_event1; //개를 향해 움직이는 카메라 이벤트 트리거
    [SerializeField] bool event2_end = false; //이벤트2 끝 확인
    [SerializeField] bool tutorial_end = false; //튜토리얼 끝 확인
    [SerializeField] bool tutorial2_end = false; //튜토리얼 끝 확인
    [SerializeField] bool port_end = false; //전사장치 이벤트 끝 확인

    private Vector3 full_camera_pos; //full camera pos 값 가져오기
    private bool full_camera_move = false;
    
    private Vector3 pos; //튜토리얼 텍스트 위치
    private Vector3 pos2; //튜토리얼 텍스트 위치
    private Vector3 destination = new Vector3(5.8f, -3f, -10f); //서브 카메라 도착 위치
    private TalkAction talk_action; //TalkAction 스크립트 참조

    public bool battle_start; //배틀 시작

    [SerializeField] private GameObject background; //배경 오브젝트
    [SerializeField] private Animator bg_animator; //Background Animator 참조
    [SerializeField] private Sprite[] background_sprites; //배경 스프라이트 배열
    [SerializeField] private GameObject ground; //ground 오브젝트
    [SerializeField] private Sprite[] ground_sprites; //Ground 스프라이트 배열
    
    
    private BossHealth boss_health;
    [SerializeField] public bool phase_start = false; //페이즈 전환 이벤트1 트리거
    [SerializeField] public bool phase_start2 = false; //페이즈 전환 이벤트2 트리거
    [SerializeField] public bool phase_start3 = false; //페이즈 전환 이벤트3 트리거

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    private Vector3 boss_spawn_pos;
    private Vector3 player_spawn_pos;

    [SerializeField] private GameObject corrider_potal;
    
    [SerializeField] public bool end_second_bossdog = false;

    private bool space_pressed = false;

    private PlayerController player_controller;


    // Start is called before the first frame update
    void Start()
    {
        //TalkAction 스크립트 할당
        talk_action = FindObjectOfType<TalkAction>();
        //메인 카메라(주인공) 비활성화, 서브 카메라(보스) 활성화
        main_camera.SetActive(false);
        sub_camera.SetActive(true);
        tutorial3.SetActive(false);
        tutorial4.SetActive(false);
        //카메라 움직임 이벤트1 실행
        camera_move_event1 = true;
        //튜토리얼 끝나기 전까지 battle_start 막기
        battle_start = false;
        //BossHealth 할당
        boss_health = FindObjectOfType<BossHealth>();

        boss_spawn_pos = boss.transform.position;
        player_spawn_pos = player.transform.position;
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        // BossManager 스크립트 참조
        boss_manager = boss.GetComponent<BossManager>();

        //background animator 할당
        bg_animator = background.GetComponent<Animator>();
        //full_camera_pos 할당
        full_camera_pos = full_camera.transform.position;

        animator = player.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //카메라 이벤트 1 : 보스룸 입장과 동시에 보스 쪽으로 카메라 빠르게 이동
        if (camera_move_event1)
        {
            player_controller.is_interacting = true;
            if (sub_camera.transform.position.x >= destination.x - 0.1f)
            {
                camera_move_event1 = false;
                //화난 개의 모습을 보여주기
                Debug.Log("그르렁거리는 개");

                Debug.Log("first_revive = " + GameManager.instance.first_revive);
                Debug.Log("second_revive = " + GameManager.instance.second_revive);

                //첫번째, 두번째 Revive인 경우
                if (GameManager.instance.first_revive || GameManager.instance.second_revive) 
                {
                    sub_camera.SetActive(false);
                    main_camera.SetActive(true);
                    GameManager.instance.story_info = 8;
                    GameManager.instance.tutorial_info = 3;
                    player_controller.is_interacting = false;
                    battle_start = true;
                }
                else // Revive 아닌 경우 대화창 이벤트 실행
                {
                    talk_action.Action(); 
                }
                 
            }
            else
            {
                //도착 지점까지 카메라 이동하기
                sub_camera.transform.position = Vector3.Lerp(sub_camera.transform.position, destination, 0.01f);
            }
        }

        if (GameManager.instance.story_info == 7 && !event2_end)
        {
            //메인 카메라(주인공) 활성화, 서브 카메라(보스) 비활성화
            sub_camera.SetActive(false);
            main_camera.SetActive(true);

            //대화창 이벤트 실행
            talk_action.Action();
           
            event2_end = true;
            player_controller.is_interacting = false;
        }

        if (GameManager.instance.story_info == 8 && GameManager.instance.tutorial_info == 2)
        {
            //튜토리얼 코루틴 활성화
            StartCoroutine(showTutorial());
            
            GameManager.instance.tutorial_info++;
        }

        if (boss_health.boss_hp == 0f && !phase_start)
        {
            //보스 움직임 멈춤(배틀 일시정지)
            battle_start = false;
            phase_start = true;

            StartCoroutine(PhaseTransition());
        }

        if (full_camera_move)
        {
            Vector3 dir_pos = full_camera_pos;
            //0.3f 거리 내에서 -1f ~ 1f 만큼 양 옆으로 이동 효과
            dir_pos.x = full_camera_pos.x + 0.3f * Mathf.Sin(Time.time * 10f);
            full_camera.transform.position = dir_pos;
            Invoke("EndFullCameraMove",3f);
        }

        if (GameManager.instance.story_info == 9 && !phase_start2)
        {
            //캐릭터 쪽으로 카메라 이동
            sub_camera.SetActive(false);
            full_camera.SetActive(false);
            main_camera.SetActive(true);
            
            phase_start2 = true;

            //두 번째 Revive인 경우
            if (GameManager.instance.second_revive)
            {
                GameManager.instance.story_info = 10;
            }
            else
            {
                talk_action.Action();
            }
        
        }
        

        if (GameManager.instance.story_info == 10 && GameManager.instance.tutorial_info == 3)
        {
            if (GameManager.instance.second_revive)
            {
                battle_start = true;
                end_second_bossdog = true;
                GameManager.instance.tutorial_info++;
            }
            else
            {
                //튜토리얼 코루틴 활성화
                StartCoroutine(showTutorial());
                GameManager.instance.tutorial_info++;
            }
            
        }

        if (GameManager.instance.story_info == 11 && !port_end)
        {
            sub_camera.SetActive(false);
            main_camera.SetActive(true);
            port_end = true;
            //전사장치 생성
            corrider_potal.SetActive(true);
            GameManager.instance.boss_num++; // 보스 인덱스를 사용하도록 수정 (24.02.13)
            GameManager.instance.is_back = true;
        }

        if (battle_start && boss_manager.player_hp == 0) // 플레이어 hp 0 이 되면 비틀거리고 Corrider 씬으로 이동한 다음 부활한다.
        {
            Debug.Log("실행");
            StartCoroutine(PlayerDeath());
        }


        if (Input.GetKeyDown(KeyCode.Space) && (tutorial3.activeSelf || tutorial4.activeSelf)) // 스페이스 키를 눌렀는지 체크
        {
            space_pressed = true;
        }

    }

    //튜토리얼
    IEnumerator showTutorial()
    {
        player_controller.is_interacting = true;
        yield return new WaitForSeconds(0.5f);
        if (GameManager.instance.story_info == 8 && GameManager.instance.tutorial_info == 3)
        {
            tutorial3.SetActive(true);
        }
        else
        {
            tutorial4.SetActive(true);
        }

        yield return StartCoroutine(waitForKeyPress());

        tutorial3.SetActive(false);
        tutorial4.SetActive(false);
        player_controller.is_interacting = false;
        battle_start = true;
        if (GameManager.instance.story_info == 10 && GameManager.instance.tutorial_info == 3)
        {
            end_second_bossdog = true;
        }
    }

    IEnumerator PhaseTransition()
    {
        main_camera.SetActive(false);
        sub_camera.SetActive(false);
        full_camera.SetActive(true);

        full_camera_move = true;
        
        bg_animator.SetTrigger("PhaseTransition");
        yield return new WaitForSeconds(0.5f);

        //두 번째 Revive인 경우
        if (GameManager.instance.second_revive && GameManager.instance.story_info == 8)
        {
            GameManager.instance.story_info = 9;
        }
        else if (GameManager.instance.second_revive && GameManager.instance.story_info == 10)
        {
            GameManager.instance.story_info = 11;
        }
        // 첫 번째 Revive & 첫 플레이인 경우
        else 
        {
            //대화창 이벤트
            talk_action.Action();
        }

        
        //배경 변경해놓기
        SpriteRenderer bg_sr = background.GetComponent<SpriteRenderer>();
        SpriteRenderer gr_sr = ground.GetComponent<SpriteRenderer>();
        bg_sr.sprite = background_sprites[1];
        gr_sr.sprite = ground_sprites[1];
        
        //플레이어랑 보스 원래 위치로 이동시키기
        boss.GetComponent<Animator>().SetBool("IsRun",false);
        boss.GetComponent<Animator>().Play("Idle");
        boss.GetComponent<SpriteRenderer>().flipX = false;
        
        player.transform.position = player_spawn_pos;
        boss.transform.position = boss_spawn_pos;

    }
    

    public void CompleteHacking()
    {
        main_camera.SetActive(false);
        Vector3 camera_pos = new Vector3(boss.transform.position.x, sub_camera.transform.position.y, sub_camera.transform.position.z);
        sub_camera.transform.position = camera_pos;
        sub_camera.SetActive(true);
        //개가 개 집으로 끌려들어감.
        Debug.Log("개가 개집으로 끌려 들어감.");
        boss.SetActive(false);
        talk_action.Action();
        
    }

    IEnumerator PlayerDeath()
    {
        //보스 움직임 멈춤(배틀 일시정지)
        battle_start = false;

        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.08f);

        GameManager.instance.if_revive = true;

        if (GameManager.instance.story_info == 10)
        {
            GameManager.instance.first_revive = false;
            GameManager.instance.second_revive = true;

        }
        else
        {
            GameManager.instance.first_revive = true;
            GameManager.instance.second_revive = false;
        }

        sub_camera.SetActive(false);
        full_camera.SetActive(false);
        main_camera.SetActive(true);

        SceneManager.LoadScene("Corrider"); // 플레이어가 보스전 중 사망하면 Corrider 씬으로 이동하여 부활
    }
    IEnumerator waitForKeyPress() // Space 키를 누르면 다음 대사로 넘어가는 함수
    {
        while (!space_pressed)
        {
            yield return null;
        }
        space_pressed = false; // Space 키를 눌렀다는 체크를 초기화
    }

    void EndFullCameraMove()
    {
        full_camera_move = false;
    }
}
