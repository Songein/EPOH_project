using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogScene : MonoBehaviour
{
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject sub_camera;

    private bool camera_move_event1;
    private Vector3 destination = new Vector3(5.8f, -3f, -10f);
    // Start is called before the first frame update
    void Start()
    {
        //메인 카메라(주인공) 비활성화, 서브 카메라(보스) 활성화
        main_camera.SetActive(false);
        sub_camera.SetActive(true);
        //카메라 움직임 이벤트1 실행ㄴ
        camera_move_event1 = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        //카메라 이벤트 1 : 보스룸 입장과 동시에 보스 쪽으로 카메라 빠르게 이동ㄴ 
        if (camera_move_event1)
        {
            if (sub_camera.transform.position.x >= destination.x - 0.1f)
            {
                camera_move_event1 = false;
                //화난 개의 모습을 보여주기
                StartCoroutine(ShowDogAngry());
            }
            else
            {
                sub_camera.transform.position = Vector3.Lerp(sub_camera.transform.position, destination, 0.01f);
            }
        }
        
    }

    IEnumerator ShowDogAngry()
    {
        Debug.Log("그르렁거리는 개");
        //주인공 대사창 실행

        yield return new WaitForSeconds(3f);
        sub_camera.SetActive(false);
        main_camera.SetActive(true);
    }
}
