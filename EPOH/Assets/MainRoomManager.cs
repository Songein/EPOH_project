using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomManager : MonoBehaviour
{
    [SerializeField] private Vector3 player_left_pos;
    [SerializeField] private Vector3 player_right_pos;
    [SerializeField] private GameObject[] trigger_objs;
    [SerializeField] private GameObject[] cut_scenes;
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject sub_camera;
    [SerializeField] private Vector3 camera_dest;

    private TalkAction talk_action;
    private GameObject player;

    private bool robot_dog_event1_start = false;
    private bool robot_dog_event2_start = false;
    private bool robot_dog_cut_scene_start = false;
    private bool camera_move = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        talk_action = FindObjectOfType<TalkAction>();
        if (GameManager.instance.is_back)
        {
            player.transform.position = player_right_pos;
            player.GetComponent<SpriteRenderer>().flipX = true;
            
            if (GameManager.instance.story_info == 11)
            {
                trigger_objs[0].SetActive(true);
                talk_action.Action();
            }
        }
        else
        {
            player.transform.position = player_left_pos;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.story_info == 11 && talk_action.talk_index == 2 && !robot_dog_event1_start)
        {
            robot_dog_event1_start = true;
            main_camera.SetActive(false);
            sub_camera.SetActive(true);
        }

        if (robot_dog_event1_start && !camera_move)
        {
            if (Mathf.Abs(camera_dest.x - sub_camera.transform.position.x) <= 0.1f)
            {
                camera_move = true;
                Debug.Log(Mathf.Abs(camera_dest.x - sub_camera.transform.position.x));
            }
            else
            {
                //로봇개를 비춤.s
                sub_camera.transform.position = Vector3.Lerp(sub_camera.transform.position, camera_dest, 0.05f);
            }
            
        }

        if (GameManager.instance.story_info == 11 && talk_action.talk_index == 3 && !robot_dog_event2_start)
        {
            robot_dog_event2_start = true;
            //주인공 대화창의 초상화 부분 변경(비틀거리는 주인공)
        }

        if (GameManager.instance.story_info == 12 && !robot_dog_cut_scene_start)
        {
            robot_dog_cut_scene_start = true;
            cut_scenes[0].SetActive(true);
            talk_action.Action();
        }

        if (GameManager.instance.story_info >= 13 && GameManager.instance.story_info <= 24 && !talk_action.is_talking)
        {
            talk_action.Action();
        }

        if (GameManager.instance.story_info == 25 && !talk_action.is_talking)
        {
            sub_camera.SetActive(false);
            main_camera.SetActive(true);
            cut_scenes[0].SetActive(false);
            talk_action.Action();
        }
    }
}
