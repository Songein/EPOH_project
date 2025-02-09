using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : Attackable
{
    [SerializeField] private InjectionSkill.CodeType _codeType;
    [SerializeField] private float _movingSpeed = 3f;
    [SerializeField] private float _hackPoint = 3f;
    [SerializeField] private bool _isReverse = false;
    private Vector2 _targetPosition;
    private bool _canMove = false;

    private void OnEnable()
    {
        StartCoroutine(MoveCode());
        _targetPosition = new Vector2(BossManagerNew.Instance.bossData._leftBottom.x , transform.position.y);
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movingSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                _canMove = false;
                DestroyCodes();
            }
        }
    }

    IEnumerator MoveCode()
    {
        yield return new WaitForSeconds(_duration);
        if (_isReverse)
        {
            if (_codeType == InjectionSkill.CodeType.Black)
            {
                _canMove = true;
            }
        }
        else
        {
            if (_codeType == InjectionSkill.CodeType.Red)
            {
                _canMove = true;
            }
        }
    }

    private void DestroyCodes()
    {
        Code[] _codes = FindObjectsOfType<Code>();
        foreach (var code in _codes)
        {
            Destroy(code.gameObject);
        }
    }
}
