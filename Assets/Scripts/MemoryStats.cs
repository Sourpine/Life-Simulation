using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryStats : MonoBehaviour
{
    public int lifeTime = 1;
    public int roundsAlive = 0;
    public GameObject memoryHost;
    public bool justSpawned = true;

    void Start()
    {
        memoryHost = transform.parent.GetChild(0).transform.gameObject;
    }
    void Update()
    {
        memoryHost = transform.parent.GetChild(0).transform.gameObject;
        lifeTime = memoryHost.GetComponent<StartingGenes>().memoryLifeTime - roundsAlive;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
