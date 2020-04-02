using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSphere : MonoBehaviour
{
    public GameObject factory;
    public GameObject orig;
    public void addSphere()
    {
        //GameObject cubeobj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject sphereobj = GameObject.Instantiate(orig);
        //cubeobj.AddComponent < DragObject>();
        //cubeobj.transform.localScale = new Vector3(1, 1, 1);
        //cubeobj.transform.position = new Vector3(0, 0.5f, 0);
        sphereobj.SetActive(true);
        sphereobj.transform.parent = factory.transform;
    }
}
