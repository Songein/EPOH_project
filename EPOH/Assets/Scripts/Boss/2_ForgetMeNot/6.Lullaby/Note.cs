using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Attackable
{
    [SerializeField] private float _movingSpeed = 3f;
    private Vector2 _forwardDirection;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _forwardDirection = -transform.up;
        _rigidbody.AddForce(_forwardDirection.normalized * _movingSpeed, ForceMode2D.Impulse);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Wall") || other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
