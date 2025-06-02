using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Attackable
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _ballSpeed;
    private Vector3 _lastVelocity;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _lastVelocity = _rigidbody.velocity;
    }
    
    void FixedUpdate()
    {
        var maxSpeed = _ballSpeed * 2f; // 최대 속도 설정
        if (_rigidbody.velocity.magnitude > maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
        }
    }

    void Start()
    {
        // 초기 속도 설정
        Vector2 initialDirection = new Vector2(1, 1).normalized; // 대각선 방향으로 우선 튕겨주기
        _rigidbody.velocity = initialDirection * _ballSpeed;
        //지속시간 후 오브젝트 파괴 코루틴 호출
        StartCoroutine(StopSkill());
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // 땅에 닿으면 소리
        if (other.collider.CompareTag("Ground") || other.collider.CompareTag("OneWayPlatform"))
        {
            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.FMN_Ball);

        }

        base.OnCollisionEnter2D(other);
        var speed = Mathf.Max(_lastVelocity.magnitude, _ballSpeed * 0.5f);;
        var direction = Vector3.Reflect(_lastVelocity.normalized, other.contacts[0].normal);

        _rigidbody.velocity = direction * speed;

    }
}
