using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyRain : Attackable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        // 땅에 닿으면 파괴
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
