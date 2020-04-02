using System;
using UnityEngine;


namespace TriLib
{
    namespace Samples
    {
        /// <summary>
        /// Represents a simple model loading sample.
        /// </summary>
        public class LoadSample : MonoBehaviour
        {
            GameObject dialog = null;

#if UNITY_EDITOR || !UNITY_WINRT
            /// <summary>
            /// Tries to load "Bouncing.fbx" model
            /// </summary>
            protected void Start()
            {

                using (var assetLoader = new AssetLoader())
                {
                    try
                    {
                        var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
                        assetLoaderOptions.RotationAngles = new Vector3(0f, 0f, 0f);
                        assetLoaderOptions.AutoPlayAnimations = true;
                        //Use this for PC
                        //var loadedGameObject = assetLoader.LoadFromFile("C:/Users/felix/Desktop/bird.OBJ", assetLoaderOptions);  //This can be a .fbx file or a .obj file
                        
                        //ask for permission
                        //Use this for Android
                        GameObject loadedGameObject = assetLoader.LoadFromFile("storage/emulated/0/Download/bird.OBJ", assetLoaderOptions);  //This can be a .fbx file or a .obj file
                        //Inventory.addItem(loadedGameObject);
                        loadedGameObject.transform.position = new Vector3(0f, 0f, 0f);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.ToString());
                    }
                }
            }
#endif
        }
    }
}
