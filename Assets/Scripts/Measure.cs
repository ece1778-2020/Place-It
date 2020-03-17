using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Measure : MonoBehaviour
{
    private GameObject object1;
    private GameObject object2;
    private bool settingObj1 = true;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("DisplayCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        // One finger
        if (Input.touchCount == 1)
        {
            Debug.Log("TouchCount 1");
            // Tap on Object
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (settingObj1)
                    {
                        object1 = hit.transform.gameObject;
                        Debug.Log("OBJ1 SETTED");
                        settingObj1 = !settingObj1;
                        cleanLines();
                        var texts = canvas.GetComponentsInChildren<Text>();
                        object2 = null;
                        texts[0].text = "Please select ending point to measure";
                    }
                    else
                    {
                        object2 = hit.transform.gameObject;
                        Debug.Log("OBJ2 SETTED");
                        settingObj1 = !settingObj1;
                    }
                    if (object1 != null && object2 != null)
                    {
                        calDistance(object1.transform, object2.transform);
                    }
                   
                }
            }
        }
    }

    private void calDistance(Transform obj1, Transform obj2)
    {
        float Dist = 100 * Vector3.Distance(obj1.position, obj2.position);
        Debug.DrawRay(obj1.position, obj2.position, Color.green);
        Debug.Log(Dist);
        
        var texts = canvas.GetComponentsInChildren<Text>();
        texts[0].text = Dist.ToString("0.##") + " cm";
        var lr = canvas.AddComponent<LineRenderer>();
        lr.widthCurve = AnimationCurve.Linear(0, .01f, 1, .01f);
        lr.SetPosition(0, obj1.position);
        lr.SetPosition(1, obj2.position);
    }

    private void cleanLines()
    {
        var prev = canvas.GetComponentsInChildren<LineRenderer>();
        for (var i = 0; i < prev.Length; i ++)
        {
            Destroy(prev[i]);
        }
    }

}
