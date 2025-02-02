using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    private Vector3 topSpawnPoint; // 카메라 상단에서 Y축 +2 위치
    [SerializeField] private Vector3[] spawnPositions; // X축으로 떨어진 5개 위치
   // [SerializeField] private GameObject machinePrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float moveDuration;
    [SerializeField] private float destroyDelay;
   // [SerializeField] private float growSpeed;
   // [SerializeField] private float maxLength;


    private void Start()
    {
        Camera mainCamera = Camera.main; //Camera.main은 게임 오브젝트가 활성화된 후에야 사용 가능 전역변수 X
        if (mainCamera != null)
        {
            topSpawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(0.01f, 1f, mainCamera.nearClipPlane));
            topSpawnPoint.y += 2; ///[[[[[[[[[[[[[[ 기계 초기 위치 지정 여기(machine)]]]]]]]]]]]]]]]]]
            topSpawnPoint.z = 0;

            // X축으로 K씩 차이나는 위치 K개 생성
            for (int i = 0; i < this.spawnPositions.Length; i++)
            {
                this.spawnPositions[i] = new Vector3(topSpawnPoint.x + i * 3, topSpawnPoint.y, 0);  ///[[[[[[[[기계간 간격 조정은 x축 (i-4) 에서 조절(machine)]]]]]]]
            }
        }

    }
    public void Activate()
    {
        RandomFive(spawnPositions);
    }

    public void RandomFive(Vector3[] spawnPositions)
    {
        // 1.Select five Random places to make the machine
        List<Vector3> shuffledPositions = new List<Vector3>(spawnPositions);

        // 리스트를 랜덤으로 섞기
        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledPositions.Count);
            (shuffledPositions[i], shuffledPositions[randomIndex]) = (shuffledPositions[randomIndex], shuffledPositions[i]); //리스트 안에 있는 두 값을 교환
        }

        // 5개를 선택
        List<Vector3> randomfirstPositions = shuffledPositions.GetRange(0, 5);



        StartCoroutine(ShootLaser(randomfirstPositions));
    }
    private IEnumerator ShootLaser(List<Vector3> randomfirstPositions)
    {
        for (int i = 0; i < randomfirstPositions.Count; i++)
        {
            float ychange = randomfirstPositions[i].y - 0.8f;
            GameObject rain = Instantiate(laserPrefab, new Vector3(randomfirstPositions[i].x, ychange, 0), Quaternion.identity);  // [[[[[[[[[[laser postion 조정]]]]]]]]]

            yield return new WaitForSeconds(3f);
            /*
            // laser가 점점 아래로 길어지는 애니메이션
            while (rain.transform.localScale.y < maxLength)
            {
                // 크기 증가 (아래로 확장)
                rain.transform.localScale += new Vector3(0f, growSpeed * Time.deltaTime, 0f);

                // 위치 고정: laser가 아래로만 길어지도록 보정
                rain.transform.position = new Vector3(
                    rain.transform.position.x,
                     (ychange) - (rain.transform.localScale.y / 2), // 아래로 확장 (초기위치 - 길이 길어지게)
                    rain.transform.position.z
                );

                yield return null; // 다음 프레임까지 대기
            }
            */

            // laser 삭제 후 기계 유지(destroy laser then stay the machine)
            Destroy(rain);
            yield return new WaitForSeconds(destroyDelay);
        }
        
    }






}


