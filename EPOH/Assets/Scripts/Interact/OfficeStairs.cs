using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeStairs : MonoBehaviour
{
    public string moveSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerInteract.Instance.canInteract)
        {
            switch (tag)
            {
                case "Interaction":
                    PlayerInteract.Instance.OnInteract = null;
                    PlayerInteract.Instance.OnInteract += () =>
                    {
                        PlayerController.Instance.canMove = false;
                        SceneChanger.Instance.ChangeScene(moveSceneName).Forget();
                    };
                    return;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerInteract>().canInteract)
        {
            PlayerInteract.Instance.OnInteract = null;
        }
    }
}
