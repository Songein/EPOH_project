using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

public class HackingForN : MonoBehaviour
{
    private PlayerController pcontrol;
    private BossManagerNew _bossManager;
    public float _hackingPoint;
    private float hackingGoal;

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
            if(BossManagerNew.Current.isGeneralRaid)
                BossManagerNew.Current.ClearBossRaidAsync().Forget();
            else BossManagerNew.Current.ClearFinalBossRaidAsync().Forget();
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
        _text.text = $"{(int)(_hackingPoint/ hackingGoal * 100)}" + "%";
    }

    public bool IsClear()
    {
        return _hackingPoint == hackingGoal;
    }
}
