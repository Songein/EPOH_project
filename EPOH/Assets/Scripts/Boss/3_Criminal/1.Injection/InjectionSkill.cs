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
    [SerializeField] private List<GameObject> redObjects;    // Red 오브젝트 리스트 (5개)
    [SerializeField] private List<GameObject> blackObjects;  // Black 오브젝트 리스트 (5개)
     private List<GameObject> selectedList;  // 선택된 결과 리스트

    
   // [SerializeField] private List<GameObject> _codeLists = new List<GameObject>();
    private List<GameObject> _codeShuffleLists = new List<GameObject>();
    
    public void Activate()
    {
      
        _codeShuffleLists = ShuffleList();
        //맵 오른쪽 가로에 코드 5개 생성
        float _startY = BossManagerNew.Current.bossData._rightTop.y;
        float _endY = BossManagerNew.Current.bossData._leftBottom.y;
        float step = (_startY - _endY) / 4;

        float _y = _startY;
        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Criminal_Keyboard); //소리
        foreach (var code in _codeShuffleLists)
        {
            Vector2 newPos = new Vector2(BossManagerNew.Current.bossData._rightTop.x, _y);
            Instantiate(code, newPos, Quaternion.identity);
            _y -= step;
        }

        /*
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
        */
    }

    List<GameObject> ShuffleList()
    {
        // 초기화
        selectedList = new List<GameObject>();

        // 5개의 위치 인덱스 생성
        List<int> positions = new List<int> { 0, 1, 2, 3, 4 };

        // 인덱스 섞기 (Shuffle)
        for (int i = 0; i < positions.Count; i++)
        {
            int randomIndex = Random.Range(i, positions.Count);
            int temp = positions[i];
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // 첫 3개는 Red 선택
        for (int i = 0; i < 3; i++)
        {
            selectedList.Add(redObjects[positions[i]]);
        }

        // 나머지 2개는 Black 선택
        for (int i = 3; i < 5; i++)
        {
            selectedList.Add(blackObjects[positions[i]]);
        }

        //빨간색이랑 검은색 섞기
        for (int i = selectedList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (selectedList[i], selectedList[randomIndex]) = (selectedList[randomIndex], selectedList[i]);
        }


        return selectedList;
    }


    /*
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
    */
}
