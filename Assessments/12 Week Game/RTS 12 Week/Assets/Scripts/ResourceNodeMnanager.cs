using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeMnanager : MonoBehaviour
{
    public enum ResourceType { Wood, Herb, Gold, Cables}

    public ResourceType resourceType;

    public float harvestTime;
    public float availableResource;
    public int gatheres;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResourceTick());
    }

    // Update is called once per frame
    void Update()
    {
        if(availableResource <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ResourceGather()
    {
        if (gatheres != 0)
        {
            availableResource -= gatheres;
        }
    }

    IEnumerator ResourceTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ResourceGather();
        }
    }
}
