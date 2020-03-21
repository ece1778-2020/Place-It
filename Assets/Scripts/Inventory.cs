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
    private int offset = 0;
    private bool hasSwipeFeature = true;
    public GameObject fake1;
    public GameObject fake2;
    public GameObject fake3;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas = GameObject.Find("Inventory");
        RuntimePreviewGenerator.OrthographicMode = true;
        RuntimePreviewGenerator.PreviewDirection = new Vector3(-1, -1 , -1);
        //fake
        addItem(fake1);
        addItem(fake2);
        checkBtnState();
    }

    // Update is called once per frame
    void Update()
    {
        //fake
        for (var i = 0; i <inventoryItem.Count; i++)
        {
            //GameObject slot = GameObject.Find("Slot" + i);
            GameObject btn = inventoryItem[i];
            btn.transform.SetParent(inventoryCanvas.transform);
            adjustBtn(btn, i + offset);
        }
        checkLeftBtnState();
        checkRightBtnState();
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

        //#if UNITY_EDITOR
        //        Texture2D texture = UnityEditor.AssetPreview.GetAssetPreview(obj);
        Texture2D texture = RuntimePreviewGenerator.GenerateModelPreview(obj.transform, 110, 110, false);
        btn.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 110, 110), new Vector2(0, 0));
    //#endif
        
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

    public void LeftSwipe()
    {
        if (offset > 0 - inventoryItem.Count + 3)
        {
            offset -= 1;
        }
        checkLeftBtnState();
    }

    public void RightSwipe()
    {
        if (offset < 0)
        {
            offset += 1;
        }
        checkRightBtnState();
    }

    private void checkBtnState()
    {
        if (inventoryItem.Count <= 3)
        {
            GameObject.Find("LeftBtn").SetActive(false);
            GameObject.Find("RightBtn").SetActive(false);
            hasSwipeFeature = false;
        }
    }

    private void checkLeftBtnState()
    {
        if (hasSwipeFeature)
        {
            if (offset <= 0 - inventoryItem.Count + 3)
            {
                GameObject.Find("LeftBtn").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("LeftBtn").GetComponent<Button>().interactable = true;
            }
        }
        
    }

    private void checkRightBtnState()
    {
        if (hasSwipeFeature)
        {
            if (offset >= 0)
            {
                GameObject.Find("RightBtn").GetComponent<Button>().interactable = false;
            }
            else
            {
                GameObject.Find("RightBtn").GetComponent<Button>().interactable = true;
            }
        }
    }

}
