using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f; // 카메라 이동 속도

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 방향키 입력에 따라 카메라의 위치를 조정
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
