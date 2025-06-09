using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(UIManager).Name);
                    _instance = singletonObject.AddComponent<UIManager>();
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
    
    //여러 UI 창을 관리하기 위해 스택 이용
    private Stack<UIBase> uiStack = new Stack<UIBase>();
    public UIBase topUI;
    public UIBase requestUI;
    public UIBase popUpUI;
    public UIBase dialogueUI;
    public UIBase takeObjectUI;
    public UIBase cutSceneUI;
    
    private void Update()
    {
        if (IsAnyUIOpen())
        {
            //플레이어 Lock
            LockPlayer();
        }
    }
    
    public void OpenUI(UIBase ui)
    {
        if (ui == null) return;

        // 스택에 추가하고 UI를 활성화
        // 상호작용 금지
        LockPlayer();
        uiStack.Push(ui);
        topUI = ui;
        ui.OnOpen();
    }

    public void OpenUI(UIBase ui, EffectStructure effect)
    {
        if (ui == null) return;
        
        if (effect == null)
        {
            return;
        }
        
        // 스택에 추가하고 UI를 활성화
        // 상호작용 금지
        LockPlayer();
        uiStack.Push(ui);
        topUI = ui;
        ui.OnOpen(effect);
    }
    
    public void CloseTopUI()
    {
        if (uiStack.Count == 0) return;

        UIBase topUI = uiStack.Pop();
        topUI.OnClose();
        
        if (uiStack.Count > 0)
        {
            this.topUI = uiStack.Peek();
            LockPlayer();
        }
        else
        {
            this.topUI = null;
            UnLockPlayer();
        }
    }
    
    public void CloseAllUI()
    {
        while (uiStack.Count > 0)
        {
            CloseTopUI();
        }
    }
    
    public UIBase GetTopUI()
    {
        if (uiStack.Count == 0) return null;
        return uiStack.Peek();
    }
    
    public bool IsAnyUIOpen()
    {
        return uiStack.Count > 0;
    }
    
    public bool IsUIOpen(UIBase ui)
    {
        return uiStack.Contains(ui);
    }
    
    public void LockPlayer()
    {
        PlayerController.Instance.canMove = false;
        PlayerInteract.Instance.canInteract = false;
    }

    public void LockInteraction()
    {
        PlayerInteract.Instance.canInteract = false;
    }

    public void UnLockPlayer()
    {
        PlayerController.Instance.canMove = true;
        PlayerInteract.Instance.canInteract = true;
    }
    
    public Sprite GetSpriteFromFilePath(string path)
    {
        Sprite resultSprite = Resources.Load<Sprite>(path);

        if (resultSprite == null)
        {
            Debug.Log(path + "에 이미지 리소스가 존재하지 않습니다.");
            return null;
        }

        return resultSprite;
    }
}
