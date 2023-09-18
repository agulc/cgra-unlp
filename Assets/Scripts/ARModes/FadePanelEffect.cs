using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadePanelEffect : MonoBehaviour
{
    public void togglePannel(CanvasGroup panel)
    {
        if (panel.gameObject.activeInHierarchy == true)
        {
            panel.DOFade(0f, 1f).OnComplete(() => panel.gameObject.SetActive(false));
        }
        else
        {
            panel.gameObject.SetActive(false);
            panel.DOFade(1f, 1f);
        }
    }
}
