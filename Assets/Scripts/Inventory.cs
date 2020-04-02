using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    private List<GameObject> inventoryItem;
    public GameObject inventoryCanvas;
    private int NUMBER_OF_ITEM_DISPLAY_IN_FRONT = 3;
    private int offset = 0;
    private bool hasSwipeFeature = true;
    public GameObject stool;
    public GameObject loadedGameObject;
    public static GameObject drawItObj;


    // Start is called before the first frame update
    void Start()
    {
     

        //inventoryCanvas = GameObject.Find("Inventory");
        ImportFile importFile = new ImportFile();
        List<FileData> fileDataList = importFile.GetListOfFiles();
        inventoryItem = new List<GameObject>();
        foreach (FileData fileData in fileDataList)
        {
            loadItem(fileData);
        }
        addItem(stool);
        checkBtnState();

    }
    private void loadItem(FileData item)
    {
        RuntimePreviewGenerator.OrthographicMode = true;
        RuntimePreviewGenerator.PreviewDirection = new Vector3(-1, -1, -1);

        using (var assetLoader = new TriLib.AssetLoader())
        {
            try
            {
                var assetLoaderOptions = TriLib.AssetLoaderOptions.CreateInstance();
                var wrapperGameObject = gameObject;
                //assetLoaderOptions.AutoPlayAnimations = true;

                //Use this for PC
                //GameObject loaded = assetLoader.LoadFromFile("C:/Users/xuhzc/Downloads/objects/stand.OBJ", assetLoaderOptions);  //This can be a .fbx file or a .obj file

                //Use this for Android
                GameObject loaded = assetLoader.LoadFromFile(item.filePath, assetLoaderOptions, wrapperGameObject);  //This can be a .fbx file or a .obj file

                loadedGameObject = Instantiate(loaded);
                loadedGameObject.transform.position = new Vector3(10000f, 10000f, 10000f);
                loadedGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

                BoxCollider c = loadedGameObject.AddComponent<BoxCollider>();
                Debug.Log("================box=====================");
                c.size = new Vector3(50, 50, 50);
                c.center = new Vector3(0, 25, 0);
                Destroy(loaded);

            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        addItem(loadedGameObject);
    }

    // Update is called once per frame
    void Update()
    {

        //load item from draw it
        if (drawItObj != null)
        {
            drawItObj.transform.position = new Vector3(10000f, 10000f, 10000f);
            BoxCollider c = drawItObj.AddComponent<BoxCollider>();
            Debug.Log("================box=====================");
            c.size = new Vector3(1, 1, 1);
            c.center = new Vector3(0, 0.5f, 0);
            //drawItObj.transform.rotation = Quaternion.identity;

            addItem(drawItObj);
            drawItObj = null;
        }

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

    public GameObject addItem(GameObject go)
    {
        if (inventoryItem == null)
        {
            inventoryItem = new List<GameObject>();
        }
        GameObject item = createItem(go);
        inventoryItem.Add(item);
        return item;
    }

    public List<GameObject> getItems()
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
        if (inventoryItem == null || inventoryItem.Count <= 3)
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
