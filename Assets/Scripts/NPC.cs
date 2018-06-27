
    using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPC : MonoBehaviour
{

    public Transform[] points;
    public GameObject[] wayPoints;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = wayPoints[Random.Range(0,7)].transform;
        }
     
        
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

       
        agent.destination = points[destPoint].position;

      
      
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        
       
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
