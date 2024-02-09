using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDogScene : MonoBehaviour
{
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject sub_camera;
    [SerializeField] private GameObject tutorial_text;
    
    [SerializeField] bool camera_move_event1;
    [SerializeField] bool event2_end = false;
    [SerializeField] bool tutorial_end = false;
    private Vector3 pos;
    private Vector3 destination = new Vector3(5.8f, -3f, -10f);
    private TalkAction talk_action;

    [SerializeField] private bool battle_start;
    // Start is called before the first frame update
    void Start()
    {
        //TalkAction 스크립트 할당
        talk_action = FindObjectOfType<TalkAction>();
        //메인 카메라(주인공) 비활성화, 서브 카메라(보스) 활성화
        main_camera.SetActive(false);
        sub_camera.SetActive(true);
        tutorial_text.SetActive(false);
        pos = tutorial_text.transform.position;
        //카메라 움직임 이벤트1 실행
        camera_move_event1 = true;
        //튜토리얼 끝나기 전까지 battle_start 막기
        battle_start = false;
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
                sub_camera.transform.position = Vector3.Lerp(sub_camera.transform.position, destination, 0.01f);
            }
        }

        if (GameManager.instance.story_info == 7 && !event2_end)
        {
            //메인 카메라(주인공) 활성화, 서브 카메라(보스) 비활성화
            sub_camera.SetActive(false);
            main_camera.SetActive(true);
            talk_action.Action();
            event2_end = true;
        }

        if (GameManager.instance.story_info == 8 && !tutorial_end)
        {
            tutorial_text.SetActive(true);
            Vector3 dir_pos = pos;
            //0.3f 거리 내에서 -1f ~ 1f 만큼 위아래로 이동 효과
            dir_pos.y = pos.y + 0.3f * Mathf.Sin(Time.time * 1f);
            tutorial_text.transform.position = dir_pos;
        }
        
    }

    public void EndTutorial()
    {
        tutorial_text.SetActive(false);
        tutorial_end = true;
    }
}
