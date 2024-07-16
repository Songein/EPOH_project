using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    IEnumerator WaitForSec(float sec)
    {
        yield return new WaitForSeconds(sec);
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
                case 15:
                    gm.eventFlag = true;
                    Debug.Log("DOG 보스전");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 16:
                    DialogueTrigger.TriggerDialogue(16,16);
                    break;
                case 17:
                    gm.eventFlag = true;
                    Debug.Log("로봇 개 인형 클로즈업");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 18:
                    DialogueTrigger.TriggerDialogue(18,18);
                    break;
                case 19:
                    gm.eventFlag = true;
                    Debug.Log("주인공이 비틀거린다.");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 20:
                    DialogueTrigger.TriggerDialogue(20,20);
                    break;
                case 21:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상");
                    SceneManager.LoadScene("CutScene1");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                default:
                    break;
            }
        }
    }
}
