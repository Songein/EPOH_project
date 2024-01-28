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
        // 최대값을 100으로 가정하여 백분율 계산
        float max_boss_hp = 1000;
        float max_player_hp = 200;
        float max_hacking_point = 200;

        // BossManager의 변수를 읽어와서 슬라이더 값으로 설정
        boss_hp_slider.value = boss_manager.boss_hp / max_boss_hp;
        player_hp_slider.value = boss_manager.player_hp / max_player_hp;
        hacking_point_slider.value = boss_manager.hacking_point / max_hacking_point;
    }

    void Update()
    {
        // 매 프레임마다 슬라이더 값을 BossManager의 변수에 맞춰 업데이트
        updateSliderValues();
    }
}

