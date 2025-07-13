using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPosition : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = gameObject.transform.position;
        BossData bossData = BossManagerNew.Current.bossData;

        if (SaveManager.instance.progressId == "Progress_Beginning")
        {
            pos = new(-9.5f, -8.16f, -1f);
        }
        else if (SaveManager.instance.progressId == null) {
            pos = new((bossData._leftBottom.x + bossData._rightTop.x) / 2, bossData._leftBottom.y, -1f);
        }
        else
        {
            pos.x = SaveManager.instance.x;
            pos.y = SaveManager.instance.y;
            pos.z = SaveManager.instance.z;
            gameObject.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
