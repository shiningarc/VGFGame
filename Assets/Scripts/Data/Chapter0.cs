using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.Plot
{
    public class Chapter0 : ChapterBase
    {
        public override void Run()
        {
            BindSceneEvent("Sample Scene", (msg) =>
            {
                VGF_Player.Instance.transform.position =  GameObject.Find("Æðµã").transform.position;
                Caption("ÐòÕÂ");

            });
            SceneMove("Sample Scene");
        }
    }
}

