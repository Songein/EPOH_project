using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorriderManager : MonoBehaviour
{
    [SerializeField] private GameObject Picture; //복도 속 액자
    [SerializeField] private Sprite[] before_pictures;
    [SerializeField] private Sprite[] after_pictures;
    private GameObject player;
    [SerializeField] private Vector3 player_left_pos;
    [SerializeField] private Vector3 player_right_pos;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        if (GameManager.instance.is_back)
        {
            player.transform.position = player_right_pos;
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            player.transform.position = player_left_pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
