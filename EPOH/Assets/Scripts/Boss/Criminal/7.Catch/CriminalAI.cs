using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalAI : MonoBehaviour
{
    [SerializeField] private JailManager _jail;
    [SerializeField] private float _escapeSpeed = 3f; // 도망 속도
    [SerializeField] private float _hitForce = 5f; // 타격 시 밀리는 힘
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private bool _isHit = false;
    private Coroutine moveCoroutine;
    Vector2 _frontDirection = Vector2.left;
    private LayerMask obstacleLayer;

    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        obstacleLayer = LayerMask.GetMask("Wall");
        moveCoroutine = StartCoroutine(Move());
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
        moveCoroutine = StartCoroutine(Move());
    }

    private void FixedUpdate()
    {
        // 전방 장애물 감지
        if (Physics2D.Raycast(transform.position, _frontDirection, 3f, obstacleLayer))
        {
            Debug.Log("장애물 감지! 방향 변경");

            // 기존 Move 코루틴 중지
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }

            // 반대 방향으로 변경 후 즉시 적용
            _frontDirection *= -1;
            _sr.flipX = !_sr.flipX;
            _rb.velocity = _frontDirection * _escapeSpeed;

            // 새로운 Move 코루틴 시작 (1초 후 방향 전환을 허용)
            moveCoroutine = StartCoroutine(ResumeMove());
        }
    }

    IEnumerator ResumeMove()
    {
        yield return new WaitForSeconds(3f); // 반대 방향으로 이동 후 1초 대기
        moveCoroutine = StartCoroutine(Move()); // 원래 이동 패턴 재개
    }

    private void OnDrawGizmos()
    {
        // Scene에서 Ray 시각화 (디버깅 용)
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _frontDirection * 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackArea"))
        {
            // 플레이어 방향으로 반대 밀기
            Vector2 hitDirection = (transform.position - other.transform.position).normalized;
            _rb.AddForce(hitDirection * _hitForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jail") && _isHit)
        {
            // 철창이 낮아진 후에는 감옥을 빠져나올 수 없음
            _rb.velocity = Vector2.zero;
        }
    }
}
