using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    //Öð½¥»Ö¸´ÑÕÉ«
    public void FadeIn()
    {
        Color targetColor=new Color(1,1,1,1);
        SpriteRenderer.DOColor(targetColor,Settings.fadeDuration);
    }
    //Öð½¥°ëÍ¸Ã÷
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);
        SpriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }

}
