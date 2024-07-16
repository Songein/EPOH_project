using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomManager : MonoBehaviour
{
    private bool eventFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        //주인공 행동
        Debug.Log("주인공 클로즈업");
        GameManager.instance.story_info++;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.story_info == 2 && !eventFlag)
        {
            DialogueTrigger.TriggerDialogue(1,1);
        }
    }
}
