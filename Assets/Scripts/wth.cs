using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject input_Length;

    public void change_text()
    {
        input_Length.GetComponent<Text>().text = "???????????????????????????";
    }
}
