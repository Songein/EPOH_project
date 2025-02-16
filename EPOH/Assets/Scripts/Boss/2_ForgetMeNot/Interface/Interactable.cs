using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected PlayerInteract _player;
    protected void OnEnable()
    {
        _player = FindObjectOfType<PlayerInteract>();
    }

    //플레이어가 상호작용 할 경우
    public virtual void OnInteract(){}

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.OnInteract += OnInteract;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.OnInteract -= OnInteract;
        }
    }

    protected void OnDisable()
    {
        _player.OnInteract -= OnInteract;
    }
}
