using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunawaySkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private List<GameObject> _criminalPrefabList; // 범죄자 프리팹
    [SerializeField] private List<Transform> _criminals;  // 위치를 섞을 오브젝트 리스트
    [SerializeField] private int _shuffleCnt;  // 섞는 횟수
    [SerializeField] private float swapDuration = 0.5f; // 교환 애니메이션 지속 시간
    [SerializeField] private float moveDuration = 1f; // 교환 애니메이션 지속 시간
    public void Activate()
    {
        // 범죄자 생성 및 위치 값 리스트에 저장
        _criminals.Clear();
        foreach (var criminal in _criminalPrefabList)
        {
            GameObject _criminal = Instantiate(criminal);
            _criminals.Add(_criminal.transform);
        }

        StartCoroutine(ShuffleAndMove());
    }

    private IEnumerator ShuffleAndMove()
    {
        // 진짜 범죄자 웃는 애니메이션 실행
        yield return new WaitForSeconds(3f);
        
        BossData bossData = BossManagerNew.Current.bossData;
        // 셔플 먼저 진행
        yield return StartCoroutine(ShuffleObjectsCoroutine(_shuffleCnt));

            // 셔플 진행 후, 맵 내의 랜덤한 위치로 범죄자들 이동
            foreach (var criminal in _criminals)
        {
            Animator animator = criminal.GetComponent<Animator>();
            animator.SetTrigger("Default");

            float randomX = Random.Range(bossData._leftBottom.x, bossData._rightTop.x);
            float randomY = Random.Range(bossData._leftBottom.y, bossData._rightTop.y);
            Vector2 destPos = new Vector2(randomX, randomY);
            yield return StartCoroutine(MovePositions(criminal,criminal.position, destPos, moveDuration));
            // 이동 완료 후에 해당 범죄자와 상호작용 가능하도록 설정

                animator.SetTrigger("Idle");
            criminal.GetComponent<Criminal>().canInteract = true;

        }
    }

    private IEnumerator ShuffleObjectsCoroutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // 두 개의 랜덤한 오브젝트 선택
            int indexA = Random.Range(0, _criminals.Count);
            int indexB = Random.Range(0, _criminals.Count);

            // 같은 오브젝트가 선택되지 않도록 보장
            while (indexA == indexB)
            {
                indexB = Random.Range(0, _criminals.Count);
            }

            // 두 오브젝트 위치 저장
            Vector3 positionA = _criminals[indexA].position;
            Vector3 positionB = _criminals[indexB].position;

            // 코루틴을 이용해 위치 변경 애니메이션 실행
            yield return StartCoroutine(SwapPositions(_criminals[indexA], _criminals[indexB], positionA, positionB, swapDuration));
        }
    }

    private IEnumerator SwapPositions(Transform objA, Transform objB, Vector2 startA, Vector2 startB, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            objA.position = Vector3.Lerp(startA, startB, t);
            objB.position = Vector3.Lerp(startB, startA, t);
            yield return null;
        }
        
        // 최종 위치 설정
        objA.position = startB;
        objB.position = startA;
    }

    private IEnumerator MovePositions(Transform obj, Vector2 startPos, Vector2 destPos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            obj.position = Vector3.Lerp(startPos, destPos, t);
            yield return null;
        }
        
        // 최종 위치 설정
        obj.position = destPos;

        
    }
}
