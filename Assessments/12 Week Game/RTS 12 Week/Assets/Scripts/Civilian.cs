using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Civilian : MonoBehaviour
{
    public TaskList task;
    public ResourceNodeMnanager.ResourceType heldResourcesType;

    public ResourceManager RM;
    
    private ActionList AL;
    private NavMeshAgent agent;

    public bool isSafe;

    GameObject targetNode;
    //public GameObject itemLimit;

    public bool isGathering = false;
    public int heldResource;
    public int maxHeldResource;

    public float radiation;
    public float maxRad = 100;
    public float startRad = 0;
    public Slider radiationBar;
    public Text rLevelDis;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(GatherTick());
        StartCoroutine(RadiationPoision());
        AL = FindObjectOfType<ActionList>();

        radiation = startRad;
        radiationBar.value = radiation;
    }

    // Update is called once per frame
    void Update()
    {
        rLevelDis.text = "Radiation: " + radiation;
        radiationBar.value = radiation;
    
        if (radiation >= 100)
        {
            Destroy(gameObject);
        }

        if (targetNode == null)
        {
            task = TaskList.Idle;
        }
        if (heldResource >= maxHeldResource)
        {
            isGathering = false;
            //itemLimit.SetActive(true);
            task = TaskList.Idle;
        }

        if (Input.GetMouseButtonDown(1) && GetComponent<ObjectInfo>().isSelected)
        {
            RightClick();
        }
    }
    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                AL.Move(agent, hit);
                task = TaskList.Moving;
            }
            else if (hit.collider.tag == "Resource")
            {
                AL.Harvest(agent, hit);
                task = TaskList.Gathering;
                targetNode = hit.collider.gameObject;
            }
            else if (hit.collider.tag == "Safe Zone")
            {
                AL.SafeZone(agent, hit);
                task = TaskList.Deliver;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.tag == "Resource" && task == TaskList.Gathering)
        {
            isGathering = true;
            hitObject.GetComponent<ResourceNodeMnanager>().gatheres++;
            heldResourcesType = hitObject.GetComponent<ResourceNodeMnanager>().resourceType;
        }     
        if (hitObject.tag == "Safe Zone")
        {
            isSafe = true;
            if (heldResourcesType == ResourceNodeMnanager.ResourceType.Wood)
            {
                RM.wood += heldResource;
                heldResource = 0;
                //itemLimit.SetActive(false);
                task = TaskList.Idle;
            }
            else if(heldResourcesType == ResourceNodeMnanager.ResourceType.Herb)
            {
                RM.herb += heldResource;
                heldResource = 0;
                //itemLimit.SetActive(false);
                task = TaskList.Idle;
            }
            else if(heldResourcesType == ResourceNodeMnanager.ResourceType.Gold)
            {
                RM.gold += heldResource;
                heldResource = 0;
                //itemLimit.SetActive(false);
                task = TaskList.Idle;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        if (hitObject.tag == "Safe Zone")
        {
            isSafe = false;
        }
        if (hitObject.tag == "Resource")
        {
            isGathering = false;
            hitObject.GetComponent<ResourceNodeMnanager>().gatheres--;
        }
    }

    IEnumerator RadiationPoision()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isSafe)
            {
                radiation--;
                if(radiation <= 0)
                {
                    radiation = 0;
                }
            }
            else if(!isSafe)
            {
                radiation++;
                if(radiation >= 100)
                {
                    radiation = 100;
                }
            }
        }
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering)
            {
                heldResource ++;
            }
        }
    }
}
