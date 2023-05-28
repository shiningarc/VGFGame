using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCanvas : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.onValueChanged.AddListener(OnvalueChange);
    }
    public void OnvalueChange(float value)
    {
        SoundManager.Instance.audioSource.volume = value;
    }
}
