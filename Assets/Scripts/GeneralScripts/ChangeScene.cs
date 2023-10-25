using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
