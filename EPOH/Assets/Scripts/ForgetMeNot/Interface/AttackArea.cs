using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private PolygonCollider2D _collider; //AttackArea의 collider;
    private float _attackPower; //공격 세기
    
    void Awake()
    {
        //공격 범위 콜라이더 할당
        _collider = GetComponent<PolygonCollider2D>();
    }
    
    //공격 세기 설정 함수
    public void SetAttackPower(float power)
    {
        Debug.Log("[AttackArea] : 공격 세기를 " + power + "로 설정");
        _attackPower = power;
    }
    
    //공격 세기 리턴하는 함수
    public float GetAttackPower()
    {
        return _attackPower;
    }

    //플레이어 이동에 따라 공격범위 뒤집기
    public void Flip()
    {
        var points = _collider.points;
        //플레이어가 오른쪽을 쳐다보고 있으면 collider offset의 x 값을 1, 아니면 -1로 설정
        for (int i = 0; i < points.Length; i++)
        {
            float prevX = points[i].x;
            points[i] = new Vector2(prevX * (-1f), points[i].y);
        }
        _collider.points = points;
    }

    public void Flip(bool value)
    {
        var points = _collider.points;
        float direction = value ? 1f : (-1f);
        //플레이어가 오른쪽을 쳐다보고 있으면 collider offset의 x 값을 1, 아니면 -1로 설정
        for (int i = 0; i < points.Length; i++)
        {
            float prevX = points[i].x;
            points[i] = new Vector2(prevX * direction, points[i].y);
        }
        _collider.points = points;
    }
}
