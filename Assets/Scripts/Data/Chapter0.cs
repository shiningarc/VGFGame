using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自定义VGF.Plot，实现场景转换功能
namespace VGF.Plot
{
    public class Chapter0 : ChapterBase
    {
        //场景转换
        public override void Run()
        {
            //绑定场景事件
            BindSceneEvent("Sample Scene", (msg) =>
            {
                VGF_Player.Instance.transform.position = GameObject.Find("起点").transform.position;
                Caption("序章");

            });
            
            //移动场景
            SceneMove("Sample Scene");
        }
    }
}
