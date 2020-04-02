using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resize : MonoBehaviour
{

    private static GameObject currSelected;
    //public GameObject input_Length;



    void OnMouseDown()

    {
        Debug.Log("\n============");
        
        if (gameObject.name != "apply")
        {
            currSelected = gameObject;
            Debug.Log("current selected" + gameObject.name);
        }
        else
        {
            Debug.Log("no object selected");
        }
        

    }

    public void change()
    {
        //input_Length.GetComponent<Text>().text = "???????????????????????????";
    }

    void Update()
    {
        
        //if (currSelected != null)
        //{

        //    Debug.Log(currSelected.name);
        //    Debug.Log(currSelected.transform.localScale.x);

        //}
        //else
        //{
        //    Debug.Log("\n============Selected null");
        //}
    }
    public void updateSize()
    {
        Debug.Log("Update");

        float len = float.Parse(GameObject.Find("CamMovementControl").transform.Find("Length").transform.Find("LengthText").GetComponent<Text>().text);
        float wid = float.Parse(GameObject.Find("CamMovementControl").transform.Find("Width").transform.Find("WidthText").GetComponent<Text>().text);
        float hei = float.Parse(GameObject.Find("CamMovementControl").transform.Find("Height").transform.Find("HeightText").GetComponent<Text>().text);
        Debug.Log(len.ToString());
        Debug.Log(wid.ToString());
        Debug.Log(hei.ToString());
        if (currSelected != null)
        {

            currSelected.transform.localScale = new Vector3(len, hei, wid);
        }
    }
}
