using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HackingForN : MonoBehaviour
{
    private PlayerController pcontrol;
    private BossManagerNew _bossManager;
    public float _hackingPoint;
    private int hackingGoal;

    [SerializeField] private TextMeshProUGUI _text;
    public void Start()
    {
        BossData bossdata = BossManagerNew.Current.bossData;
        pcontrol = FindObjectOfType<PlayerController>();
        _bossManager = FindObjectOfType<BossManagerNew>();
        _bossManager.OnDecreaseHackingPoint += DecreaseHackingPoint;
        _bossManager.OnIncreaseHackingPoint += IncreaseHackingPoint;
        hackingGoal = bossdata.hackingGoal;
        Debug.Log("HackingNeuron시작");
    }
    private void Update()
    {
        if (Input.GetButtonDown("Teleport") && pcontrol.can_teleport == false) {
            BossManagerNew.Current.OnDecreaseHackingPoint?.Invoke(5);
            Debug.Log($"[Cookie] : 플레이어 해킹포인트 5%(텔레포트) 만큼 감소");

        }
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
        if (_hackingPoint + value >= hackingGoal)
        {
            _hackingPoint = hackingGoal;
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
        _text.text = $"{_hackingPoint/ hackingGoal * 100}" + "%";
    }
}
