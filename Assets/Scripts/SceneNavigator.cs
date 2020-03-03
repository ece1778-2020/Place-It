using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneNavigator : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public GameObject MultiModel_Btn, BagModel_Btn;
    public void LoadMultiObjectScene()
    {
        StartCoroutine(LoadSampleScene("SampleScene"));
    }

    public void LoadBagScene()
    {
        StartCoroutine(LoadSampleScene("BagScene"));
    }
    IEnumerator LoadSampleScene(string SceneName)
    {
        yield return null;

        MultiModel_Btn.SetActive(false);
        BagModel_Btn.SetActive(false);
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                m_Text.text = "Scene Ready";
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
