using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.ObjectManipulation;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class SampleController : Manipulator
{
  
    public Camera FirstPersonCamera;


    public GameObject GameObjectVerticalPlanePrefab;
    public GameObject GameObjectHorizontalPlanePrefab;
    public GameObject GameObjectHorizontalUpwardPlanePrefab;
    public GameObject mComponent;

    public GameObject ManipulatorPrefab;

    public int numberOfObjectsAllowed = 2;
    private int currentNumberOfObjects = 0, currentNumberOfVerticalObjects = 0, currentNumberOfUpObjects=0;

    private const float k_ModelRotation = 180.0f;
    //private GameObject modelprefab;

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.TargetObject == null)
        {
            return true;
        }
        Debug.Log("\n\nTarget Object not null");
        return false;
    }

    /// <summary>
    /// Function called when the manipulation is ended.
    /// </summary>
    /// <param name="gesture">The current gesture.</param>
    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.WasCancelled)
        {
            return;
        }

        // If gesture is targeting an existing object we are done.
        if (gesture.TargetObject != null)
        {
            Debug.Log("\n\nTarget Object != null");
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(
            gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                // Instantiate game object at the hit pose.
                DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;

                //Debug.Log("\n\n\n\n\ndetectedPlane.PlaneType=" + detectedPlane.PlaneType.ToString());
                //Debug.Log("DetectedPlaneType.HorizontalUpwardFacing=" + DetectedPlaneType.HorizontalUpwardFacing.ToString());
                //Debug.Log("currentNumberOfObjects=" + currentNumberOfObjects.ToString());
                //Debug.Log("numberOfObjectsAllowed=" + numberOfObjectsAllowed.ToString());
                //Debug.Log("===>Condition=" + (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing && currentNumberOfObjects < numberOfObjectsAllowed).ToString());
                if (detectedPlane.PlaneType == DetectedPlaneType.Vertical && currentNumberOfVerticalObjects < numberOfObjectsAllowed)
                {
                    GameObject modelprefab = Instantiate(GameObjectVerticalPlanePrefab, hit.Pose.position, hit.Pose.rotation);
                    GenerateModels(modelprefab, hit);
                    currentNumberOfVerticalObjects++;
                }
                else if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing && currentNumberOfObjects < numberOfObjectsAllowed )
                {
                    Debug.Log("\n\n\n more horizontal !!!!!");
                    GameObject modelprefab = Instantiate(GameObjectHorizontalPlanePrefab, hit.Pose.position, hit.Pose.rotation);
                    GenerateModels(modelprefab, hit);
                    currentNumberOfObjects++;
                }
                else if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalDownwardFacing && currentNumberOfUpObjects < numberOfObjectsAllowed)
                {
                    GameObject modelprefab = Instantiate(GameObjectHorizontalUpwardPlanePrefab, hit.Pose.position, hit.Pose.rotation);
                    GenerateModels(modelprefab, hit);
                    currentNumberOfUpObjects++;
                }

                
            }
        }
    }

    void GenerateModels(GameObject modelprefab, TrackableHit hit)
    {
        // Instantiate manipulator.
        var manipulator =
            Instantiate(ManipulatorPrefab, hit.Pose.position, hit.Pose.rotation);

        // Make game object a child of the manipulator.
        modelprefab.transform.parent = manipulator.transform;

        // Create an anchor to allow ARCore to track the hitpoint as understanding of
        // the physical world evolves.
        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

        // Make manipulator a child of the anchor.
        manipulator.transform.parent = anchor.transform;

        // Select the placed object.
        manipulator.GetComponent<Manipulator>().Select();
    }
}
