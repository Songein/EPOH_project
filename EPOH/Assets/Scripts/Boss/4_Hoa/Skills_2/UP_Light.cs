using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_Light : MonoBehaviour, BossSkillInterface
{


    private Vector3 topSpawnPoint; // 카메라 상단에서 Y축 +2 위치
    [SerializeField] private Vector3[] spawnPositions; // X축으로 떨어진 5개 위치
   // [SerializeField] private Vector3 SpawnPoint;
    [SerializeField] private GameObject machinePrefab;
    [SerializeField] private GameObject YmachinePrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float moveDuration;
    [SerializeField] private float destroyDelay;
  //  [SerializeField] private float growSpeed;
   // [SerializeField] private float maxLength;
    

    private void Start()
    {

        // topSpawnPoint = new Vector3(-25f, 12f,0f);


    }
    public void Activate()
    {
        //Vector3 topSpawnPoint = SpawnPoint;
        BossData bossData = BossManagerNew.Current.bossData;

        // X축으로 K씩 차이나는 위치 K개 생성
        for (int i = 0; i < this.spawnPositions.Length; i++)
        {
            float xPos = Mathf.Lerp(bossData._leftBottom.x +2, bossData._rightTop.x -2, (float)i / (spawnPositions.Length - 1));
            this.spawnPositions[i] = new Vector3(xPos, bossData._rightTop.y + 2.5f, 0);  ///[[[[[[[[기계간 간격 조정은 x축 (i-4) 에서 조절(machine)]]]]]]]
        }
        RandomThree(spawnPositions);
    }

    public void RandomThree(Vector3[] spawnPositions)
    {
        // 1.Select five Random places to make the machine
        List<Vector3> shuffledPositions = new List<Vector3>(spawnPositions);

        // 리스트를 랜덤으로 섞기
        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledPositions.Count);
            (shuffledPositions[i], shuffledPositions[randomIndex]) = (shuffledPositions[randomIndex], shuffledPositions[i]); //리스트 안에 있는 두 값을 교환
        }

        // 3개를 선택
      //  List<Vector3> randomfirstPositions = shuffledPositions.GetRange(0, 3);



        GetDownFiveLaser(shuffledPositions);
    }

    private void GetDownFiveLaser(List<Vector3> shuffledPositions)
    {

            for (int i = 0; i < 3; i++)
            {
                GameObject Ymachine = Instantiate(YmachinePrefab, shuffledPositions[i], Quaternion.identity);   //In 5 places, create the laser (randomly)
                Vector3 endPosition = new Vector3(shuffledPositions[i].x, shuffledPositions[i].y - 3f, 0);
                StartCoroutine(MoveYMachine(Ymachine, shuffledPositions[i], endPosition, moveDuration));
        }
            for (int i = 3; i < shuffledPositions.Count; i++)
            {
                GameObject Nmachine = Instantiate(machinePrefab, shuffledPositions[i], Quaternion.identity);   //In 5 places, create the laser (randomly)

                Vector3 endPosition = new Vector3(shuffledPositions[i].x, shuffledPositions[i].y - 3f, 0);  // [[[[[[[[[y축 조정은 여기서(machine)]]]]]]]]]]]]]]]

                StartCoroutine(MoveMachine(Nmachine, shuffledPositions[i], endPosition, moveDuration));  //2. Get the five machine and move them down
            }
        
    }

    private IEnumerator MoveYMachine(GameObject machine, Vector3 startPosition, Vector3 endPosition, float moveDuration) //이동만 하는거
    {
        float timeElapsed = 0f; // 경과 시간

        // 1초 동안 천천히 이동
        while (timeElapsed < moveDuration)
        {
            machine.transform.position = Vector3.Lerp(startPosition, endPosition, timeElapsed / moveDuration); // Lerp로 보간
            timeElapsed += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 정확한 최종 위치 설정
        machine.transform.position = endPosition;
    }

        private IEnumerator MoveMachine(GameObject machine, Vector3 startPosition, Vector3 endPosition, float moveDuration) //이동한 뒤 쏘는거
    {
        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.Hoa_Light); //소리
        float timeElapsed = 0f; // 경과 시간

        // 1초 동안 천천히 이동
        while (timeElapsed < moveDuration)
        {
            machine.transform.position = Vector3.Lerp(startPosition, endPosition, timeElapsed / moveDuration); // Lerp로 보간
            timeElapsed += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 정확한 최종 위치 설정
        machine.transform.position = endPosition;


        StartCoroutine(ShootLaser(endPosition));  // 3.Shoot the laser

    }
    
    private IEnumerator ShootLaser(Vector3 endPosition)
    {
        //yield return new WaitForSeconds(1f);

        float ychange = endPosition.y;
        GameObject laser = Instantiate(laserPrefab, new Vector3(endPosition.x, ychange, 0), Quaternion.identity);  // [[[[[[[[[[laser postion 조정]]]]]]]]]


        /*
        // laser가 점점 아래로 길어지는 애니메이션
        while (laser.transform.localScale.y < maxLength)
        {
            // 크기 증가 (아래로 확장)
            laser.transform.localScale += new Vector3(0f, growSpeed * Time.deltaTime, 0f);

            // 위치 고정: laser가 아래로만 길어지도록 보정
            laser.transform.position = new Vector3(
                laser.transform.position.x,
                 (ychange) - (laser.transform.localScale.y / 2), // 아래로 확장 (초기위치 - 길이 길어지게)
                laser.transform.position.z
            );

            yield return null; // 다음 프레임까지 대기
        }
        */
        yield return new WaitForSeconds(1f);

        // laser 삭제 후 기계 유지(destroy laser then stay the machine)
        Destroy(laser);
        yield return new WaitForSeconds(destroyDelay);
        StartCoroutine(MoveUpMachines());
    }

    private IEnumerator MoveUpMachines()
    {
        //4.Move up the machines back again(Come up "All together" code)
        // "machine" 태그가 있는 모든 게임 오브젝트를 찾음
        GameObject[] machines = GameObject.FindGameObjectsWithTag("HoaMachine");

        // 모든 machine에 대해 병렬로 이동 시작
        List<Coroutine> moveCoroutines = new List<Coroutine>();

        foreach (GameObject machine in machines)
        {
            // 각 machine에 대해 코루틴을 별도로 시작
            moveCoroutines.Add(StartCoroutine(MoveUpMachine(machine)));
        }

        // 모든 코루틴이 끝날 때까지 기다림
        foreach (Coroutine coroutine in moveCoroutines)
        {
            yield return coroutine;

        }

        BossManagerNew.Current.OnSkillEnd?.Invoke();
    }

    private IEnumerator MoveUpMachine(GameObject machine)
    {
        //4.Move up the machines back again(Making them up code)
        Vector3 startPosition = machine.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 6, startPosition.z); // [[[[[[[[[[[[[[올라가는 y축 조정은 여기서(machine)]]]]]]]]]]]]

        float timeElapsed = 0f;

        // 1초 동안 천천히 이동
        while (timeElapsed < moveDuration)
        {
            machine.transform.position = Vector3.Lerp(startPosition, endPosition, timeElapsed / moveDuration);  //[[[ 올라가는 시간은 moveDuration }]]
            timeElapsed += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 정확한 최종 위치 설정
        machine.transform.position = endPosition;

        Destroy(machine); // 5. Destroy the machine to be not shown in the screen
    }





}
