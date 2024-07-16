using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutSceneManager : MonoBehaviour
{
    GameManager gm = GameManager.instance;
    
    IEnumerator WaitForSec(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
    
    void Update()
    {
        if (!gm.eventFlag)
        {
            switch (gm.story_info)
            {
                case 22:
                    DialogueTrigger.TriggerDialogue(22,34);
                    break;
                case 35:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상 끝");
                    SceneManager.LoadScene("MainRoom");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 41:
                    DialogueTrigger.TriggerDialogue(41,41);
                    break;
                case 42:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상 끝");
                    SceneManager.LoadScene("MainRoom");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 49:
                    DialogueTrigger.TriggerDialogue(49,49);
                    break;
                case 50:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상");
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                case 51:
                    DialogueTrigger.TriggerDialogue(51,57);
                    break;
                case 58:
                    gm.eventFlag = true;
                    Debug.Log("과거 회상 끝");
                    SceneManager.LoadScene("MainRoom");
                    WaitForSec(2f);
                    gm.story_info++;
                    gm.eventFlag = false;
                    break;
                default :
                    break;
            }
        }
    }
}
