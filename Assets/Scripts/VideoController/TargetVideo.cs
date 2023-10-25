using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TargetVideo : MonoBehaviour
{
    private static VideoClip videoClip;
    private static string videoClipURL;

    public static void SetVideoClip(VideoClip newVideoClip)
    {
        videoClip = newVideoClip;
    }

    public static VideoClip GetVideoClip()
    {
        return videoClip;
    }

    public static void SetVideoClipURL(string newVideoClipURL)
    {
        videoClipURL = newVideoClipURL;
    }

    public static string GetVideoClipURL()
    {
        return videoClipURL;
    }
}
