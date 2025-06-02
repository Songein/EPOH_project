using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalAI : MonoBehaviour
{
    [SerializeField] private float _escapeSpeed = 3f; // 도망 속도
    [SerializeField] private float _hitForce = 5f; // 타격 시 밀리는 힘
    [SerializeField] private float _hitDuration = 1.5f; // 타격 지속 시간
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private bool _isHit = false;
    private Coroutine _moveCoroutine;
    Vector2 _frontDirection = Vector2.left;
    public LayerMask _obstacleLayer;
    private bool _isInJail;

    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _obstacleLayer = LayerMask.GetMask("Wall", "JailBorder");
        _moveCoroutine = StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        int randomDirection = Random.Range(0, 2);
        if (randomDirection == 0)
        {
            // 오른쪽으로 이동
            _frontDirection = Vector2.right;
            _sr.flipX = true;
        }
        else
        {
            // 왼쪽으로 이동
            _frontDirection = Vector2.left;
            _sr.flipX = false;
        }
        _rb.velocity = _frontDirection * _escapeSpeed;
        yield return new WaitForSeconds(5f);
        _moveCoroutine = StartCoroutine(Move());
    }

    private void FixedUpdate()
    {
        // 전방 장애물 감지
        if (Physics2D.Raycast(transform.position, _frontDirection, 1f, _obstacleLayer) && !_isHit)
        {
            Debug.Log("장애물 감지! 방향 변경");

            // 기존 Move 코루틴 중지
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

            // 반대 방향으로 변경 후 즉시 적용
            _frontDirection *= -1;
            _sr.flipX = !_sr.flipX;
            _rb.velocity = _frontDirection * _escapeSpeed;

            // 새로운 Move 코루틴 시작 (1초 후 방향 전환을 허용)
            _moveCoroutine = StartCoroutine(ResumeMove());
        }
    }

    IEnumerator ResumeMove()
    {
        yield return new WaitForSeconds(3f); // 반대 방향으로 이동 후 1초 대기
        _moveCoroutine = StartCoroutine(Move()); // 원래 이동 패턴 재개
    }

    public void EndMove()
    {
        StopAllCoroutines();
        _isInJail = true;
        _rb.velocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        // Scene에서 Ray 시각화 (디버깅 용)
        if (!_isHit)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.blue;
        }
        
        Gizmos.DrawRay(transform.position, _frontDirection * 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackArea"))
        {
            bool isEnd = FindObjectOfType<JailManager>().IsEnd();
            if (isEnd) return;
            _isHit = true;
            StartCoroutine(AfterHit());
            // 반대 방향으로 밀리기
            Vector2 hitDirection = (transform.position.x > other.transform.position.x) ? Vector2.right : Vector2.left;
            _rb.AddForce(hitDirection * _hitForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator AfterHit()
    {
        yield return new WaitForSeconds(_hitDuration);
        _isHit = false;
    }
}
