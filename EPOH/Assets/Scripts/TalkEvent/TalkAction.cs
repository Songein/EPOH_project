using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkAction : MonoBehaviour
{
    public TalkManager talk_manager;
    public GameManager game_manager;
    public TypingEffect talk_effect;
    public PlayerController player_controller;

    public GameObject talk_panel; // 대화창 panel
    public bool is_talking = false; // 대화중인지 확인

    public int talk_index; // 현재 진행되고 있는 대화의 순번


    public void Action()
    {
        Talk(game_manager.story_info);
        talk_panel.SetActive(is_talking);
        player_controller.is_talking = is_talking;
    }

    void Talk(int id)
    {
        string talk_line = talk_manager.GetTalk(id, talk_index); // talk manager의 GetTalk 함수를 호출하여 대사를 한 줄 받아옴
        if (talk_line == null) // 더 이상 대사가 없을 경우
        {
            is_talking=false; // 대화 상태를 false로
            talk_index = 0; // 인덱스 초기화
            game_manager.story_info++;
            return;
        }
        // 대사가 있을 경우
        talk_effect.SetMessage(talk_line); // talk effect로 메세지를 출력
        talk_index++; // 대사 인덱스를 올림
        is_talking = true; // 대화 상태를 true로
    }
}
