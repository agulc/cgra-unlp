using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{

    [SerializeField] GameObject placedPrefab;
    GameObject spawnedObject;
    ARRaycastManager raycaster;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Pose hitPose;


    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<ARRaycastManager>();
    }

    void OnPlaceObject(InputValue value)
    {
        //Obtener la posición que se tocó en la pantalla
        Vector2 touchPosition = value.Get<Vector2>();

        //Generar un vector (Raycast) desde el punto en que se toco la pantalla dentro de la escena, buscando un plano.
        //Si el raycast golpeó con un plano
        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //Obtener el punto de colision, su posicion y rotacion (pose) (nos importa el mas cercano, o sea hits[0])
            hitPose = hits[0].pose;

            if (spawnedObject == null) //por si no está creado
            {
                ScreenLog.Suc("Creando: " + placedPrefab.ToString());
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                ScreenLog.War("Cambiando posicion: " + placedPrefab.ToString());
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }
}
