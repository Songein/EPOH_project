using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCollider : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchColliders());


    }

    public IEnumerator SwitchColliders()
    {

        PolygonCollider2D[] colliders = GetComponents<PolygonCollider2D>();
    
        colliders[0].enabled = true;


        yield return new WaitForSeconds(0.8f); //0.8초 지나면


        colliders[0].enabled = false; //collider 조절
        colliders[1].enabled = true;
    }


}
