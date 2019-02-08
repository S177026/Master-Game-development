using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class ObjectInfo : MonoBehaviour
{
    public bool isSelected = false;
    public bool isUnit;

    public GameObject selectionIndicator;

    public string objectName;

    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selectionIndicator.SetActive(isSelected);

        if (isUnit && isSelected)
        {
            Panel.gameObject.SetActive(true);
            Debug.Log("UI Showing");
        }
        else if (!isSelected)
        {
            Panel.gameObject.SetActive(false);

        }
    }
}
