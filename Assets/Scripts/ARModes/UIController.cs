using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

[System.Serializable]
public class UIPanelDictionary :
    SerializableDictionaryBase<string, CanvasGroup> { }
public class UIController : Singleton<UIController>
{
    [SerializeField] UIPanelDictionary UIPanels;

    CanvasGroup currentPanel;


    protected override void Awake() //no se puede usar protected override void Awake porque eso haría que Unity no pueda invocar ningun Awake. Probar de ponerlo si todo funciona como está.
    {
        base.Awake();
        ResetAllUI();
    }


    void ResetAllUI()
    {
        foreach (CanvasGroup panel in UIPanels.Values)
        {
            panel.alpha = 0;
            panel.gameObject.SetActive(false);
        }
    }

    public static void ShowUI(string name)
    {
        Instance?._ShowUI(name);
    }

    void _ShowUI(string name)
    {
        CanvasGroup panel;
        if (UIPanels.TryGetValue(name, out panel))
        {
            ChangeUI(UIPanels[name]);
        }
        else
        {
            ScreenLog.Err("Undefined UI Panel " + name);
        }
    }

    void ChangeUI(CanvasGroup panel)
    {
        if (panel == currentPanel)
            return;

        if (currentPanel)
            FadeOut(currentPanel);

        currentPanel = panel;

        if (currentPanel)
            FadeIn(currentPanel);
    }

    void FadeIn(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true);
        panel.DOFade(1f, 1f);
    }

    void FadeOut(CanvasGroup panel)
    {
        panel.DOFade(0f, 1f).OnComplete(() => panel.gameObject.SetActive(false));

    }

}
