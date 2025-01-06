using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //인스턴스화
    public static PlayerInteract instance;
    
    //플레이어 상호작용
    private GameObject interact_obj; //플레이어가 상호작용할 오브젝트
    public bool is_interacting = false; //플레이어가 상호작용 중인지
    private Interaction interaction; //플레이어가 상호작용할 오브젝트에 부착된 Interact 스크립트

    //플레이어 대화
    public bool is_talking = false;
    private TalkAction talk_action;
    
    //보스 맵 내 상호작용 패턴
    public Action OnInteract;

    void Awake()
    {
        PlayerInteract.instance = this;
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Interact")) // 상호작용
        {
            if (is_talking) // 대화 중이면
            {
                talk_action.Action(); //다음 대화
            }
            else if (interact_obj != null) // 대화 중이 아니고 상호작용 할 오브젝트가 있을 경우
            {
                Debug.Log(interact_obj.name + "과 상호작용 시작");
                interaction.Interact();
                is_interacting = true;
            }
            else OnInteract?.Invoke();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //트리거 충돌한 오브젝트가 Interaction 태그를 갖고 있는 오브젝트일 경우
        if (other.CompareTag("Interaction"))
        {
            //상호작용 할 오브젝트에 트리거 충돌 오브젝트를 할당
            interact_obj = other.gameObject;
            interaction = interact_obj.GetComponent<Interaction>(); // 충돌한 오브젝트의 Interaction 할당
            Debug.Log("[PlayerController] : " + other.name + "과 상호작용 가능");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        //트리거 충돌 오브젝트에게서 멀어질 때
        if (interact_obj != null)
        {
            //상호작용 할 오브젝트에 null 할당
            interact_obj = null;
            Debug.Log("[PlayerController] : " + other.name + "과 상호작용 불가능");
        }
        
    }
}
