using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronActive : MonoBehaviour
{
    [SerializeField] private Vector3[] neuronPosition;
    [SerializeField] private GameObject neuron;
    private HackingForN hacking;



    public void Activate()
    {   
        StartCoroutine(createNeuron());


    }

    IEnumerator createNeuron()
    {
        HackingForN hacking = FindObjectOfType<HackingForN>();
        while (hacking._hackingPoint < hacking.hackingGoal)
        {
            int randomNum = Random.Range(0, neuronPosition.Length);
            GameObject g_neuron = Instantiate(neuron, neuronPosition[randomNum], Quaternion.identity);
            yield return new WaitForSeconds(10f);
            Destroy(g_neuron);
           
        }
        
    }

    

}
