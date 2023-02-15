using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public string toolName;
    public string description;

    [SerializeField] private GameObject piece;
    [SerializeField] public bool openAndCloseAnimation;
    [SerializeField] private float closePosition = 0f;
    [SerializeField] private float openPosition = 0f;

    private float endPosition;
    private float currentPossition;

    void Start()
    {
        endPosition = openPosition;
        currentPossition = closePosition;
    }

    void Update()
    {
        if (openAndCloseAnimation)
        {
            if (currentPossition < endPosition)
            {
                currentPossition = currentPossition + 0.5f;
                piece.transform.Rotate(0f, 0f, 0.5f);
            }
            else
            {
                endPosition = closePosition;
            }

            if (currentPossition > endPosition)
            {
                currentPossition = currentPossition - 0.5f;
                piece.transform.Rotate(0f, 0f, -0.5f);
            }
            else
            {
                endPosition = openPosition;
            }
        }
    }
}
