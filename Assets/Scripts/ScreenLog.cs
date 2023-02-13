using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ScreenLog : MonoBehaviour
{

    public TMP_Text logText;
    public static ScreenLog Instance
    {
        get; 
        private set;
    }

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        logText.text = "";
    }

    private void WriteLog(string msg)
    {
        if (logText)
            logText.text += msg + "\n";
    }

    public static void Log(string msg)
    {
        if (Instance)
            Instance.WriteLog("<color=#" + ColorUtility.ToHtmlStringRGB(Color.white) + ">" + msg);
        Debug.Log(msg);
    }

    public static void War(string msg)
    {
        if (Instance)
            Instance.WriteLog("<color=#" + ColorUtility.ToHtmlStringRGB(Color.yellow) + ">" + msg);
        Debug.LogWarning(msg);
    }

    public static void Err(string msg)
    {
        if (Instance)
            Instance.WriteLog("<color=#" + ColorUtility.ToHtmlStringRGB(Color.red) + ">" + msg);
        Debug.LogError(msg);
    }

    public static void Suc(string msg)
    {
        if (Instance)
            Instance.WriteLog("<color=#" + ColorUtility.ToHtmlStringRGB(Color.green) + ">" + msg);
        Debug.LogAssertion(msg);
    }

}
