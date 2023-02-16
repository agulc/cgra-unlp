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
    [SerializeField] TMP_Text toolName;
    [SerializeField] Toggle infoButton;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] TMP_Text detailsText;

    Camera cam;
    private int layerMask;

    private void Start()
    {
        cam = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("PlacedObjects");
    }
    private void OnEnable()
    {
        UIController.ShowUI("Main");

        foreach (ARTrackedImage image in imageManager.trackables)
        {
            InstantiateTool(image);
        }
        imageManager.trackedImagesChanged += OnTrackedImageChanged;

        toolName.text = "";
        infoButton.interactable = false;
        detailsPanel.SetActive(false);
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
        else
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Tool tool = hit.collider.GetComponentInParent<Tool>();
                toolName.text = tool.toolName;
                detailsText.text = tool.toolDescription;
                infoButton.interactable = true;
                
            }
            else
            {
                if (detailsPanel.activeInHierarchy == false)
                {
                    toolName.text = "";
                    detailsText.text = "";
                    infoButton.interactable = false;
                }
            }  
        }
    }
}
