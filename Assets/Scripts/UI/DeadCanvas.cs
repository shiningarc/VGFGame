using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VGF;

public class DeadCanvas : MonoBehaviour
{
    public Button BtnReturn;
    void Start()
    {
        BtnReturn.onClick.AddListener(Return2MainMenu);
    }
    public void Return2MainMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
