using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    private PlayerHealth _player;
    public TextMeshProUGUI healthText;

    void Start()
    {
        _player = FindObjectOfType<PlayerHealth>();
        healthText.text = $"{_player.player_hp}";
        _player.OnHealthChanged += UpdateHealthUI; // 값이 변경될 때마다 실행
    }

    void UpdateHealthUI(float newHealth)
    {
        healthText.text = $"{newHealth}";
    }
}
