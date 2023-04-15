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
                VGF_Player.Instance.transform.position = new Vector3(77, 7, 175);
                Caption("ÐòÕÂ");

            });
            SceneMove("Sample Scene");
        }
    }
}

