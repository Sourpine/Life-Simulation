using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGenes : MonoBehaviour
{
    public float fullness = 0f;
    public float moveSpeed = 1f;
    public float awareness = 5f;
    public int energy = 50;
    public float eTime = 1f;
    public float eTimer = 0f;
    public bool alive = true;
    public int memoryLifeTime = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eTimer >= eTime)
        {
            energy -= 1;
            eTimer = 0;
        }
        if (energy > 0)
        {
            eTimer += Time.deltaTime;
        }
        else
        {
            eTimer = 0;
            energy = 0;
            alive = false;
        }
        GetComponent<SphereCollider>().radius = awareness;
    }
}
