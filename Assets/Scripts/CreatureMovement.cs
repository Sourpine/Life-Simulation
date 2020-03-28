using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour
{
    public int energy;
    public float moveSpeed;
    public GameObject target;
    public GameObject memPrefab;
    Transform wanderTarget;
    NavMeshAgent agent;
    Vector3 home;
    public List<Transform> targetMemoryList;
    float rotTime = 3f;
    float rotTimer = 0;
    int rotLocNum = 0;
    public bool hasTarget = false;
    public Transform[] rotLoc;
    public GameObject emptyParent;
    public bool alive;
    public bool isNight = false;
    public float memCheckRange = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        energy = GetComponent<StartingGenes>().energy;
        moveSpeed = GetComponent<StartingGenes>().moveSpeed;
        alive = GetComponent<StartingGenes>().alive;
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (home == null)
        {
            home = transform.position;
            agent = GetComponent<NavMeshAgent>();
        }
        energy = GetComponent<StartingGenes>().energy;
        moveSpeed = GetComponent<StartingGenes>().moveSpeed;
        alive = GetComponent<StartingGenes>().alive;

        rotTimer += Time.deltaTime;
        emptyParent.transform.position = gameObject.transform.position;

        if (hasTarget && alive && !isNight)
        {
            agent.destination = target.transform.position;
            if (rotTimer >= rotTime)
            {
                rotTimer = 0;
            }
        }
        else if (alive && !isNight && targetMemoryList.Count != 0 && targetMemoryList[0].GetComponent<MemoryStats>().justSpawned == false)
        {
            agent.destination = targetMemoryList[0].transform.position;
            Vector3 direction = agent.destination - transform.position;
            if (direction.magnitude <= memCheckRange)
            {

            }
        }
        else if (alive && !isNight) 
        {
            if (rotTimer >= rotTime)
            {
                rotLocNum = Random.Range(0, 8);
                rotTimer = 0;
            }
            wanderTarget = rotLoc[rotLocNum];
            agent.destination = wanderTarget.position;
        }
        else if (alive && isNight)
        {
            if (rotTimer >= rotTime)
            {
                rotTimer = 0;
            }
            agent.destination = home;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "food" && hasTarget == false)
        {
            target = other.gameObject;
            hasTarget = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "food" && hasTarget == false)
        {
            target = other.gameObject;
            hasTarget = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "food")
        {
            GetComponent<StartingGenes>().fullness += 1;
            GameObject mem = Instantiate(memPrefab, collision.gameObject.transform.position, Quaternion.identity);
            mem.transform.parent = gameObject.transform.parent;
            targetMemoryList.Add(mem.transform);
            target = GameObject.Find("Cube (1)");
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            hasTarget = false;
        }
    }
}
