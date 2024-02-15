using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Event4Button : MonoBehaviour
{

    public GameObject dog_request_panel;
    public GameObject widget;
    public GameObject portal;

    private TalkAction action;
    private PlayerController player_controller; 


    void Start()
    {
        action = FindObjectOfType<TalkAction>();
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        portal.SetActive(false);
    }


    public void startEvent4Talk()
    {
        GameManager.instance.boss_num = 0;

        if (GameManager.instance.story_info == 4 && dog_request_panel.activeSelf)
        {
            StartCoroutine(EventStart());
        }
        
    }

    IEnumerator EventStart()
    {
        dog_request_panel.SetActive(false);
        widget.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        player_controller.is_interacting = false;
        action.Action();
        portal.SetActive(true);
    }
}