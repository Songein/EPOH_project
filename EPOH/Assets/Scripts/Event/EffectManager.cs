using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Action OnEffectEnd;
    
    private static EffectManager _instance;

    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EffectManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(EffectManager).Name);
                    _instance = singletonObject.AddComponent<EffectManager>();
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
}
