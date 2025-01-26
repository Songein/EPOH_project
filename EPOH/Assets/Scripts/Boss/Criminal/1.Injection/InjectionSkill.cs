using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionSkill : MonoBehaviour,BossSkillInterface
{
    public enum CodeType
    {
        Red,
        Black
    }
    [SerializeField] private List<GameObject> _codeLists = new List<GameObject>();
    public void Activate()
    {
        //맵 오른쪽 가로에 코드 5개 생성
        float _startY = BossManagerNew.Instance.bossData._rightTop.y;
        float _endY = BossManagerNew.Instance.bossData._leftBottom.y;
        float step = (_startY - _endY) / 5;
        
        Debug.LogWarning($"{_startY}, {_endY}, {step}");

        float _y = _startY;
        foreach (var code in _codeLists)
        {
            Vector2 newPos = new Vector2(BossManagerNew.Instance.bossData._rightTop.x, _y);
            Instantiate(code, newPos,Quaternion.identity);
            _y -= step;
        }
    }
}
