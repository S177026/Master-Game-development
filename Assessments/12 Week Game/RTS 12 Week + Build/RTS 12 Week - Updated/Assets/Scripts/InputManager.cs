using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject selectedObject;
    public Texture boxTex;
    private ObjectInfo selectedInfo;
    private Rect selectBox;

    private Vector2 boxStart;
    private Vector2 boxEnd;


    private GameObject[] units;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        /*if (Input.GetMouseButtonUp(0))
        {
            units = GameObject.FindGameObjectsWithTag("Civilian");
            MultiSelect();
        }
        */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedInfo.isSelected = false;

            units = GameObject.FindGameObjectsWithTag("Civilian"); //Grabs all selectable objects

            foreach (GameObject unit in units) //For each selectable object
            {
                unit.GetComponent<ObjectInfo>().isSelected = false; //Deselects all selectable objects
            }
        }

        if(Input.GetMouseButton(0) && boxStart == Vector2.zero)
        {
            boxStart = Input.mousePosition;
        }else if(Input.GetMouseButton(0) && boxStart != Vector2.zero)
        {
            boxEnd = Input.mousePosition;
        }

        selectBox = new Rect(boxStart.x, Screen.height - boxStart.y, boxEnd.x - boxStart.x, -1 * ((Screen.height - boxStart.y) - (Screen.height - boxEnd.y)));
    }

    /*public void MultiSelect()
    {
        foreach(GameObject unit in units)
        {
            if (unit.GetComponent<ObjectInfo>().isUnit)
            {
                Vector2 unitPos = Camera.main.WorldToScreenPoint(unit.transform.position);

                if (selectBox.Contains(unitPos, true))
                {
                    unit.GetComponent<ObjectInfo>().isSelected = true;
                }
            }
        }
        boxStart = Vector2.zero;
        boxEnd = Vector2.zero;
    }
    */

    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "Ground")
            {
                selectedInfo.isSelected = false;
                //selectedInfo = null;
                Debug.Log("Ground Click");

                units = GameObject.FindGameObjectsWithTag("Civilian"); //Grabs all selectable objects

                foreach (GameObject unit in units) //For each selectable object
                {
                    unit.GetComponent<ObjectInfo>().isSelected = false; //Deselects all selectable objects
                }
                selectedInfo = null;
            }
            else if(hit.collider.tag == "Civilian")
            {
                units = GameObject.FindGameObjectsWithTag("Civilian"); //Grabs all selectable objects

                foreach (GameObject unit in units) //For each selectable object
                {
                    unit.GetComponent<ObjectInfo>().isSelected = false; //Deselects all selectable objects
                }

                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;
                Debug.Log("Civilian Clicked On: " + selectedInfo.objectName);
            }  
        }
    }


     /*void OnGUI()
    {
        if(boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectBox, boxTex);
        }
    }
    */
}
