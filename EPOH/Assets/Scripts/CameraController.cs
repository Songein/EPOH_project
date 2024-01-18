using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    float smoothing = 0.2f;
    public Vector2 minCameraBoundary; // 카메라의 좌하단 범위 제한
    public Vector2 maxCameraBoundary; // 카메라의 우상단 범위 제한

    private void Start(){
        player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z); // 타겟(플레이어)의 위치

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x); // Clamp를 사용해 일정 범위 안의 값이 되도록 수정
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing); // 지정된 위치로 부드럽게 이동
    }
}
