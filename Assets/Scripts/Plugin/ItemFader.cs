using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//该类实现物品的渐隐和渐现效果
[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    //逐渐恢复颜色
    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        SpriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }
    
    //逐渐半透明
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);
        SpriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }
}
