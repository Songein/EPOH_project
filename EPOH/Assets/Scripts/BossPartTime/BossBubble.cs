using System.Collections;
using UnityEngine;

public class BossBubble : MonoBehaviour
{
    private BossDogController dog; //BossDogController 참조

    private float duration = 5f; // 버블 유지 시간
    private bool isActive = false; // 활성 상태 체크
    private GameObject bubbleContainer;
    public GameObject bubbleObject; 

    private void Awake()
    {
        dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        
    }

    public void Activate()
    {
        // 버블 활성화
        StartCoroutine(Bubble());
    }

    private IEnumerator Bubble()
    {
        bubbleContainer = new GameObject("BubbleContainer");

        float bubbleWidth = Mathf.Abs(dog.spawnRightPoint.x - dog.spawnLeftPoint.x);
        int bubbleCount = Mathf.CeilToInt(bubbleWidth / bubbleObject.transform.localScale.x);
        float bubbleSpacing = bubbleObject.transform.localScale.x;

        for (int i = 0; i < bubbleCount; i++)
        {
            float yOffset = 3.0f;

            Vector3 bubblePosition = new Vector3(
                dog.spawnLeftPoint.x + (i * bubbleSpacing),
                (dog.spawnLeftPoint.y + dog.spawnRightPoint.y) / 2.0f + yOffset,
                (dog.spawnLeftPoint.z + dog.spawnRightPoint.z) / 2.0f
            );

            GameObject bubble = Instantiate(bubbleObject, bubblePosition, Quaternion.identity, bubbleContainer.transform);

            // 버블의 애니메이션 실행
            Animator bubbleAnimator = bubble.GetComponent<Animator>();
            if (bubbleAnimator != null)
            {
                bubbleAnimator.SetTrigger("StartBubble"); 
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
        playerController.moveSpeed *= 2; // 이동 속도 2배 증가
        yield return new WaitForSeconds(duration);
        playerController.moveSpeed = originalSpeed; // 원래 속도로 복원
    }
}
