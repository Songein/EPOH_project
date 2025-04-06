using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronActive : MonoBehaviour
{
    [SerializeField] private Vector3[] neuronPosition;
    [SerializeField] private GameObject neuron;
    private HackingForN hacking;

    public static NeuronActive NM;

    private void Start()
    {
        Activate();
    }
    public void Activate()
    {
        StartCoroutine(createNeuron());

    }


    IEnumerator createNeuron()
    {
        HackingForN hacking = FindObjectOfType<HackingForN>();
        while (hacking._hackingPoint < hacking.hackingGoal)
        {
            if (hacking._hackingPoint >= hacking.hackingGoal) // 종료 조건 재확인
            {
                Debug.Log("해킹 목표 도달! 뉴런 생성 중단!");
                yield break; // 코루틴 종료
            }

            int randomNum = Random.Range(0, neuronPosition.Length);
            GameObject g_neuron = Instantiate(neuron, neuronPosition[randomNum], Quaternion.identity);
            yield return new WaitForSeconds(10f);
            Destroy(g_neuron);
           
        }

        Debug.Log("뉴런 생성 중단!");

    }

    

}
