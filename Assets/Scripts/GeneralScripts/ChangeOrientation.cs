using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrientation : MonoBehaviour
{
    public ScreenOrientation startingOrientation = ScreenOrientation.Portrait;
    private ScreenOrientation currentOrientation;

    void Start()
    {
        currentOrientation = startingOrientation;
        this.UpdateOrientation();
    }

    public void OnClick()
    {
        if (currentOrientation == ScreenOrientation.LandscapeLeft)
            currentOrientation = ScreenOrientation.Portrait;
        else
            currentOrientation = ScreenOrientation.LandscapeLeft;

        this.UpdateOrientation();
    }

    private void UpdateOrientation()
    {
        Screen.orientation = currentOrientation;
    }
    
}
