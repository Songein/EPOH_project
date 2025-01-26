using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hacking : MonoBehaviour
{
    [SerializeField] private float _hackingPoint = 0f;
    [SerializeField] private TextMeshProUGUI _text;
    public void OnEnable()
    {
        BossManagerNew.Instance.OnDecreaseHackingPoint += DecreaseHackingPoint;
        BossManagerNew.Instance.OnIncreaseHackingPoint += IncreaseHackingPoint;
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
            Debug.Log($"Hacking Point : -{value} -> {_hackingPoint}");
        }

        UpdateText();
    }

    public void IncreaseHackingPoint(float value)
    {
        if (_hackingPoint + value >= 100)
        {
            _hackingPoint = 100f;
            BossManagerNew.Instance.EndBossRaid();
        }
        else
        {
            _hackingPoint += value;
            Debug.Log($"Hacking Point : +{value} -> {_hackingPoint}");
        }

        UpdateText();
    }
    
    public void OnDisable()
    {
        BossManagerNew.Instance.OnDecreaseHackingPoint -= DecreaseHackingPoint;
        BossManagerNew.Instance.OnIncreaseHackingPoint -= IncreaseHackingPoint;
    }

    public void UpdateText()
    {
        _text.text = $"{_hackingPoint}" + "%";
    }
}
