using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPrefabs : MonoBehaviour
{
    [SerializeField] private float duration;
    // Start is called before the first frame update
    void Start()
    {
        
        if (gameObject.activeSelf)
        {
            StartCoroutine(destroyDelay());
        }
   
    }

    IEnumerator destroyDelay() {

        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

   
}
