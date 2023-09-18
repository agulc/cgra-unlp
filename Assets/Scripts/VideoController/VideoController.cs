using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private int seconds = 5;
    [SerializeField]
    private int framerate = 30;

    private long step;

    private void Awake()
    {
        step = framerate * seconds;
    }

    public void StepForward ()
    {
        videoPlayer.frame += step;
    }

    public void StepBackwards()
    {
        videoPlayer.frame -= step;
    }

    public void Restart()
    {
        videoPlayer.frame = 0;
    }


}
