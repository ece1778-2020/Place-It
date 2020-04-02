using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveObjToPlaceIt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveObj()
    {
        GameObject obj = GameObject.Find("ObjFactory");
        //foreach (Transform child in obj.transform)
        //{
        //    Destroy(child.GetComponent<BoxCollider>());
        //}

        //MeshRenderer[] r = obj.GetComponentsInChildren<MeshRenderer>();
        DontDestroyOnLoad(obj);
        Inventory.drawItObj = obj; 
        SceneManager.LoadScene("PlaceIt");
    }
}
