using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//该类用于实现触发器碰撞后产生效果的功能
public class TriggerItemFader : MonoBehaviour
{
    //进入触发器时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length > 0)
        {
            //设置物品逐渐变成半透明状态
            foreach (var item in faders)
            {
                item.FadeOut();
            }
        }
    }

    //离开触发器时调用
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length > 0)
        {
            //设置物品逐渐恢复到不透明状态
            foreach (var item in faders)
            {
                item.FadeIn();
            }
        }
    }
}
