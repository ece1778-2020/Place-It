using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    private static List<GameObject> inventoryItem;
    public GameObject fake1;
    public GameObject fake2;
    public GameObject fake3;

    // Start is called before the first frame update
    void Start()
    {
        //fake
        addItem(fake1);
        addItem(fake2);
        addItem(fake3);
    }

    // Update is called once per frame
    void Update()
    {
        //fake
        for (var i = 1; i <=3; i++)
        {
            GameObject slot = GameObject.Find("Slot" + i);
            GameObject btn = inventoryItem[i];
            updateBtn(slot, btn);
        }
    }

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

    private static GameObject createItem(GameObject obj)
    {
        GameObject btn = Instantiate(Resources.Load("Prefabs/BtnSample")) as GameObject;
        //set the obj
        btn.GetComponent<ItemListener>().setCurrGameObject(obj);
        ((RectTransform)btn.transform).sizeDelta = new Vector2(110, 110);
        #if UNITY_EDITOR
        var texture = AssetPreview.GetAssetPreview(obj);
        btn.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
        #endif
        return btn;
    }

    private void updateBtn(GameObject slot, GameObject btn)
    {
        foreach (Transform child in slot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        btn.transform.parent = slot.transform;
    }
}
