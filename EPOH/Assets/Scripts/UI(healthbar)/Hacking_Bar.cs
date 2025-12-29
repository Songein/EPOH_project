using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hacking_Bar : MonoBehaviour
{
    private Hacking HackingPoint;
    private BossData bossData;
    public Slider HackingSlider;
    // Start is called before the first frame update
    void Start()
    {
        HackingPoint = FindObjectOfType<Hacking>();
        bossData = BossManagerNew.Current.bossData;
        if (bossData != null)
        {
            HackingSlider.maxValue = bossData.hackingGoal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HackingPoint != null && HackingSlider != null)
        {
            HackingSlider.value = HackingPoint._hackingPoint;
        }
    }
}
