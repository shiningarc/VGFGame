using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


//����ʵ����Ʒ�Ľ����ͽ���Ч��
[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    //�𽥻ָ���ɫ
    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        SpriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }
    
    //�𽥰�͸��
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);
        SpriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }
}
