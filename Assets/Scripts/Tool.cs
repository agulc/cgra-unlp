using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public string toolName;
    public string toolDescription;

    [SerializeField] private GameObject piece;
    [SerializeField] public bool isOpen = false;
    [SerializeField] private float closePosition = 0f;
    [SerializeField] private float openPosition = 42f;
    [SerializeField] private float animationSpeed = 0.5f;

    private float currentPossition;

    void Start()
    {
        currentPossition = closePosition;
    }

    void Update()
    {
        if (isOpen)
        {
            if (currentPossition < openPosition)
            {
                currentPossition = currentPossition + animationSpeed;
                piece.transform.Rotate(0f, 0f, animationSpeed);
            }
        }
        else
        {
            if (currentPossition > closePosition)
            {
                currentPossition = currentPossition - animationSpeed;
                piece.transform.Rotate(0f, 0f, -animationSpeed);
            }
        }
    }
}
