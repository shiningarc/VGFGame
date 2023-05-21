using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public string skillName;
    public string skillCoolTime;
    private bool mIsAvaliable;
    
    public bool isAvaliable
    {
        get
        {
            return mIsAvaliable;
        }
        set
        {
            mIsAvaliable = value;
            if(mIsAvaliable)
            {
                timer1.StartCnt();
                unAvaliableImage.enabled = false;
            }
            else
            {
                unAvaliableImage.enabled = true;
            }
        }
    }
    private TimeCounter timer1;
    public Image skillImage;
    public Image MaskImage;
    public Image unAvaliableImage;

    void Start()
    {
        timer1 = gameObject.AddComponent<TimeCounter>();
        skillImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MaskImage.fillAmount = 1-timer1.timeCnt / timer1.setTime;
    }

    public void RefreshUI(Skill skill)
    {
        timer1.setTime = skill.DurationTime;
        skillName = skill.Name;
        skillImage.sprite = skill.sprite;
        isAvaliable = skill.Avaliable;
    }

    public void Cool()
    {
        timer1.StartCnt();
    }
}
