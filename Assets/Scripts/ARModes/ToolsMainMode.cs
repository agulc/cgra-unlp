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
    [SerializeField] ARTrackedImageManager imageManager;
    [Space]
    [SerializeField] ToolPrefabDictionary toolPrefabs;
    [SerializeField] TMP_Text toolName;
    [Space]
    [SerializeField] Toggle infoButton;
    [SerializeField] TMP_Text infoButtonText;
    [SerializeField] TMP_Text infoButtonLogo;
    [Space]
    [SerializeField] GameObject detailsPanel;
    [SerializeField] TMP_Text detailsText;
    [Space]
    [SerializeField] Button animationButton;
    [SerializeField] TMP_Text animationButtonText;

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

        detailsPanel.SetActive(false);
        disableToolInterface();
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
                enableToolInterface(tool);
                
            }
            else
            {
                disableToolInterface();
            }  
        }
    }

    private void enableToolInterface(Tool tool)
    {
        toolName.text = tool.toolName;
        detailsText.text = tool.toolDescription.text;
        infoButton.interactable = true;
        animationButton.interactable = true;
        infoButtonText.text = "Información";
        infoButtonLogo.text = "?";
        if (tool.isOpen)
            animationButtonText.text = "Cerrar";
        else
            animationButtonText.text = "Abrir";
    }

    private void disableToolInterface()
    {
        if (detailsPanel.activeInHierarchy == false)
        {
            toolName.text = "";
            detailsText.text = "";
            infoButtonText.text = "";
            infoButtonLogo.text = "";
            infoButton.interactable = false;
        }
        animationButton.interactable = false;
        animationButtonText.text = "";
    }
    public void ToggleAnimation()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Tool tool = hit.collider.GetComponentInParent<Tool>();
            tool.isOpen = !tool.isOpen;
        }
    }
}
