using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPosition : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos;
   
        switch (SaveManager.instance.SceneName)
        {
            case "MainRoomTest":
                
                pos = inital_Location.instance.MainRoomTest;
                break;

            case "OfficeRoom1":
                pos = inital_Location.instance.OfficeRoom1;
                break;

            // 다른 씬도 추가 가능
            case "OfficeRoom2":
                pos = inital_Location.instance.OfficeRoom2;
                break;
            case "OfficeRoom3":
                pos = inital_Location.instance.OfficeRoom3;
                break;
            case "OfficeRoom4":
                pos = inital_Location.instance.OfficeRoom4;
                break;
            case "BossRoomDog":
                pos = inital_Location.instance.OfficeRoom1;
                break;
            case "BossRoomPartTime":
                pos = inital_Location.instance.OfficeRoom2;
                break;
            case "BossRoomForgetMeNot":
                pos = inital_Location.instance.OfficeRoom3;
                break;
            case "BossRoomHoa":
                pos = inital_Location.instance.OfficeRoom4;
                break;

            default:
                Debug.LogWarning("알 수 없는 씬 이름: " + SaveManager.instance.SceneName);
                pos = inital_Location.instance.MainRoomTest;
                break;
        }

        gameObject.transform.position = pos;
    }
}
