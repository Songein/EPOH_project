using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MainRoomManager : MonoBehaviour
{
    GameManager gm = GameManager.instance;
    // Start is called before the first frame update
    void Start()
    {
        //주인공 행동
        Debug.Log("주인공 클로즈업");
        gm.story_info++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.eventFlag)
        {
            switch (gm.story_info)
            {
                case 2:
                    DialogueTrigger.TriggerDialogue(2,2);
                    break;
                case 3:
                    gm.eventFlag = true;
                    Debug.Log("화면 풀샷");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 4:
                    gm.eventFlag = true;
                    Debug.Log("주인공 비틀거린다.");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 5:
                    DialogueTrigger.TriggerDialogue(5,6);
                    break;
                case 8:
                    DialogueTrigger.TriggerDialogue(8,10);
                    break;
                case 11:
                    gm.eventFlag = true;
                    Debug.Log("DOG 의뢰서 확인");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 12:
                    DialogueTrigger.TriggerDialogue(12,12);
                    break;
                case 13:
                    gm.eventFlag = true;
                    Debug.Log("DOG 의뢰 수락");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 14:
                    DialogueTrigger.TriggerDialogue(14,14);
                    break;
                default:
                    break;
            }
        }
    }
}
