using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ObjExportHelper : MonoBehaviour
{
    public List<GameObject> meshObjectList;
    //private GameObject exportObj;
    //public GameObject GO1;
    //public GameObject Go2;

        void Start()
    {
        ////exportObj = new GameObject();
        meshObjectList = new List<GameObject>();
    }

    public void exportObject(GameObject parentObject)
    {
        parentObject.transform.position = new Vector3(0, 0.5f, 0);
        parentObject.transform.localScale = new Vector3(100, 100, 100);
        Transform[] allChildren = GameObject.Find("ObjFactory").GetComponentsInChildren<Transform>();
        foreach (Transform child in GameObject.Find("ObjFactory").transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                meshObjectList.Add(child.gameObject);
            }

        }

        //meshObjectList.Add(GO1);
        //meshObjectList.Add(Go2);
        // combine meshes
        CombineInstance[] combine = new CombineInstance[meshObjectList.Count];
        int i = 0;
        while (i < meshObjectList.Count)
        {
            MeshFilter meshFilter1 = meshObjectList[i].gameObject.GetComponent<MeshFilter>();
            combine[i].mesh = meshFilter1.sharedMesh;
            combine[i].transform = meshFilter1.transform.localToWorldMatrix;
            i++;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        //Mesh mesh = CombineMeshes(new List<Mesh> { GO1.GetComponent<MeshFilter>().mesh, Go2.GetComponent<MeshFilter>().mesh });
        //Debug.Log(mesh.ToString());
        parentObject.AddComponent<MeshFilter>().mesh = combinedMesh;
        string path = Application.persistentDataPath + "/exporrted.OBJ";
        MeshFilter meshFilter = parentObject.GetComponent<MeshFilter>();
        Debug.Log(meshFilter.name);
        ObjExporter.MeshToFile(meshFilter, path);
        ImportFile importFile = new ImportFile();
        importFile._FileData.filePath = path;
        importFile._FileData.fileName = Path.GetFileName(path);
        importFile.SaveIntoJson();
        
        SceneManager.LoadScene("PlaceIt");
    }

}
