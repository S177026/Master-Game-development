using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ObjectInfo : MonoBehaviour
{
    public TaskList task;
    public ResourceManager RM;

    GameObject targetNode;

    public ResourceNodeMnanager.ResourceType heldResourcesType;
    public bool isSelected = false;
    public bool isGathering = false;

    public string objectName;

    private NavMeshAgent agent;

    public int heldResource;
    public int maxHeldResource;
    public GameObject itemLimit;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(GatherTick());
    }

    // Update is called once per frame
    void Update()
    {
        if(targetNode == null)
        {
            task = TaskList.Idle;
        }
        if (heldResource >= maxHeldResource)
        {
            isGathering = false;
            itemLimit.SetActive(true);
            task = TaskList.Idle;
        }

        if(Input.GetMouseButtonDown(1) && isSelected)
        {
            RightClick();
        }
    }

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000))
        {
            if(hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
                Debug.Log("Moving");
                task = TaskList.Moving;
            }
            else if(hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                task = TaskList.Gathering;
                Debug.Log("Is Going To Havest");
                targetNode = hit.collider.gameObject;
            }
            else if (hit.collider.tag == "Safe Zone")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                task = TaskList.Deliver;
                Debug.Log("Is Delivering Shit");
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if(hitObject.tag == "Resource" && task == TaskList.Gathering)
        {
            isGathering = true;
            hitObject.GetComponent<ResourceNodeMnanager>().gatheres++;
            heldResourcesType = hitObject.GetComponent<ResourceNodeMnanager>().resourceType;
        }
        else if(hitObject.tag == "Safe Zone")
        {
            if(RM.wood >= RM.maxWood)
            {
                task = TaskList.Idle;
            }
            else
            {
                RM.wood += heldResource;
                heldResource = 0;
                itemLimit.SetActive(false);
                task = TaskList.Idle;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.tag == "Resource")
        {
            isGathering = false;
            hitObject.GetComponent<ResourceNodeMnanager>().gatheres--;
        }
    }
    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering)
            {
                heldResource++;
            }
        }
    }
}
