using System.Collections;
using UnityEngine;

public class BossBubble : MonoBehaviour, BossSkillInterface
{
    private float duration = 5f; // 버블 유지 시간
    private bool isActive = false; // 활성 상태 체크
    private GameObject bubbleContainer;
    public GameObject bubbleObject;
    private BossData _bossData;
    public void Activate()
    {
        // 버블 활성화
        StartCoroutine(Bubble());
    }

    private IEnumerator Bubble()
    {
        _bossData = BossManagerNew.Current.bossData;
        bubbleContainer = new GameObject("BubbleContainer");

        float bubbleWidth = Mathf.Abs(_bossData._rightTop.x - _bossData._leftBottom.x);
        int bubbleCount = Mathf.CeilToInt(bubbleWidth / bubbleObject.transform.localScale.x);
        float bubbleSpacing = bubbleObject.transform.localScale.x;
        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Bubble); //소리
        for (int i = 0; i < bubbleCount; i++)
        {
            float yOffset = 3.0f;

            Vector3 bubblePosition = new Vector3(
                _bossData._leftBottom.x + (i * bubbleSpacing),
                (_bossData._leftBottom.y + _bossData._rightTop.y) / 2.0f + yOffset,
                (_bossData._leftBottom.z + _bossData._rightTop.z) / 2.0f
            );
          
            GameObject bubble = Instantiate(bubbleObject, bubblePosition, Quaternion.identity, bubbleContainer.transform);

            Collider2D bubbleCollider = bubble.GetComponent<Collider2D>();
            if (bubbleCollider != null)
            {
                bubbleCollider.offset -= new Vector2(0, 5.0f); // Y축으로 Collider를 내림 (값은 조정 가능)
            }

            // 버블의 애니메이션 실행
            Animator bubbleAnimator = bubble.GetComponent<Animator>();
            if (bubbleAnimator != null)
            {
                bubbleAnimator.SetTrigger("bubble1"); 
            }
        }

        gameObject.SetActive(true);
        isActive = true;

        yield return new WaitForSeconds(duration);
        Destroy(bubbleContainer);
        isActive = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                StartCoroutine(BoostSpeed(playerController));
            }
        }
    }

    private IEnumerator BoostSpeed(PlayerController playerController)
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed *= 4; // 이동 속도 4배 증가
        yield return new WaitForSeconds(duration);
        playerController.moveSpeed = originalSpeed; // 원래 속도로 복원
    }
}
