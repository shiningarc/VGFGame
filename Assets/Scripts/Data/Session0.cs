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
                Word("[v 5]唔~~[Halt 2][v 10]多么美好的一天啊");
                Word("[v 10]啊不对");
                Word("[v 10]得快到<color=red>银行</color>去");
                at("Alex").Interactive(() =>
                {
                    Debug.Log("Say");
                    at("Alex").Say("别打扰我");

                })
                .Interactive(() =>
                {
                    at("Alex").Say("我要生气咯");
                })
                .Interactive(() =>
                {
                    at("Alex").Say("我真的要生气咯");
                })
                .Interactive(() =>
                {
                    Word("[v 10]<size=60><color=red>扣你10点血</color></size>");
                    EventHandler.CallDoDamage2Player(10);
                },true);

                at("Blake").Interactive(() =>
                {
                    Word("你好");
                },true);
                Arrival("人行道前", (msg) =>
                {
                    Debug.Log("2333");
                    Word("咻————[halt 1 v 10]一辆辆汽车疾驰而过。让你感到有些紧张");
                });
                AutoSave();
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

