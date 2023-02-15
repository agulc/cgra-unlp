using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;

[System.Serializable] public class ToolPrefabDictionary : SerializableDictionaryBase<string, GameObject> { }

public class ToolsMainMode : MonoBehaviour
{
    [SerializeField] ToolPrefabDictionary toolPrefabs;
    [SerializeField] ARTrackedImageManager imageManager;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UIController.ShowUI("Main");

        foreach (ARTrackedImage image in imageManager.trackables)
        {
            InstantiateTool(image);
        }
        imageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    void InstantiateTool(ARTrackedImage image)
    {
        string name = image.referenceImage.name.Split('-')[0];
        
        if (image.transform.childCount == 0)
        {
            GameObject tool = Instantiate(toolPrefabs[name]);
            tool.transform.SetParent(image.transform, false); //puede ser util en true
        }
        else
        {
            Debug.Log($"{name} already instantiated");
        }
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            InstantiateTool(newImage);
        }
    }

    private void Update()
    {
        if (imageManager.trackables.count == 0)
        {
            InteractionController.EnableMode("Scan");
        }
    }
}
