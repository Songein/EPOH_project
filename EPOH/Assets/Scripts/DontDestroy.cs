using UnityEngine;


public class DontDestroy : MonoBehaviour
{

    // 파괴되지 않도록 설정
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
