using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//����ʵ����Ʒ�Ľ����ͽ���Ч��
//[RequireComponent(typeof(SortingGroup))]
public class ItemFaders : ItemFader
{
    private SpriteRenderer[] SpriteRenderers;
    private void Awake()
    {
        SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    
    //�𽥻ָ���ɫ
    public override void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        for(int i = 0; i < SpriteRenderers.Length; i++)
        {
            SpriteRenderers[i].DOColor(targetColor, Settings.fadeDuration);
        }
       
    }
    
    //�𽥰�͸��
    public override void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);   
        for (int i = 0; i < SpriteRenderers.Length; i++)
        {
            SpriteRenderers[i].DOColor(targetColor, Settings.fadeDuration);
        }
    }
}
