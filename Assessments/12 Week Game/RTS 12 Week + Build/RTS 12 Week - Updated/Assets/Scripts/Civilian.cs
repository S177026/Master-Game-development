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
    public ObjectInfo playerInfo;

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

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(GatherTick());
        StartCoroutine(RadiationPoision());
        AL = FindObjectOfType<ActionList>();

        Time.timeScale = 1;
        isDead = false;
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
            playerInfo.Panel.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (targetNode == null)
        {
            task = TaskList.Idle;
        }
        if (heldResource >= maxHeldResource)
        {
            isGathering = false;
            task = TaskList.Idle;
        }

        if (Input.GetMouseButtonDown(1) && GetComponent<ObjectInfo>().isSelected)
        {
            RightClick();
        }

        if (isGathering)
        {
            if (heldResourcesType == ResourceNodeMnanager.ResourceType.Wood)
            {
                RM.wood += heldResource;               
            }
            else if (heldResourcesType == ResourceNodeMnanager.ResourceType.Herb)
            {
                RM.herb += heldResource;
            }
            else if (heldResourcesType == ResourceNodeMnanager.ResourceType.Gold)
            {
                RM.gold += heldResource;
            }
            else if (heldResourcesType == ResourceNodeMnanager.ResourceType.Cables)
            {
                RM.cables += heldResource;
            }
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
            yield return new WaitForSeconds(0.10f);
            if (isSafe)
            {
                radiation -= 5;
                if (radiation <= 0)
                {
                    radiation = 0;
                }
            }
            else if (!isSafe)
            {
                radiation++;
                if (radiation >= 100)
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
            yield return new WaitForSeconds(1f);
            if (isGathering)
            {
                heldResource += 1;
            }
        }
    }
}




