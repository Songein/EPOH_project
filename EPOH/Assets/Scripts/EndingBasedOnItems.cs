using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingBasedOnItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.items_all_collected)
        {
            SceneManager.LoadScene("NormalEnding");
        }
        else
        {
            SceneManager.LoadScene("BadEnding");
        }
    }

}
