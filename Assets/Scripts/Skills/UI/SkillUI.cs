using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    [SerializeField] private SkillSlot[] skillSlots;
    public Transform RuleItemRoot;          //存放背包格子的父物体
    public SkillBar skillBarPrefab;
    public GameObject SkillPanel;
    public Button btnOpen;
    public Button btnClose;
    private bool isOpen = false;
    public void RefreshSlotUI(List<Skill> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            skillSlots[i].RefreshUI(skills[i]);
        }
    }
    public void InitUI(List<Skill> skills)
    {
        List<Skill> unlockSkills = new List<Skill>();
        if (RuleItemRoot.childCount > 0)
        {
            for (int i = 0; i < RuleItemRoot.childCount; i++)
            {
                Destroy(RuleItemRoot.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].Unlocked)
            {
                unlockSkills.Add(skills[i]);
            }
            var skillBar = Instantiate(skillBarPrefab, RuleItemRoot);
            skillBar.UpdateSkillBar(skills[i]);
            skillBar.gameObject.SetActive(true);
        }
        RefreshSlotUI(unlockSkills);
    }

    public void CoolSkill(string name)
    {
        foreach(SkillSlot slot in skillSlots)
        {
            if(slot.skillName == name)
            {
                slot.Cool();
            }
        }
    }

    public void UnlockSkill(string name)
    {

    }

    public void ChangeSkillAvablility(string name,bool isAvaliable)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if (slot.skillName == name)
            {
                slot.isAvaliable = isAvaliable;
            }
        }
    }
    void Start()
    {
        btnOpen.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                OpenInventoryUI();
            }
        });
        btnClose.onClick.AddListener(() =>
        {
            if (isOpen)
            {
                CloseInventoryUI();
            }
        });

    }
    void OpenInventoryUI()
    {
        SkillPanel.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
    }

    //背包关闭的监测
    void CloseInventoryUI()
    {
        SkillPanel.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
    }
}
