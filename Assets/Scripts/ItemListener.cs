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
        //selectedGameObject = Instantiate(Resources.Load("Prefabs/MeasureAnchor")) as GameObject;
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
        Debug.Log("SELECTED: "+ItemListener.getSelectedGameObject().name);
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
