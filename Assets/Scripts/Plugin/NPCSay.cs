using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;
using WordZone;
using VGF.UI;


//�Զ���VGF.NPC�⣬ʵ��NPC˵������
namespace VGF.NPC
{
    public class NPCSay : MonoBehaviour, ICharacter
    {
        public List<string> dialoguePiece = new List<string>();
        public bool canSay;
        [Autowired]
        public static WordZone.WordZone wordZone;

        //��֡����״̬
        void Update()
        {
            if (canSay)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HintLoader.Instance.HintOff();
                    for (int i = 0; i < dialoguePiece.Count; i++)
                    {
                        wordZone.ParseAndEnque(dialoguePiece[i]);
                    }
                }
            }
        }

        //����E��˵��
        public void OnPlayerEnter()
        {
            HintLoader.Instance.HintOn("Press E to Talk");
            canSay = true;
        }

        //��ʾ�رնԻ�
        public void OnPlayerExit()
        {
            HintLoader.Instance.HintOff();
            canSay = false;
        }
    }
}
