using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossBubble : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private GameObject bubblePrefab;
    public void Activate() {
        CreateBubble();
    }

    public void CreateBubble() {
        BossData bossData = BossManagerNew.Current.bossData;


        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.PT_Bubble); //소리
        Vector3 bubblePosition = new Vector3((bossData._leftBottom.x + bossData._rightTop.x)/2, bossData._leftBottom.y +4.2f,0);
            Instantiate(bubblePrefab, bubblePosition, Quaternion.identity);    
        

    }

}
