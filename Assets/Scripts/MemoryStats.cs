using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryStats : MonoBehaviour
{
    public int lifeTime;
    public int roundsAlive = 0;
    public GameObject memoryHost;

    void Start()
    {
        memoryHost = transform.parent.gameObject;
    }
    void Update()
    {
        memoryHost = transform.parent.gameObject;
        lifeTime = memoryHost.GetComponent<StartingGenes>().memoryLifeTime - roundsAlive;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
