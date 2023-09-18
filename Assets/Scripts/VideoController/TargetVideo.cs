using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TargetVideo : MonoBehaviour
{
    private static VideoClip videoClip;

    public static void SetVideoClip(VideoClip newVideoClip)
    {
        videoClip = newVideoClip;
    }

    public static VideoClip GetVideoClip()
    {
        return videoClip;
    }
}
