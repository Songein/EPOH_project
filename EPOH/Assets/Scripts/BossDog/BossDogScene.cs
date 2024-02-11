using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDogScene : MonoBehaviour
{
    [SerializeField] private GameObject main_camera; //플레이어 메인 카메라
    [SerializeField] private GameObject sub_camera; //보스 서브 카메라
    [SerializeField] private GameObject tutorial_text; //튜토리얼 텍스트 오브젝트
    [SerializeField] private GameObject tutorial_text2; //튜토리얼 텍스트 오브젝트
    
    [SerializeField] bool camera_move_event1; //개를 향해 움직이는 카메라 이벤트 트리거
    [SerializeField] bool event2_end = false; //이벤트2 끝 확인
    [SerializeField] bool tutorial_end = false; //튜토리얼 끝 확인
    [SerializeField] bool tutorial2_end = false; //튜토리얼 끝 확인
    
    private Vector3 pos; //튜토리얼 텍스트 위치
    private Vector3 pos2; //튜토리얼 텍스트 위치
    private Vector3 destination = new Vector3(5.8f, -3f, -10f); //서브 카메라 도착 위치
    private TalkAction talk_action; //TalkAction 스크립트 참조

    public bool battle_start; //배틀 시작

    [SerializeField] private GameObject background; //배경 오브젝트
    [SerializeField] private GameObject[] platforms; //발판 오브젝트들
    [SerializeField] private Sprite[] background_sprites; //배경 스프라이트 배열
    [SerializeField] private Sprite[] platform_sprites; //발판 스프라이트

    private BossHealth boss_health;
    [SerializeField] private bool phase_start = false; //페이즈 전환 이벤트1 트리거
    [SerializeField] private bool phase_start2 = false; //페이즈 전환 이벤트2 트리거
    [SerializeField] private bool phase_start3 = false; //페이즈 전환 이벤트3 트리거

    [SerializeField] private GameObject phase_transition;
    [SerializeField] private Animation transition_animation;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    private Vector3 boss_spawn_pos;
    private Vector3 player_spawn_pos;
    
    // Start is called before the first frame update
    void Start()
    {
        //TalkAction 스크립트 할당
        talk_action = FindObjectOfType<TalkAction>();
        //메인 카메라(주인공) 비활성화, 서브 카메라(보스) 활성화
        main_camera.SetActive(false);
        sub_camera.SetActive(true);
        tutorial_text.SetActive(false);
        pos = tutorial_text.transform.position; //튜토리얼 텍스트 위치 값 가져오기
        pos2 = tutorial_text2.transform.position; //튜토리얼 텍스트 위치 값 가져오기
        //카메라 움직임 이벤트1 실행
        camera_move_event1 = true;
        //튜토리얼 끝나기 전까지 battle_start 막기
        battle_start = false;
        //BossHealth 할당
        boss_health = FindObjectOfType<BossHealth>();

        boss_spawn_pos = boss.transform.position;
        player_spawn_pos = player.transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        //카메라 이벤트 1 : 보스룸 입장과 동시에 보스 쪽으로 카메라 빠르게 이동ㄴ 
        if (camera_move_event1)
        {
            if (sub_camera.transform.position.x >= destination.x - 0.1f)
            {
                camera_move_event1 = false;
                //화난 개의 모습을 보여주기
                Debug.Log("그르렁거리는 개");
                //주인공의 대사 이벤트 실행
                talk_action.Action();
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
        }

        if (GameManager.instance.story_info == 8 && !tutorial_end)
        {
            //튜토리얼 텍스트 오브젝트 활성화
            tutorial_text.SetActive(true);
            Vector3 dir_pos = pos;
            //0.3f 거리 내에서 -1f ~ 1f 만큼 위아래로 이동 효과
            dir_pos.y = pos.y + 0.3f * Mathf.Sin(Time.time * 1f);
            tutorial_text.transform.position = dir_pos;
        }

        if (boss_health.boss_hp == 0f && !phase_start)
        {
            //보스 움직임 멈춤(배틀 일시정지)
            battle_start = false;
            phase_start = true;

            StartCoroutine(PhaseCloseTransition());
        }

        if (GameManager.instance.story_info == 9 && !phase_start2)
        {
            //캐릭터 쪽으로 카메라 이동
            sub_camera.SetActive(false);
            main_camera.SetActive(true);
            
            phase_start2 = true;
            talk_action.Action();
        }

        if (GameManager.instance.story_info == 8 && talk_action.talk_index == 2 && !phase_start3)
        {
            phase_start3 = true;
            StartCoroutine(PhaseOpenTransition());
        }

        if (GameManager.instance.story_info == 10 && !tutorial2_end)
        {
            phase_transition.SetActive(false);
            
            //튜토리얼 텍스트 오브젝트 활성화
            tutorial_text2.SetActive(true);
            Vector3 dir_pos = pos2;
            //0.3f 거리 내에서 -1f ~ 1f 만큼 위아래로 이동 효과
            dir_pos.y = pos.y + 0.3f * Mathf.Sin(Time.time * 1f);
            tutorial_text2.transform.position = dir_pos;
        }
        
    }

    //튜토리얼 끝
    public void EndTutorial()
    {
        if (GameManager.instance.story_info == 8)
        {
            tutorial_text.SetActive(false);
            tutorial_end = true;
        }
        else
        {
            tutorial_text2.SetActive(false);
            tutorial2_end = true;
        }
        battle_start = true;
        
    }

    IEnumerator PhaseCloseTransition()
    {
        //페이즈 close 애니메이션 시작
        transition_animation.Play("Close Transition");
        yield return new WaitForSeconds(1.8f);
        //대화창 이벤트
        talk_action.Action();
        //배경 변경해놓기
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        sr.sprite = background_sprites[1];
        //플레이어랑 보스 원래 위치로 이동시키기
        player.transform.position = player_spawn_pos;
        boss.transform.position = boss_spawn_pos;
        
    }

    IEnumerator PhaseOpenTransition()
    {
        main_camera.SetActive(false);
        sub_camera.SetActive(true);
        
        //페이즈 open 애니메이션 시작
        transition_animation.Play("Open Transition");
        yield return new WaitForSeconds(1.8f);
    }
}
