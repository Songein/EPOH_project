using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Image boss_hp_fill; // boss_hp fill iamge
    [SerializeField] Image player_hp_fill; // player_hp fill image
    [SerializeField] GameObject hacking_point_fill; // hacking_point fill image
    [SerializeField] TextMeshProUGUI hacking_point_fill_text; // hacking_point fill text

    private Vector3 hacking_point_fill_pos; //hacking_point_fill pos

    public BossManager boss_manager; // BossManager 스크립트에 대한 참조

    private void Start()
    {
        // BossManager 스크립트 참조
        boss_manager = FindObjectOfType<BossManager>();
        //hacking_point_fill_pos
        hacking_point_fill_pos = hacking_point_fill.GetComponent<RectTransform>().anchoredPosition;
    }

    private void updateSliderValues()
    {
        // 최대값을 100으로 가정하여 백분율 계산
        float max_boss_hp = 1000;
        float max_player_hp = 200;
        float max_hacking_point = 200;

        // BossManager의 변수를 읽어와서 슬라이더 값으로 설정
        boss_hp_fill.fillAmount = boss_manager.boss_hp / max_boss_hp;
        player_hp_fill.fillAmount = boss_manager.player_hp / max_player_hp;
        
        //Hack 값 얻기
        float hack_value = boss_manager.hacking_point / max_hacking_point;
        //Hack 값 Int로 변환하여 텍스트로 표시
        //hack_value 값은 0~1 이기에 %로 나타내기 위해서는 100을 곱해줘야 함.
        hacking_point_fill_text.text = (Mathf.RoundToInt(hack_value * 100)) + "%";
        //hack_pos는 47 ~ 107 까지 되어야 하기에, 0~1에 60을 곱해준 값이어야 함.
        float hack_pos = 47 + hack_value * 60;

        hacking_point_fill.GetComponent<RectTransform>().anchoredPosition = new Vector3(hacking_point_fill_pos.x, hack_pos, hacking_point_fill_pos.z);
        //Debug.Log(hacking_point_fill_pos);
    }

    void Update()
    {
        // 매 프레임마다 슬라이더 값을 BossManager의 변수에 맞춰 업데이트
        updateSliderValues();
    }
}

