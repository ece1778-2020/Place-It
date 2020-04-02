using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamRotate : MonoBehaviour
{
    private float speedMod = 10.0f;//a speed modifier
    private Vector3 rotateValue;
    private Vector3 mousePositionStart;
    private bool isHoldingMouseDown = false;
    private GameObject isHoldingGameObject = null;
    public GameObject yzMoving;
    public GameObject xzMoving;
    public GameObject reset;
    private enum MovingPlane { XY, YZ, XZ, DEFAULT };
    private MovingPlane movingPlane = MovingPlane.XY;
    private Quaternion orginRotation;
    private Vector3 originPosition;
    private void Start()
    {
        transform.LookAt(new Vector3(0, 0, 0));
        orginRotation = transform.rotation;
        originPosition = transform.position;
        yzMoving.GetComponent<Button>().onClick.AddListener(() =>
        {
            updateMovingPlane(MovingPlane.YZ);
        });
        xzMoving.GetComponent<Button>().onClick.AddListener(() =>
        {
            updateMovingPlane(MovingPlane.XZ);
        });
        reset.GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.rotation = orginRotation;
            transform.position = originPosition;
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ObjectComponent")
                {
                    isHoldingMouseDown = false;
                }
                else
                {
                    mousePositionStart = Input.mousePosition;
                    isHoldingMouseDown = true;
                }
            }
            else
            {
                mousePositionStart = Input.mousePosition;
                isHoldingMouseDown = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isHoldingMouseDown)
            {
                isHoldingMouseDown = false;
            }
        }
        if (isHoldingMouseDown)
        {
            float xDiff = Input.mousePosition.x - mousePositionStart.x;
            float yDiff = Input.mousePosition.y - mousePositionStart.y;
           // switch (movingPlane)
           // {
                //case MovingPlane.YZ:
                    transform.RotateAround(new Vector3(0, 0, 0), new Vector3(-1.0f, 0.0f, 0.0f), yDiff);
                    //break;
               // case MovingPlane.XZ:
                    transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0.0f, 1.0f, 0.0f), xDiff);
                    //break;
           // }
            mousePositionStart = Input.mousePosition;
            Debug.Log("RIGHT" + Camera.main.transform.right);
            Debug.Log("FODWARD" + Camera.main.transform.forward);
            Debug.Log("UP" + Camera.main.transform.up);
            Debug.Log("=============");
        }
    }
    private void updateMovingPlane(MovingPlane m)
    {
        movingPlane = m;
    }
}