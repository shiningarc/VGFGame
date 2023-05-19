using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : Singleton<ShadowPool>
{
    public GameObject shadoePrefab;
    public int shadowCount;
    private Queue<GameObject> avalibaleObjects = new Queue<GameObject>();
    public void FillPool()
    {
        for(int i=0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadoePrefab);
            newShadow.transform.SetParent(transform);
            ReturnPool(newShadow);
        }
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        avalibaleObjects.Enqueue(gameObject);
    }
    public GameObject GetFromPool()
    {
        if(avalibaleObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = avalibaleObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
}
