using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour
{
    //보스 개 프리팹
    [SerializeField] public GameObject bossPrefab;
    //보스 개 리스트
    public List<GameObject> bossList = new List<GameObject>();
    //플레이어
    public GameObject _player;
    //플레이어 Rigidbody2D
    public Rigidbody2D _playerRigid;
    
    //보스 개 스폰 포인트
    [SerializeField] public Vector3 spawnLeftPoint; //스폰 포인트(왼쪽)
    [SerializeField] public Vector3 spawnMiddlePoint; //스폰 포인트(중앙)
    [SerializeField] public Vector3 spawnRightPoint; //스폰 포인트(오른쪽)
    
    //보스 개 하위 스킬
    private Howling1 _howling1;
    private Howling2 _howling2;
    
    void Awake()
    {
        //플레이어 할당
        _player = GameObject.FindWithTag("Player");
        _playerRigid = _player.GetComponent<Rigidbody2D>();
    }
    
    public bool IsPlayerRight(GameObject boss)
    {
        
        if (_playerRigid.transform.position.x - boss.transform.position.x >= 0)
        {
            //Debug.Log("right");
            FlipX(boss, false);
            return true;
        }
        
        //Debug.Log("left");
        FlipX(boss, true);
        return false;
    }

    public void FlipX(GameObject boss, bool value)
    {
        boss.GetComponent<SpriteRenderer>().flipX = value;
    }

    public void GenerateDog(Vector2 spawnPoint)
    {
        GameObject boss = Instantiate(bossPrefab, spawnPoint, Quaternion.identity);
        IsPlayerRight(boss);
        bossList.Add(boss);
    }

    public void ActiveSkill(string skillName)
    {
        for (int i = 0; i < bossList.Count; i++)
        {
            switch (skillName)
            {
                case ("Howling1"):
                    bossList[i].GetComponent<Howling1>().Activate();
                    break;
                case ("Howling2"):
                    bossList[i].GetComponent<Howling2>().Activate();
                    break;
                default :
                    break;
            }
        }
    }
    
}
