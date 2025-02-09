using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForInvincible : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("PH is entered");
            PlayerHealth pH = collision.GetComponent<PlayerHealth>();
            if (pH == null)
            {
                Debug.Log("pH is null!");
            }

            pH.is_invincible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PH is exited!");
            PlayerHealth pH = collision.GetComponent<PlayerHealth>();

            pH.is_invincible = false;
        }
    }

}
