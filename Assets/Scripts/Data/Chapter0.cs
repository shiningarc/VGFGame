using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Զ���VGF.Plot��ʵ�ֳ���ת������
namespace VGF.Plot
{
    public class Chapter0 : ChapterBase
    {
        //����ת��
        public override void Run()
        {
            //�󶨳����¼�
            BindSceneEvent("Sample Scene", (msg) =>
            {
                VGF_Player.Instance.transform.position = GameObject.Find("���").transform.position;
                Caption("����");

            });
            
            //�ƶ�����
            SceneMove("Sample Scene");
        }
    }
}
