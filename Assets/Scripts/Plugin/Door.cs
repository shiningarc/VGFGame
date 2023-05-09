using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.SceneSystem;
using AutumnFramework;

public class Door : MonoBehaviour
{
    [Header("要去的场景名称")]
    public string SceneName;
    SceneLoader sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = Autumn.Harvest<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            sceneLoader.SwitchSceneByName(SceneName);
        }
    }
}
