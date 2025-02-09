using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCopySkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject firstVirus;
    [SerializeField] private GameObject virusPrefab;

    [SerializeField] private float _afterStartCopy; // 증식하기까지의 시간
    [SerializeField] private int _copyCnt; // 증식 횟수
    [SerializeField] private float _copyDuration; // 증식 간격
    [SerializeField] private float _spawnDistance; // 바이러스 간 스폰 거리

    private BossData bossData;
    private HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();
    
    public void Activate()
    {
        StartCoroutine(SpawnFirstVirus());
    }

    private IEnumerator SpawnFirstVirus()
    {
        bossData = BossManagerNew.Current.bossData;

        // 맵 내 랜덤한 위치에 바이러스 생성
        Vector2 virusPos = GetValidRandomPosition();
        Instantiate(firstVirus, virusPos, Quaternion.identity);
        occupiedPositions.Add(virusPos); // 바이러스 위치 저장
        
        yield return new WaitForSeconds(_afterStartCopy);

        // 해당 바이러스를 기준으로 증식 시작
        StartCoroutine(CopyVirus(virusPos));
    }

    private IEnumerator CopyVirus(Vector3 startPosition)
    {
        Vector2 currentPosition = startPosition;
        
        for (int i = 0; i < _copyCnt; i++)
        {
            yield return new WaitForSeconds(_copyDuration);

            Vector2 spawnPosition;
            int maxAttempts = 10; // 무한 루프 방지를 위한 최대 시도 횟수
            int attempt = 0;

            do
            {
                // 8방향 중 랜덤한 방향 선택
                Vector2 randomDirection = GetRandomDirection();
                spawnPosition = currentPosition + randomDirection * _spawnDistance;
                attempt++;
            }
            while ((occupiedPositions.Contains(spawnPosition) || !CheckAvailPos(spawnPosition)) && attempt < maxAttempts);

            // 만약 유효한 위치를 찾지 못하면 생성하지 않음
            if (occupiedPositions.Contains(spawnPosition) || !CheckAvailPos(spawnPosition))
                continue;

            // 바이러스 생성
            InstantiateVirus(spawnPosition);

            // 다음 생성 위치 업데이트
            currentPosition = spawnPosition;
        }
    }

    private bool CheckAvailPos(Vector2 spawnPosition)
    {
        // 새로운 위치가 맵 밖으로 나간 위치라면 재계산
        if (spawnPosition.x <= bossData._leftBottom.x || spawnPosition.x >= bossData._rightTop.x ||
            spawnPosition.y <= bossData._leftBottom.y || spawnPosition.y >= bossData._rightTop.y)
        {
            return false;
        }

        return true;
    }

    private Vector2 GetRandomDirection()
    {
        // 4방향 벡터 배열
        Vector2[] directions = new Vector2[]
        {
            new Vector2(1, 0),   // 오른쪽
            new Vector2(-1, 0),  // 왼쪽
            new Vector2(0, 1),   // 위쪽
            new Vector2(0, -1),  // 아래쪽
        };

        // 랜덤하게 방향 선택
        return directions[Random.Range(0, directions.Length)];
    }

    private Vector2 GetValidRandomPosition()
    {
        Vector2 position;
        int maxAttempts = 10; // 무한 루프 방지를 위한 최대 시도 횟수
        int attempt = 0;

        do
        {
            position = new Vector2(
                Random.Range(bossData._leftBottom.x, bossData._rightTop.x),
                Random.Range(bossData._leftBottom.y, bossData._rightTop.y)
            );
            attempt++;
        }
        while (occupiedPositions.Contains(position) && attempt < maxAttempts);

        return position;
    }

    private void InstantiateVirus(Vector2 position)
    {
        GameObject virus = Instantiate(virusPrefab, position, Quaternion.identity);
        occupiedPositions.Add(position); // 바이러스 위치 저장
    }
}
