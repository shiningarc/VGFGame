using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.Plot
{
    public class Session0 : SessionBase
    {
        public override void Run()
        {
            BindSceneEvent("The Modern City", (msg) =>
            {
                VGF_Player_2D.Instance.transform.position =  GameObject.Find("Origin_City").transform.position;
                //Caption("序章");
                Word("[v 3]唔~~[Halt 2]多么美好的一天啊");
                Word("[v 1]啊不对");
                Word("[v 1]得快到银行去");
                at("Alex").Interactive(() =>
                {
                    Debug.Log("Say");
                    at("Alex").Say("你好");
                });

            });
            BindSceneEvent("The Bank", (msg) =>
            {
                VGF_Player_2D.Instance.transform.position = GameObject.Find("BankEntrance").transform.position;
                //Caption("序章");
                Word("得快去取钱");

            });
            SceneMove("The Modern City");
        }
    }
}

