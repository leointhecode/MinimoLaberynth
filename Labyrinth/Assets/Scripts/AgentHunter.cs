using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentHunter : MonoBehaviour
{

    void Update()
    {

        GameObject point = GameObject.FindGameObjectWithTag("Point");
        GetComponent<NavMeshAgent>().destination = point.transform.position;
    }



}
