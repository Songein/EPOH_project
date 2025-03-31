using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailManager : MonoBehaviour
{
    public Transform jailDoor; // 감옥 철창
    public LayerMask prisonerLayer;
    
    public float lowerSpeed = 1f; // 철창이 내려오는 속도
    [SerializeField] private float minHeight ; // 철창이 멈출 최저 높이
    [SerializeField] private float closeHeight ; // 철창이 닫힌 것으로 판단할 높이 기준
    
    private bool isLowering = true;
    public bool jailClosed = false;
    private bool _isSuccess = false;
    private bool _isEnd = false;

    [SerializeField] private float _hackingPoint;
    private GameObject _criminalObj;
    
    private void OnEnable()
    {
        // 활성화되면 천천히 하강
        _isSuccess = false;
        _isEnd = false;
        jailClosed = false;
        StartCoroutine(LowerJailDoor());
    }

    private IEnumerator LowerJailDoor()
    {
        while (jailDoor.position.y > minHeight)
        {
            jailDoor.position -= new Vector3(0, lowerSpeed * Time.deltaTime, 0);
            if (jailDoor.position.y <= closeHeight)
            {
                jailClosed = true;
                Debug.Log("감옥이 범죄자 머리 아래로 내려옴.");
            }
            yield return null;
        }

        // 철창이 최저 높이에 도달하면 감옥 닫힘
        Debug.Log("감옥이 완전히 닫혔습니다!");
        _isEnd = true;
        CheckResult();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & prisonerLayer) != 0)
        {
            if(_isEnd) return;
            GetComponent<SpriteRenderer>().color = Color.magenta;
            if (jailClosed)
            {
                Debug.Log("감옥 안에 범죄자가 들어옴");
                GetComponent<SpriteRenderer>().color = Color.green;
                transform.GetChild(0).gameObject.SetActive(true);
                _isSuccess = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & prisonerLayer) != 0 && !_isEnd)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    private void CheckResult()
    {
        if (_isSuccess)
        {
            BossManagerNew.Current.OnIncreaseHackingPoint?.Invoke(_hackingPoint);
        }
        else
        {
            BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(_hackingPoint);
            _criminalObj = FindObjectOfType<CriminalAI>().gameObject;
            _criminalObj.GetComponent<CriminalAI>().EndMove();
        }

        StartCoroutine(EndSkill());
    }

    IEnumerator EndSkill()
    {
        yield return new WaitForSeconds(2f);
        _criminalObj = FindObjectOfType<CriminalAI>().gameObject;
        _criminalObj.GetComponent<CriminalAI>().EndMove();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Destroy(_criminalObj);
    }

    public bool IsEnd()
    {
        return _isEnd;
    }
}
