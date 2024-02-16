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
    
    //[SerializeField] private GameObject talk_portrait; //대화창 panel의 초상화
    [SerializeField] private TextMeshProUGUI talk_name_text; //대화창 panel의 이름 텍스트
    [SerializeField] private Image talk_name_panel; //대화창 panel의 이름창
    //[SerializeField] private Sprite[] portraits; //캐릭터 초상화 모음

    [SerializeField] private Sprite[] talk_name_panel_sprites; //talk 이름창 스프라이트 -> 0 : Player, 1 : Hoa, 2 : 과거
    [SerializeField] private Sprite[] talk_panel_sprites; //talk 대화창 스프라이트 -> 0 : 현재, 1 : 과거
    
    private int[] notice_array = { 1, 4, 7, 9, 10 }; //안내음인 스토리 정보
    private int[] hoa_array = { 12, 14, 16, 18, 20, 22, 24 }; //호아의 대화 정보
    private int[] past_array = {12,13,14,15,16,17,18,19,20,21,22,23,24}; //과거 시점 대화 정보
    private bool is_notice; //안내음인지
    private bool is_hoa; //호아의 대사인지
    private bool is_past; //과거 시점인지
    
    public bool is_talking = false; // 대화중인지 확인

    public int talk_index; // 현재 진행되고 있는 대화의 순번

    private void Start()
    {
        player_controller = FindObjectOfType<PlayerController>();
    }

    public void Action()
    {
        is_notice = Array.IndexOf(notice_array, GameManager.instance.story_info) > -1 ? true : false;
        is_hoa = Array.IndexOf(hoa_array, GameManager.instance.story_info) > -1 ? true : false;
        is_past = Array.IndexOf(past_array, GameManager.instance.story_info) > -1 ? true : false;
        
        Talk(GameManager.instance.story_info);
        
        if (is_notice)
        {
            notice_panel.SetActive(is_talking); 
        }
        else
        {
            talk_panel.SetActive(is_talking);

            if (is_past)
            {
                talk_name_panel.sprite = talk_name_panel_sprites[2];
                talk_panel.GetComponent<Image>().sprite = talk_panel_sprites[1];
            }
            else
            {
                talk_panel.GetComponent<Image>().sprite = talk_panel_sprites[0];
            }
            
            if (is_hoa)
            {
                talk_name_text.text = "호아";
                if (!is_past)
                {
                    talk_name_panel.sprite = talk_name_panel_sprites[1];
                }
            }
            else
            {
                if (is_past)
                {
                    talk_name_text.text = "과거의 나?";
                }
                else
                {
                    talk_name_text.text = "나";
                    talk_name_panel.sprite = talk_name_panel_sprites[0];
                }
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
