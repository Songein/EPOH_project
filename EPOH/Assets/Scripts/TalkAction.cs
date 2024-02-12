using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkAction : MonoBehaviour
{
    public TalkManager talk_manager;
    public TypingEffect talk_effect;
    public PlayerController player_controller;

    public GameObject talk_panel; // 대화창 panel
    public GameObject notice_panel; // 안내음 panel
    
    [SerializeField] private GameObject talk_portrait; //대화창 panel의 초상화
    [SerializeField] private GameObject talk_name; //대화창 panel의 이름
    [SerializeField] private Sprite[] portraits; //캐릭터 초상화 모음
    
    private int[] noitce_array = { 1, 4, 7, 9, 10 }; //안내음인 스토리 정보
    private int[] hoa_array = { 12, 14, 16, 18, 20, 22, 24 }; //호아의 대화 정보
    private bool is_notice;
    private bool is_hoa;
    
    public bool is_talking = false; // 대화중인지 확인

    public int talk_index; // 현재 진행되고 있는 대화의 순번

    private void Start()
    {
        talk_portrait = talk_panel.transform.GetChild(0).gameObject;
        talk_name = talk_panel.transform.GetChild(1).gameObject;
    }

    public void Action()
    {
        is_notice = Array.IndexOf(notice_array, GameManager.instance.story_info) > -1 ? true : false;
        Talk(GameManager.instance.story_info);
        
        if (is_notice)
        {
            notice_panel.SetActive(is_talking); 
        }
        else
        {
            talk_panel.SetActive(is_talking);
            is_hoa = Array.IndexOf(hoa_array, GameManager.instance.story_info) > -1 ? true : false;
            if (is_hoa)
            {
                talk_portrait.GetComponent<Image>().sprite = portraits[1];
                talk_name.GetComponent<TextMeshProUGUI>().text = "[ 호아 ]";
            }
            else
            {
                talk_portrait.GetComponent<Image>().sprite = portraits[0];
                talk_name.GetComponent<TextMeshProUGUI>().text = "[ 주인공 ]";
            }
        }
        player_controller.is_talking = is_talking;
    }
    
    public void Talk(int id)
    {
        string talk_line = talk_manager.GetTalk(id, talk_index); // talk manager의 GetTalk 함수를 호출하여 대사를 한 줄 받아옴
        if (talk_line == null) // 더 이상 대사가 없을 경우
        {
            is_talking=false; // 대화 상태를 false로
            talk_index = 0; // 인덱스 초기화
            GameManager.instance.story_info++;
            return;
        }
        // 대사가 있을 경우
        talk_effect.SetMessage(talk_line, is_notice); // talk effect로 메세지를 출력
        talk_index++; // 대사 인덱스를 올림
        is_talking = true; // 대화 상태를 true로
    }
    
}
