using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionList : MonoBehaviour
{
       public void Move(NavMeshAgent agent, RaycastHit hit)
    {
        agent.destination = hit.point;
        Debug.Log("Moving");
    }

    public void Harvest(NavMeshAgent agent, RaycastHit hit)
    {
        agent.destination = hit.collider.gameObject.transform.position;
        Debug.Log("Is Going To Havest");
    }

    public void SafeZone(NavMeshAgent agent, RaycastHit hit)
    {
        agent.destination = hit.collider.gameObject.transform.position;
        Debug.Log("Is Delivering Shit");
    }
}
