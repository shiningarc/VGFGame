using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VGF;

public class EnterYourName : MonoBehaviour
{
    public InputField nameText;
    public Button BtnStart;
    void Start()
    {
        BtnStart.onClick.AddListener(() =>
        {
            if(nameText.text.Length>0)
            {
                Settings.PlayerName = nameText.text;
                GlobalSystem.NewGame();

            }
        });
    }

   
    void Update()
    {
        //if(Input.anyKey)
        //{
        //    if (nameText.text.Length > 100) return;
        //    nameText.text += Input.inputString;
        //}
    }
}
