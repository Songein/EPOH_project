using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIBase : MonoBehaviour
{
    public UnityEvent OnClickEvent;
    public UnityEvent OnHoverEvent;
    public UnityEvent OnHoverExitEvent;
    public UnityEvent OnExitEvent;
    public UnityEvent OnForceQuitEvent;
    
    private void Update()
    {
        if (UIManager.Instance.IsAnyUIOpen())
        {
            if (IsTopUI())
            {
                HandleKeyboardInput();
                HandleMouseInput();
            }
        }
    }
    
    public virtual void OnOpen()
    {
        Debug.Log($"#{gameObject.name}이(가) 열렸습니다.");
    }
    
    public virtual void OnOpen(EffectStructure effect)
    {
        if (effect == null)
        {
            return;
        }
        Debug.Log($"#{gameObject.name}이(가) 열렸습니다.");
    }
    
    public virtual void OnClose()
    {
        Debug.Log($"#{gameObject.name}이(가) 닫혔습니다.");
    }
    
    public virtual void HandleKeyboardInput() { }
    public virtual void HandleMouseInput() { }
    
    public bool IsTopUI()
    {
        return UIManager.Instance.GetTopUI() == this;
    }
    
    public void RemoveAllListeners()
    {
        OnHoverEvent.RemoveAllListeners();
        OnClickEvent.RemoveAllListeners();
        OnHoverExitEvent.RemoveAllListeners();
        OnExitEvent.RemoveAllListeners();
        OnForceQuitEvent.RemoveAllListeners();
    }
    
    public void SetButtonSelected(GameObject btn, Color color)
    {
        TextMeshProUGUI tmp = btn.GetComponent<TextMeshProUGUI>();
        tmp.color = color;
    }
    
    public void AddOnClickListener(UnityAction action)
    {
        OnClickEvent.AddListener(action);
    }
}
