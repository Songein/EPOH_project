using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lullaby : MonoBehaviour
{
    [SerializeField] private List<GameObject> _notePrefabList = new List<GameObject>();
    [SerializeField] private float _duration = 20f;
    [SerializeField] private int _noteCnt = 5;
    [SerializeField] private float _noteDistance;
    [SerializeField] private float _generateDuration = 5f;
    
    void Start()
    {
        // 음표 생성
        StartCoroutine(GenerateNote());
        // 종료 함수 호출
        StartCoroutine(EndSkill());
    }

    IEnumerator EndSkill()
    {
        yield return new WaitForSeconds(_duration);
        BossManagerNew.Current.OnSkillEnd?.Invoke();
        Destroy(gameObject);
    }

    IEnumerator GenerateNote()
    {
        // 현재 위치를 기준으로 아래 방향 180도를 음표 개수 만큼 동일한 간격으로 랜덤 생성

        int intervalAngle = 180 / (_noteCnt-1);
        float height = GetComponent<Renderer>().bounds.size.y; // Lullaby 높이

        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.FMN_Lullaby);
        for (int i=0; i<_noteCnt; i++)
        {
            int randomNoteIndex = Random.Range(0, _notePrefabList.Count);
            GameObject prefab = _notePrefabList[randomNoteIndex];
            int angle = -90 + i * intervalAngle;
            Vector2 forwardDirection = Quaternion.Euler(0, 0, angle) * Vector2.down;
            Vector2 spawnPosition = (Vector2)transform.position + forwardDirection * _noteDistance + new Vector2(0,-height/2);
            Instantiate(prefab, spawnPosition, Quaternion.Euler(0, 0, angle));
        }
        yield return new WaitForSeconds(_generateDuration);
        StartCoroutine(GenerateNote());
    }
}
