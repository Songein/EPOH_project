using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountForNeuron : MonoBehaviour
{
    TriggerNeuron triggerNeuron;
    // Update is called once per frame

    private void Start()
    {
        triggerNeuron = FindObjectOfType<TriggerNeuron>();
    }
    void Update()
    {
        if (triggerNeuron.hitCount == 3) {
            Destroy(gameObject);

            triggerNeuron.hitCount = 0;
        }
    }
}
