using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImportFile : MonoBehaviour
{
    [SerializeField] public FileData _FileData = new FileData();
    public void SaveIntoJson()
    {
        FileDataList listFileData;
        String inputString = "";
        try {
            inputString = File.ReadAllText(Application.persistentDataPath + "/ImportedFiles.json");
        }
        catch (Exception ex)
        {
           
        }
       
        if (inputString == "")
        {
            listFileData = new FileDataList();
        }
        else
        {
            listFileData = JsonUtility.FromJson<FileDataList>(inputString);
        }

        //Check if the file already in the list

        foreach (FileData eachfile in listFileData.fileDataList)
        {
            if (eachfile.filePath == _FileData.filePath)
            {
                return;
            }
        }
            listFileData.fileDataList.Add(_FileData);



        string fileInfo = JsonUtility.ToJson(listFileData);

        //using (StreamReader r = new StreamReader(Application.persistentDataPath + "/ImportedFiles.json"))
        //{
        //    string json = r.ReadToEnd();
        //    List<FileData> items = JsonConvert.DeserializeObject<List<FileData>>(json);
        //}
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ImportedFiles.json", fileInfo);
    }

    public List<FileData> GetListOfFiles()
    {
        FileDataList listFileData = new FileDataList();
        String inputString = "";
        try
        {
            inputString = File.ReadAllText(Application.persistentDataPath + "/ImportedFiles.json");
        }
        catch (Exception ex)
        {
            return listFileData.fileDataList;
        }

        if (inputString == "")
        {
            return listFileData.fileDataList;
        }
        else
        {
            listFileData = JsonUtility.FromJson<FileDataList>(inputString);
            return listFileData.fileDataList;
        }
    }
}

[System.Serializable]
public class FileDataList
{
    public List<FileData> fileDataList;
    public FileDataList()
    {
        this.fileDataList = new List<FileData>();
    }
}

[System.Serializable]
public class FileData
{
    public string filePath;
    public string fileName;
}
