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
    private List<GameObject> _codeShuffleLists = new List<GameObject>();
    public void Activate()
    {
        // 리스트 셔플
        _codeShuffleLists = ShuffleList(_codeLists);
        //맵 오른쪽 가로에 코드 5개 생성
        float _startY = BossManagerNew.Current.bossData._rightTop.y;
        float _endY = BossManagerNew.Current.bossData._leftBottom.y;
        float step = (_startY - _endY) / 4;

        float _y = _startY;
        
        foreach (var code in _codeShuffleLists)
        {
            Vector2 newPos = new Vector2(BossManagerNew.Current.bossData._rightTop.x, _y);
            Instantiate(code, newPos,Quaternion.identity);
            _y -= step;
        }
    }
    
    List<GameObject> ShuffleList(List<GameObject> list)
    {
        List<GameObject> newList = new List<GameObject>(list);
        int count = newList.Count;
        
        for (int i = count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (newList[i], newList[randomIndex]) = (newList[randomIndex], newList[i]);
        }
        
        return newList;
    }
}
