using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyBubble", 5f);
    }

    public void DestroyBubble() {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("bubble이 player와 접촉!");
                StartCoroutine(BoostSpeed(playerController));
            }
        }
    }

    private IEnumerator BoostSpeed(PlayerController playerController)
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed *= 4; // 이동 속도 4배 증가
        yield return new WaitForSeconds(5f);
        playerController.moveSpeed = originalSpeed; // 원래 속도로 복원
    }
}
