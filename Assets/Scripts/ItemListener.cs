using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListener : MonoBehaviour
{
    private static GameObject currGameObject;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void setAsCurrent()
    {
        currGameObject = gameObject;
    }

    public static GameObject getCurr()
    {
        return currGameObject;
    }
}
