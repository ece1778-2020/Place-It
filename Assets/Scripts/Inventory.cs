using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private static List<GameObject> inventoryItem;

    // Start is called before the first frame update
    void Start()
    {
    //fake
    
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public static GameObject addItem(GameObject go)
    {
        if (inventoryItem == null)
        {
            inventoryItem = new List<GameObject>();
        }
        GameObject item = createItem(go);
        inventoryItem.Add(item);
        return item;
    }

    public static List<GameObject> getItems()
    {
        if (inventoryItem == null)
        {
            inventoryItem = new List<GameObject>();
        }
        return inventoryItem;
    }

    private static GameObject createItem(GameObject gameObject)
    {
        gameObject.AddComponent<SetValue>();
        return gameObject;
    }
}
