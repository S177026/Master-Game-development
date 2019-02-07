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
    private ObjectInfo selectedInfo;

    // Start is called before the first frame update
    void Start()
    {
        qRotation = Camera.main.transform.rotation;    
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        Rotatecamera();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = qRotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }
    }

    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if(hit.collider.tag == "Ground")
            {
                selectedObject = null;
                Debug.Log("Ground Click");
            }else if(hit.collider.tag == "Civilian")
            {
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;
                Debug.Log("Civilian Clicked On: " + selectedInfo.objectName);
            }
        
        }
    }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if(Input.GetKey(KeyCode.A) || xPos > 0 && xPos < panDetect)
        {
            moveX -= panSpeed;
        }
        else if(Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - panDetect)
        {
            moveX += panSpeed;
        }
        if(Input.GetKey(KeyCode.W) || yPos < Screen.height && yPos > Screen.height - panDetect)
        {
            moveZ += panSpeed;
        }
        else if(Input.GetKey(KeyCode.S) || yPos > 0 && yPos < panDetect)
        {
            moveZ -= panSpeed;
        }

        moveY += Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 20);
        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);

        Vector3 newPos = new Vector3(moveX, moveY, moveZ);
        Camera.main.transform.position = newPos;
    }
    void Rotatecamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if (Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmmount;

            if(destination != origin)
            {
                Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
            }
        }
    }
}
