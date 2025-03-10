using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //인스턴스화
    public static PlayerInteract instance;
    
    //플레이어 상호작용
    public bool canInteract = true;
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
        if (Input.GetButtonDown("Interact") && OnInteract != null) // 상호작용
        {
            OnInteract?.Invoke();
            OnInteract = null;
        }
    }
}
