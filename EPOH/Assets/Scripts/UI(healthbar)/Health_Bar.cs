using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    public Slider healthSlider;              // 연결할 슬라이더
    private PlayerHealth playerHealth;        // 참조할 PlayerHealth 스크립트

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.player_hp;
        }
    }
}
