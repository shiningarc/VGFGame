using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//该类实现物品的渐隐和渐现效果
//[RequireComponent(typeof(SortingGroup))]
public class ItemFaders : ItemFader
{
    private SpriteRenderer[] SpriteRenderers;
    private void Awake()
    {
        SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    
    //逐渐恢复颜色
    public override void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        for(int i = 0; i < SpriteRenderers.Length; i++)
        {
            SpriteRenderers[i].DOColor(targetColor, Settings.fadeDuration);
        }
       
    }
    
    //逐渐半透明
    public override void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);   
        for (int i = 0; i < SpriteRenderers.Length; i++)
        {
            SpriteRenderers[i].DOColor(targetColor, Settings.fadeDuration);
        }
    }
}
