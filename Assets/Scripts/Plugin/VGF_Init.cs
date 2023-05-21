using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class VGF_Init : MonoBehaviour
{
    public static bool isInited = false;
    public PlayableDirector director;
    public PlayableAsset asset;

    private void Awake()
    {
        VGF();
    }
    public void VGF()
    {
        if(!isInited)
        {
            director = GetComponent<PlayableDirector>();
            director.Play(asset);
            isInited = true;
        }
        else return;
    }
}
