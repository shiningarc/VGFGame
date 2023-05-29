using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Plot;
public class PreSession : SessionBase
{
    public override void Run()
    {
        SceneMoveThen("Home", () =>
        {
            AutoSave();
            Word("hhhh",Settings.PlayerName);
            Arrival("Door", (msg) =>
            {
                VGF();
            },"出门","快取钱");
            at("Mirror_Interact").Interactive(() =>
            {
                Word("看着镜子，你感觉你很帅","NULL");
            }, true);
            at("Table_Interact").Interactive(() =>
            {
                Word("桌子上有一个香蕉，看起来很好吃", "NULL");
            }, true);
            at("Chair_Interact").Interactive(() =>
            {
                Word("柔软的椅子，很适合放松", "NULL");
            }, true);
        });
        


    }
}
