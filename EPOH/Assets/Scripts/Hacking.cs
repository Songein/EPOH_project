using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hacking : MonoBehaviour
{
    private BossManagerNew _bossManager;
    [SerializeField] public float _hackingPoint = 0f; //private -> public 으로 바꿈
    [SerializeField] private TextMeshProUGUI _text;
    public void Start()
    {
        _bossManager = FindObjectOfType<BossManagerNew>();
        _bossManager.OnDecreaseHackingPoint = DecreaseHackingPoint;
        _bossManager.OnIncreaseHackingPoint = IncreaseHackingPoint;
    }
    public float GetHackingPoint()
    {
        return _hackingPoint;
    }

    public void DecreaseHackingPoint(float value)
    {
        if (_hackingPoint - value <= 0)
        {
            _hackingPoint = 0f;
        }
        else
        {
            _hackingPoint -= value;
        }
        Debug.Log($"Hacking Point : -{value} -> {_hackingPoint}");
        UpdateText();
    }

    public void IncreaseHackingPoint(float value)
    {
        if (_hackingPoint + value >= 100)
        {
            _hackingPoint = 100f;
            BossManagerNew.Current.ClearBossRaid();
        }
        else
        {
            _hackingPoint += value;
        }
        Debug.Log($"Hacking Point : +{value} -> {_hackingPoint}");
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = $"{_hackingPoint}" + "%";
    }
}
