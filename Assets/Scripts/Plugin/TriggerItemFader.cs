using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��������ʵ�ִ�������ײ�����Ч���Ĺ���
public class TriggerItemFader : MonoBehaviour
{
    //���봥����ʱ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length > 0)
        {
            //������Ʒ�𽥱�ɰ�͸��״̬
            foreach (var item in faders)
            {
                item.FadeOut();
            }
        }
    }

    //�뿪������ʱ����
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length > 0)
        {
            //������Ʒ�𽥻ָ�����͸��״̬
            foreach (var item in faders)
            {
                item.FadeIn();
            }
        }
    }
}
