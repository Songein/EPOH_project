using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronActive : MonoBehaviour
{
    [SerializeField] private Vector3[] neuronPosition;
    [SerializeField] private GameObject neuron;
    private HackingForN hacking;
    private float hackingGoal;

    public static NeuronActive NM;

    private void Start()
    {
        BossData bossdata = BossManagerNew.Current.bossData;
        hackingGoal = bossdata.hackingGoal;
        Activate();
    }
    public void Activate()
    {
        StartCoroutine(createNeuron());

    }


    IEnumerator createNeuron()
    {
        HackingForN hacking = FindObjectOfType<HackingForN>();
        while (hacking._hackingPoint < hackingGoal)
        {
            Debug.Log("뉴런 생성 Hacking Goal: " + hackingGoal);
            int randomNum = Random.Range(0, neuronPosition.Length);
            GameObject g_neuron = Instantiate(neuron, neuronPosition[randomNum], Quaternion.identity);
            yield return new WaitForSeconds(10f);
            Destroy(g_neuron);
           
        }

        Debug.Log("뉴런 생성 중단!");

    }

    

}
