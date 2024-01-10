using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider boss_hp_slider; // boss_hp 슬라이더
    public Slider player_hp_slider; // player_hp 슬라이더
    public Slider hacking_point_slider; // hacking_point 슬라이더

    public BossManager boss_manager; // BossManager 스크립트에 대한 참조

    private void Start()
    {
        // BossManager 스크립트 참조
        boss_manager = FindObjectOfType<BossManager>();
    }

    private void updateSliderValues()
    {
        // BossManager의 변수를 읽어와서 슬라이더 값으로 설정
        boss_hp_slider.value = boss_manager.boss_hp;
        player_hp_slider.value = boss_manager.player_hp;
        hacking_point_slider.value = boss_manager.hacking_point;
    }

    void Update()
    {
        // 매 프레임마다 슬라이더 값을 BossManager의 변수에 맞춰 업데이트
        updateSliderValues();
    }
}

