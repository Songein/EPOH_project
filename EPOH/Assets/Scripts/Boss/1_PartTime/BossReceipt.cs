using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReceipt : MonoBehaviour, BossSkillInterface
{

    private BossDogController dog; //BossDogController 참조

    public GameObject player; // 플레이어 게임 오브젝트

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    //receipt 오브젝트 변수
    public GameObject receipt_prefab; // 할퀸 자국 프리팹
    public GameObject receipt_pop; // 폭발 프리팹

    private void Awake()
    {
        dog = GameObject.FindWithTag("Boss").GetComponent<BossDogController>();
        player = dog._player;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Scene의 가장 왼쪽과 오른쪽 좌표를 설정
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        
    }

    public void Activate()
    {
        StartCoroutine(Receipt());
    }

    public IEnumerator Receipt()
    {
        // receipt 오브젝트가 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
        float minX = Mathf.Max(leftEdge.x, player.transform.position.x - 10f);
        float maxX = Mathf.Min(rightEdge.x, player.transform.position.x + 10f);

        // receipt 오브젝트 생성
        Vector3 receipt_position = new Vector3(Random.Range(minX, maxX), player.transform.position.y, 0);
        GameObject receipt_object = Instantiate(receipt_prefab, receipt_position, Quaternion.identity); // 프리팹 사용

        if (receipt_object != null)
        {
            Debug.Log("receipt prefab instantiated successfully");
        }
        else
        {
            Debug.Log("Failed to instantiate receipt prefab");
        }
        receipt_object.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        // Animator 컴포넌트를 가져와 receipt 애니메이션 재생
        Animator receiptAnimator = receipt_object.GetComponent<Animator>();
        if (receiptAnimator != null)
        {
            Debug.Log("receipt Animator Runtime Controller: " + receiptAnimator.runtimeAnimatorController.name);
            receiptAnimator.Play("receipt");
        }
        else
        {
            Debug.Log("receipt Animator Not Found");
        }


        // 거리 변화 감지를 위한 변수
        float previousDistanceX = Mathf.Abs(player.transform.position.x - receipt_object.transform.position.x);
        bool wasClose = previousDistanceX <= 3f; // 초기 상태 저장
        Debug.Log("previousDistanceX: " + previousDistanceX);
        // 10초 동안 거리 변화 감지
        float elapsedTime = 0f;
        while (elapsedTime < 10f)
        {
            if (receipt_object == null)
            {
                Debug.Log("receipt_object가 이미 파괴되어 루프 종료");
                yield break;
            }

            float currentDistanceX = Mathf.Abs(player.transform.position.x - receipt_object.transform.position.x);
                Debug.Log("currentDistanceX: " + currentDistanceX);
                // 플레이어와의 거리 변화에 따른 주문벨 소리 크기 변화
                if (/*wasClose &&*/ currentDistanceX > 3f)
                {
                    float originalVolume = SoundManager2.instance.sfxSource.volume;

                    SoundManager2.instance.sfxSource.volume = 0.1f;
                    SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Receipt);
                    SoundManager2.instance.sfxSource.volume = originalVolume; // 원상 복구
                    Debug.Log("주문벨 소리가 작게 들린다");
                    // yield return new WaitForSeconds(0.2f);                   

                    wasClose = false;
                }
                else if (/*!wasClose &&*/ currentDistanceX <= 3f)
                {
                    SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Receipt);
                    Debug.Log("주문벨 소리가 크게 들린다");
                    //yield return new WaitForSeconds(0.2f);

                    wasClose = true;
                }

                /*
                // space 키를 누르면 즉시 receipt 오브젝트 삭제
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("영수증 삭제 시도, 현재 오브젝트: " + receipt_object);
                    if (receipt_object != null)
                    {
                        Destroy(receipt_object);
                        Debug.Log("영수증 삭제 완료");
                    }
                    else
                    {
                        Debug.Log("receipt_object가 null입니다. 삭제 실패");
                    }
                    yield break;
                }
                */


                // 0.2초마다 거리 체크
                yield return new WaitForSeconds(1f);
                elapsedTime += 1f;
            
        }

        if (receipt_object != null)
        {
            Destroy(receipt_object);
        }
      
    }

}
