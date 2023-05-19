using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public string skillName;
    public string skillCoolTime;
    private TimeCounter timer1;
    public Image skillImage;

    void Start()
    {
        timer1 = gameObject.AddComponent<TimeCounter>();
        skillImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        skillImage.fillAmount = timer1.timeCnt / timer1.setTime;
    }

    public void RefreshUI(Skill skill)
    {
        timer1.setTime = skill.DurationTime;
        skillName = skill.Name;
        skillImage.sprite = skill.sprite;
        timer1.StartCnt();
    }

    public void Cool()
    {
        timer1.StartCnt();
    }
}
