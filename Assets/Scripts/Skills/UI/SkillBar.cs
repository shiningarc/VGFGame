using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Text SkillName;
    public Image SkillImage;
    public Text SkillDescription;
    public Text Unlocked;
    public void UpdateSkillBar(Skill skill)
    {
        SkillName.text = skill.Name;
        SkillImage.sprite = skill.sprite;
        SkillDescription.text = skill.Description;
        if (skill.Unlocked)
            Unlocked.text = "Unlocked";
        else
            Unlocked.text = "Locked";
    }
}
