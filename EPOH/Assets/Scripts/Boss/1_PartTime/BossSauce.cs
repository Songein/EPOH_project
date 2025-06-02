using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSauce : MonoBehaviour
{
    public GameObject lidPrefab; // 뚜껑 프리팹
    public GameObject saucePrefab; // 물줄기를 표현하는 프리팹 (Sauce)
    public Transform[] lidSpawnPoints; // 뚜껑 생성 위치 배열
    public float growSpeed = 5f; // 물줄기가 길어지는 속도
    public float maxLength = 15f; // 물줄기의 최대 길이
    public float destroyDelay = 1.0f; // 물줄기가 생성된 후 사라지기까지의 시간
    public float lidToWaterDelay = 1f; // 뚜껑 생성 후 물줄기가 나오기까지의 대기 시간

    private List<GameObject> lids = new List<GameObject>(); // 생성된 뚜껑 오브젝트 리스트
    private bool isEffectActive = false;

    // 버튼 클릭으로 효과 실행
    public void OnButtonClick()
    {
        if (isEffectActive) return;

        isEffectActive = true;
        StartCoroutine(StartEffectSequence());
    }

    private IEnumerator StartEffectSequence()
    {
        // 뚜껑 활성화
        foreach (Transform spawnPoint in lidSpawnPoints)
        {
            GameObject lid = Instantiate(lidPrefab, spawnPoint.position, Quaternion.identity);
            lid.SetActive(true); // 처음에 비활성화 상태였던 뚜껑 활성화
            lids.Add(lid);

            // 뚜껑 애니메이션 시작
            Animator lidAnimator = lid.GetComponent<Animator>();
            if (lidAnimator != null)
            {
                lidAnimator.Play("OpenLid"); // 뚜껑 열리는 애니메이션 실행
            }
        }

        // 뚜껑 생성 후 대기
        yield return new WaitForSeconds(lidToWaterDelay);

        // 물줄기 생성
        foreach (GameObject lid in lids)
        {
            StartCoroutine(CreateWaterStream(lid));
        }
    }

    private IEnumerator CreateWaterStream(GameObject lid)
    {
        // 뚜껑의 아래 중심 위치 계산
        Vector3 spawnPosition = new Vector3(
            lid.transform.position.x,
            lid.transform.position.y - (lid.transform.localScale.y / 2), // 뚜껑의 아래 중심
            lid.transform.position.z
        );
        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Sauce);
        // 물줄기 생성
        GameObject sauce = Instantiate(saucePrefab, spawnPosition, Quaternion.identity);

        // 애니메이션 시작
        Animator sauceAnimator = sauce.GetComponent<Animator>();
        if (sauceAnimator != null)
        {
            sauceAnimator.Play("GrowSauce"); // 물줄기 길어지는 애니메이션 실행
        }

        // 초기 크기를 0으로 설정
        sauce.transform.localScale = new Vector3(1f, 0f, 1f);

        // 물줄기가 점점 아래로 길어지는 애니메이션
        while (sauce.transform.localScale.y < maxLength)
        {
            // 크기 증가 (아래로 확장)
            sauce.transform.localScale += new Vector3(0f, growSpeed * Time.deltaTime, 0f);

            // 위치 고정: 물줄기가 아래로만 길어지도록 보정
            sauce.transform.position = new Vector3(
                sauce.transform.position.x,
                spawnPosition.y - (sauce.transform.localScale.y / 2), // 아래로 확장
                sauce.transform.position.z
            );

            yield return null; // 다음 프레임까지 대기
        }

        // 물줄기 유지 후 삭제
        yield return new WaitForSeconds(destroyDelay);
        Destroy(sauce);

        // 뚜껑 비활성화
        lid.SetActive(false);
    }
}