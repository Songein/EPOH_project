using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private static PlayerInteract _instance;

    public static PlayerInteract Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInteract>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(PlayerInteract).Name);
                    _instance = singletonObject.AddComponent<PlayerInteract>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
    
    void Update()
    {
        if (canInteract && Input.GetButtonDown("Interact") && OnInteract != null) // 상호작용
        {
            OnInteract?.Invoke();
            OnInteract = null;
        }
    }
}
