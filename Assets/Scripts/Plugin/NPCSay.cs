using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnFramework;
using WordZone;
using VGF.UI;


//自定义VGF.NPC库，实现NPC说话功能
namespace VGF.NPC
{
    public class NPCSay : MonoBehaviour, ICharacter
    {
        public List<string> dialoguePiece = new List<string>();
        public bool canSay;
        [Autowired]
        public static WordZone.WordZone wordZone;

        //逐帧更新状态
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

        //按下E键说话
        public void OnPlayerEnter()
        {
            HintLoader.Instance.HintOn("Press E to Talk");
            canSay = true;
        }

        //提示关闭对话
        public void OnPlayerExit()
        {
            HintLoader.Instance.HintOff();
            canSay = false;
        }
    }
}
