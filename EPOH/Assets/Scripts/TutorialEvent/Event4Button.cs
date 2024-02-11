using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Event4Button : MonoBehaviour
{

    public GameObject dog_request_panel;

    public Button accept_button;

    TalkAction action;

    public PlayerController player_controller; 

    [SerializeField] bool scroll_close = false; // 스크롤 닫힘 확인


    void Start()
    {
        action = FindObjectOfType<TalkAction>();
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    public void startEvent4Talk()
    {

        if (GameManager.instance.story_info == 4 && !scroll_close)
        {
            scroll_close = true;
            dog_request_panel.SetActive(false);
            player_controller.is_interacting = false;
            Debug.Log("1player_controller.is_talking: " + player_controller.is_talking);
            Debug.Log("1player_controller.is_interacting: " + player_controller.is_interacting);
            
            
            action.Action();

            player_controller.is_interacting = false;
            Debug.Log("2player_controller.is_talking: " + player_controller.is_talking);
            Debug.Log("2player_controller.is_interacting: " + player_controller.is_interacting);
            
            
            
        }
        
    }
}