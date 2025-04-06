using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        // 땅에 닿으면 소리
        if (other.CompareTag("Ground"))
        {
            Debug.Log("땅이랑 부딪힘");
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Knife);
            Destroy(gameObject);
        }
    }
}
