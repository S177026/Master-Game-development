using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float panSpeed = 5;
    public float panDetect;
    public float rotateAmmount;
    public float rotateSpeed;

    private Quaternion qRotation;
    private float minHeight = 10f;
    private float maxHeight = 100f;

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
        //qRotation = Camera.main.transform.rotation;    
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCamera();
        //Rotatecamera();
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = qRotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            units = GameObject.FindGameObjectsWithTag("Civilian");
            MultiSelect();

            //boxStart = Vector2.zero;
            //boxEnd = Vector2.zero;
        }

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

    public void MultiSelect()
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

    //void MoveCamera()
    //{
    //    //float moveX = Camera.main.transform.position.x;
    //    //float moveY = Camera.main.transform.position.y;
    //    //float moveZ = Camera.main.transform.position.z;

    //    ////float moveX = Input.GetAxis("Horizontal") * panSpeed;
    //    ////float moveY = Camera.main.transform.position.y;
    //    ////float moveZ = Input.GetAxis("Vertical") * panSpeed;

    //    float moveX = Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime;
    //    float moveY = transform.position.y;
    //    float moveZ = Input.GetAxis("Vertical") * panSpeed * Time.deltaTime;

    //    float xPos = Input.mousePosition.x;
    //    float yPos = Input.mousePosition.y;

    //    //if(Input.GetKey(KeyCode.A) || xPos > 0 && xPos < panDetect)
    //    //{
    //    //    moveX -= panSpeed;
    //    //}
    //    //else if(Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - panDetect)
    //    //{
    //    //    moveX += panSpeed;
    //    //}
    //    //if(Input.GetKey(KeyCode.W) || yPos < Screen.height && yPos > Screen.height - panDetect)
    //    //{
    //    //    moveZ += panSpeed;
    //    //}
    //    //else if(Input.GetKey(KeyCode.S) || yPos > 0 && yPos < panDetect)
    //    //{
    //    //    moveZ -= panSpeed;
    //    //}

    //    moveY += Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 20);
    //    moveY = Mathf.Clamp(moveY, minHeight, maxHeight);

    //    Vector3 newPos = new Vector3(moveX, moveY, moveZ);

    //    transform.Translate(newPos);
    //}
    //void Rotatecamera()
    //{
    //    Vector3 origin = Camera.main.transform.eulerAngles;
    //    Vector3 destination = origin;

    //    if (Input.GetMouseButton(2))
    //    {
    //        destination.x -= Input.GetAxis("Mouse Y") * rotateAmmount;
    //        destination.y += Input.GetAxis("Mouse X") * rotateAmmount;

    //        if(destination != origin)
    //        {
    //            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
    //        }
    //    }
    //}

     void OnGUI()
    {
        if(boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectBox, boxTex);
        }
    }
}
