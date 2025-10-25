using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office2Stair : MonoBehaviour
{
    public bool isUpstair;
    public Vector3 destination;
    public GameObject canvas;
    private Action OnMove;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            OnMove += StartMove;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
            OnMove = null;
        }
    }

    void Update()
    {
        if(OnMove == null) return;
        if (isUpstair)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnMove?.Invoke();
                OnMove = null;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnMove?.Invoke();
                OnMove = null;
            }
        }
    }

    void StartMove()
    {
        canvas.SetActive(false);
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Animator animator = PlayerController.Instance.GetComponent<Animator>();
        PlayerController.Instance.LockPlayer();
        animator.Play("TeleportStart");
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.transform.position = destination;
        if (isUpstair)
        {
            animator.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.Play("TeleportEnd");
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.UnlockPlayer();
    }
}
