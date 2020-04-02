using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCube : MonoBehaviour
{
    public GameObject factory;
    public GameObject orig;
    public void addCube() {
        //GameObject cubeobj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject cubeobj = GameObject.Instantiate(orig);
        //cubeobj.AddComponent < DragObject>();
        //cubeobj.transform.localScale = new Vector3(1, 1, 1);
        //cubeobj.transform.position = new Vector3(0, 0.5f, 0);
        cubeobj.SetActive(true);
        cubeobj.transform.parent = factory.transform;
    }

}
