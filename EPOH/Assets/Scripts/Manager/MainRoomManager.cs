using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomManager : MonoBehaviour
{
    [System.Serializable]
    class bossObjectInfo
    {
        public string objectName;
        public GameObject itemPrefab;
        public Vector3 spawnPos;
    }

    [SerializeField] private List<bossObjectInfo> bossObjectInfos = new List<bossObjectInfo>();

    void Start()
    {
        for (int boss = 0; boss < bossObjectInfos.Count; boss++)
        {
            if (GameManager.instance.bossClearInfo[boss])
            {
                Instantiate(bossObjectInfos[boss].itemPrefab, bossObjectInfos[boss].spawnPos, Quaternion.identity);
            }
        }
    }
}
