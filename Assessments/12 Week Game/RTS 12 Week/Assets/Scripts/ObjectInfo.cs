using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class ObjectInfo : MonoBehaviour
{
    public CanvasGroup r_Panel;
    public bool isSelected = false;
    public bool isUnit;

    public GameObject selectionIndicator;

    public string objectName;

    // Start is called before the first frame update
    void Start()
    {
        r_Panel = GameObject.Find("UnitPanel").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        selectionIndicator.SetActive(isSelected);

        if (isUnit && isSelected)
        {
            r_Panel.alpha = 1;
            r_Panel.blocksRaycasts = true;
        }
        else if (!isSelected)
        {
            r_Panel.alpha = 0;
            r_Panel.blocksRaycasts = false;         
        }
    }
}
