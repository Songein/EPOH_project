using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPepper : MonoBehaviour, BossSkillInterface
{
    public Transform player; // 플레이어 게임 오브젝트

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    //pepper 변수
    public GameObject pepper_prefab; //pepper 프리팹
    public GameObject pepper_pop; // pepper 폭발 프리팹
    
    // Start is called before the first frame update
    void Start()
    {
        // Scene의 가장 왼쪽과 오른쪽 좌표를 설정
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));

        player = BossManagerNew.Current.player.transform;
    }

    public void Activate()
    {
        StartCoroutine(Pepper());
    }

    public IEnumerator Pepper()
    {
        int pepper_count = 3; // 총 3번의 pepper을 생성
        int number = 0;

        for (int i = 0; i < pepper_count; i++)
        {
            // pepper 오브젝트가 플레이어 x축 위치로부터 -10, 10 이내에서만 생성되도록 설정
            float minX = Mathf.Max(leftEdge.x, player.position.x - 10f);
            float maxX = Mathf.Min(rightEdge.x, player.position.x + 10f);
            
            // pepper 프리팹 생성
            Vector3 pepper_position = new Vector3(Random.Range(minX, maxX), player.position.y, 0);
            GameObject pepper_object = Instantiate(pepper_prefab, pepper_position, Quaternion.identity); // 프리팹 사용
            if (pepper_object != null)
            {
                Debug.Log("pepper prefab instantiated successfully");
            }
            else
            {
                Debug.Log("Failed to instantiate pepper prefab");
            }
            pepper_object.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            // Animator 컴포넌트를 가져와 pepper_scar 애니메이션 재생
            Animator pepperAnimator = pepper_object.GetComponent<Animator>();
            if (pepperAnimator != null)
            {
                Debug.Log("pepper bottle Animator Runtime Controller: " + pepperAnimator.runtimeAnimatorController.name);
                pepperAnimator.Play("pepper_bottle");
            }
            else
            {
                Debug.Log("pepper Animator Not Found");
            }

            
            yield return new WaitForSeconds(1.1f); // 애니메이션 길이에 맞춰 대기


            Vector3 currentScale = pepper_object.transform.localScale;
            Vector3 currentPosition = pepper_object.transform.position; // 위치 저장
            Destroy(pepper_object);  // 기존 pepper_prefab 오브젝트 삭제

            SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Pepper);
            GameObject pop_object = Instantiate(pepper_pop, currentPosition, Quaternion.identity); // scratch_pop로 교체
            pop_object.transform.localScale = currentScale; // 이전 오브젝트의 크기 유지

            Animator popAnimator = pop_object.GetComponent<Animator>();
            if (popAnimator != null)
            {
                Debug.Log("Pop Animator Runtime Controller: " + popAnimator.runtimeAnimatorController.name);
                popAnimator.Play("pepper_explode"); // pepper_explode 애니메이션 재생
            }

            yield return new WaitForSeconds(popAnimator.GetCurrentAnimatorStateInfo(0).length);

            Destroy(pop_object);
            number ++;
            Debug.Log("스크래치 횟수: " + number);


            // 다음 pepper 생성 전 1초 대기
            if (i < pepper_count - 1)
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
}
