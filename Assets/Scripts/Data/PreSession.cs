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
            },"����","��ȡǮ");
            at("Mirror_Interact").Interactive(() =>
            {
                Word("���ž��ӣ���о����˧","NULL");
            }, true);
            at("Table_Interact").Interactive(() =>
            {
                Word("��������һ���㽶���������ܺó�", "NULL");
            }, true);
            at("Chair_Interact").Interactive(() =>
            {
                Word("��������ӣ����ʺϷ���", "NULL");
            }, true);
        });
        


    }
}
