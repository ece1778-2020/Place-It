using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValue : MonoBehaviour
{
    public static int globalValue = 0;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public void Setprefab(int value)
    {
        globalValue = value;
    }
    public static int getprefab()
    {
        return globalValue;
    }
}
