//-----------------------------------------------------------------------
// <copyright file="SelectionManipulator.cs" company="Google">
//
// Copyright 2018 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.ObjectManipulation
{
    using UnityEngine;

    /// <summary>
    /// Controls the selection of an object through Tap gesture.
    /// </summary>
    public class SelectionManipulator : Manipulator
    {
        /// <summary>
        /// The visualization game object that will become active when the object is selected.
        /// </summary>
        public GameObject SelectionVisualization;

        private float m_ScaledElevation;

        /// <summary>
        /// Should be called when the object elevation changes, to make sure that the Selection
        /// Visualization remains always at the plane level. This is the elevation that the object
        /// has, independently of the scale.
        /// </summary>
        /// <param name="elevation">The current object's elevation.</param>
        public void OnElevationChanged(float elevation)
        {
            m_ScaledElevation = elevation * transform.localScale.y;
            SelectionVisualization.transform.localPosition = new Vector3(0, -elevation, 0);
        }

        /// <summary>
        /// Should be called when the object elevation changes, to make sure that the Selection
        /// Visualization remains always at the plane level. This is the elevation that the object
        /// has multiplied by the local scale in the y coordinate.
        /// </summary>
        /// <param name="scaledElevation">The current object's elevation scaled with the local y
        /// scale.</param>
        public void OnElevationChangedScaled(float scaledElevation)
        {
            m_ScaledElevation = scaledElevation;
            SelectionVisualization.transform.localPosition =
                new Vector3(0, -scaledElevation / transform.localScale.y, 0);
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        protected override void Update()
        {
            base.Update();
            if (transform.hasChanged)
            {
                float height = -m_ScaledElevation / transform.localScale.y;
                SelectionVisualization.transform.localPosition = new Vector3(0, height, 0);
            }
        }

        /// <summary>
        /// Returns true if the manipulation can be started for the given gesture.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        /// <returns>True if the manipulation can be started.</returns>
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            return true;
        }

        /// <summary>
        /// Function called when the manipulation is ended.
        /// </summary>
        /// <param name="gesture">The current gesture.</param>
        protected override void OnEndManipulation(TapGesture gesture)
        {
            Debug.Log("\n\n\n\n==============On Ending Tap");
            if (gesture.WasCancelled)
            {
                Debug.Log("\n\n\n\n==============gesture.WasCancelled");
                return;
            }

            if (ManipulationSystem.Instance == null)
            {
                Debug.Log("\n\n\n\n==============ManipulationSystem.Instance == null");
                return;
            }

            GameObject target = gesture.TargetObject;
            if (target == gameObject)
            {
                Debug.Log("\n\n\n\n==============Finish Tap");
                Debug.Log(target.name);
                try{
                    target.tag = "selectable";
                    Debug.Log("\n\n\n\n==============Added Tag");
                }
                catch{
                    Debug.Log("\n\n\n\n====== Can't add Tag");
                }
                Select();
            }

            Debug.Log("\n\n\n\n==============Init ray");
            // Raycast against the location the player touched to search for planes.
            //TrackableHit hit;
            //TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;
            //Debug.Log("\n\n\n\n==============Init Finished");
            //bool flag = Frame.Raycast(
            //    gesture.StartPosition.x, gesture.StartPosition.y, raycastFilter, out hit);

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("\n\nDid Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("\n\nDid not Hit");
            }
            //Debug.Log("\n\n\n\n==============flag="+ flag.ToString());
            //if (!flag)
            //{
            //    Debug.Log("\n\n\n\n==============ray cast");
            //    Deselect();
            //}
            Debug.Log("\n\n\n\n==============End of function");
        }

        /// <summary>
        /// Function called when this game object is deselected if it was the Selected Object.
        /// </summary>
        protected override void OnSelected()
        {
            SelectionVisualization.SetActive(true);
        }

        /// <summary>
        /// Function called when this game object is deselected if it was the Selected Object.
        /// </summary>
        protected override void OnDeselected()
        {
            SelectionVisualization.SetActive(false);
        }
    }
}
