using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA1 : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public GameObject objectiu;
    void Start()
    {        
        
    }

    void Update()
    {
        NavMeshAgent.destination = objectiu.transform.position;
    }
}
