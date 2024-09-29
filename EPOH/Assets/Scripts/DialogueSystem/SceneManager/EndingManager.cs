using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    GameManager gm = GameManager.instance;
    void Update()
    {
        if (!gm.eventFlag)
        {
            switch (gm.story_info)
            {
                case 91:
                    DialogueTrigger.TriggerDialogue(91, 91);
                    break;
                case 92:
                    gm.eventFlag = true;
                    gm.story_info = 118;
                    gm.eventFlag = false;
                    break;
                case 93:
                    DialogueTrigger.TriggerDialogue(93, 93);
                    break;
                case 94:
                    gm.eventFlag = true;
                    Debug.Log("안내음 후 효과");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 95:
                    DialogueTrigger.TriggerDialogue(95, 95);
                    break;
                case 96:
                    gm.eventFlag = true;
                    Debug.Log("(주인공을 바라보는 포피, 에드가, 올리브)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 97:
                    DialogueTrigger.TriggerDialogue(97, 100);
                    break;
                case 101:
                    gm.eventFlag = true;
                    Debug.Log("(밖으로 나가는 전환)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 102:
                    DialogueTrigger.TriggerDialogue(102, 102);
                    break;
                case 103:
                    gm.eventFlag = true;
                    Debug.Log("(회사 밖 컷씬)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 104:
                    DialogueTrigger.TriggerDialogue(104, 113);
                    break;
                case 114:
                    gm.eventFlag = true;
                    Debug.Log("(효과가 들어갈 것 같음)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 115:
                    DialogueTrigger.TriggerDialogue(115, 115);
                    break;
                case 116:
                    gm.eventFlag = true;
                    gm.story_info = 118;
                    gm.eventFlag = false;
                    break;
                case 117:
                    DialogueTrigger.TriggerDialogue(117, 117);
                    break;
                case 118:
                    gm.eventFlag = true;
                    Debug.Log("(게임 종료)");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
            }
        }
    }
}
