using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListener : MonoBehaviour
{
    private static GameObject selectedGameObject;

    private GameObject currGameObject;
    // Start is called before the first frame update
    void Start()
    {
        //currGameObject = gameObject;    
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void setCurrGameObject(GameObject obj)
    {
        this.currGameObject = obj;
    }


    public void setAsSelected()
    {
        selectedGameObject = currGameObject;
    }

    public static GameObject getSelectedGameObject()
    {
        return selectedGameObject;
    }

    public GameObject getCurrGameObject()
    {
        return currGameObject;
    }
}
