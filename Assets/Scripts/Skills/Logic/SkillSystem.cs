using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public SkillList_SO skillList_SO;
    public int displayNum;
    private int displayCnt;
    public List<GameObject> skillSlots;
    public void Start()
    {
        EventHandler.NewGame += Init;
        EventHandler.OnSkillRelease += CoolSkill;
    }
    private void OnDestroy()
    {
        EventHandler.NewGame -= Init;
        EventHandler.OnSkillRelease -= CoolSkill;
    }
    private void Init()
    {
        DataCollection.LoadSkills(skillList_SO.SkillsPrefab);
        displayCnt = 0;
        foreach (var Skillset in DataCollection.SkillDic.Where(skill => skill.Key.Unlocked))
        {
            if (displayCnt < displayNum)
            {
                
                skillSlots[displayCnt].GetComponent<SkillSlot>().RefreshUI(Skillset.Key);
                skillSlots[displayCnt].SetActive(true);
                displayCnt++;
            }
        }
    }
    private void CoolSkill(string name)
    {
        var skillSlot = skillSlots.Find((i) => { return i.GetComponent<SkillSlot>().skillName == name; }).GetComponent<SkillSlot>();
        skillSlot.Cool();
    }
    private void Update()
    {
        



    }

    private void DeInit()
    {

    }
}
