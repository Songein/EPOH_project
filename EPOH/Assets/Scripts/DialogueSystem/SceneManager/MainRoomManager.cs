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
                case 36:
                    DialogueTrigger.TriggerDialogue(36,36);
                    break;
                case 37:
                    gm.eventFlag = true;
                    Debug.Log("PartTime 의뢰");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 38:
                    gm.eventFlag = true;
                    Debug.Log("사무실에 등장한 다이어리 클로즈업");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 39:
                    DialogueTrigger.TriggerDialogue(39,39);
                    break;
                case 40:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상");
                    SceneManager.LoadScene("CutScene2");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 43:
                    DialogueTrigger.TriggerDialogue(43,43);
                    break;
                case 44:
                    gm.eventFlag = true;
                    Debug.Log("ForgetNotMe 의뢰");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 45:
                    gm.eventFlag = true;
                    Debug.Log("사무실에 등장한 액자 클로즈업");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 46:
                    DialogueTrigger.TriggerDialogue(46,46);
                    break;
                case 47:
                    gm.eventFlag = true;
                    Debug.Log("주인공이 액자로 다가간다.");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 48:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상");
                    SceneManager.LoadScene("CutScene3");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 59:
                    DialogueTrigger.TriggerDialogue(59,59);
                    break;
                case 60:
                    gm.eventFlag = true;
                    Debug.Log("Criminal 의뢰");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 61:
                    DialogueTrigger.TriggerDialogue(61,61);
                    break;
                case 62:
                    gm.eventFlag = true;
                    Debug.Log("비틀거리는 주인공");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 63:
                    DialogueTrigger.TriggerDialogue(63,63);
                    break;
                case 64:
                    gm.eventFlag = true;
                    Debug.Log("(에드거의 초콜릿이 클로즈업 된다)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 65:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상");
                    SceneManager.LoadScene("CutScene3");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 86:
                    DialogueTrigger.TriggerDialogue(86,86);
                    break;
                case 87:
                    gm.eventFlag = true;
                    Debug.Log("(근무 시간 완료)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 88:
                    DialogueTrigger.TriggerDialogue(88,89);
                    break;
                case 90:
                    gm.eventFlag = true;
                    Debug.Log("(오브젝트 챙길지 말지 분기 지점)");
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        Debug.Log("배드 엔딩");
                        gm.story_info = 91;
                        SceneManager.LoadScene("BadEnding");
                    }
                    else if (Input.GetKey(KeyCode.Alpha2))
                    {
                        Debug.Log("노멀 엔딩");
                        gm.story_info = 93;
                        SceneManager.LoadScene("NormalEnding");
                    }
                    else if (Input.GetKey(KeyCode.Alpha3))
                    {
                        Debug.Log("히든 엔딩");
                        gm.story_info = 117;
                        SceneManager.LoadScene("TrueEnding");
                    }
                    gm.eventFlag = false;
                    break;
                default:
                    break;
            }
        }
    }
}
