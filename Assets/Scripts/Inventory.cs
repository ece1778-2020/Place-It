using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    private static List<GameObject> inventoryItem;
    private GameObject inventoryCanvas;
    private int NUMBER_OF_ITEM_DISPLAY_IN_FRONT = 3;
    public GameObject fake1;
    public GameObject fake2;
    public GameObject fake3;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas = GameObject.Find("Inventory");
        //fake
        addItem(fake1);
        addItem(fake2);
        addItem(fake3);
    }

    // Update is called once per frame
    void Update()
    {
        //fake
        for (var i = 0; i <3; i++)
        {
            //GameObject slot = GameObject.Find("Slot" + i);
            GameObject btn = inventoryItem[i];
            btn.transform.parent = inventoryCanvas.transform;
            adjustBtn(btn, i);
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
        btn.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 110, 110), new Vector2(0, 0));
#endif
        
        return btn;
    }

    private void adjustBtn(GameObject btn, int num)
    {
        ((RectTransform)btn.transform).localScale = new Vector3(1, 1, 1);
        int totalItem = NUMBER_OF_ITEM_DISPLAY_IN_FRONT;
        float canvasWidth = ((RectTransform)inventoryCanvas.transform).rect.width;
        //Ex: (250-55)/2 + 55
        float xPosition = (((500 / (totalItem - 1)) - (110 / 2))/2 + (110/2)) * (num - 1);
        ((RectTransform)btn.transform).anchoredPosition = new Vector3(xPosition, 0, 0);
        ((RectTransform)btn.transform).position = new Vector3(xPosition, 0, 0);
        ((RectTransform)btn.transform).localPosition = new Vector3(xPosition, 0, 0);
    }
}
