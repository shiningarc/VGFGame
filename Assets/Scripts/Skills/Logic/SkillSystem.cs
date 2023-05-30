using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SkillSystem : MonoBehaviour
{
    public SkillList_SO skillList_SO;
    public int displayNum;
    private int displayCnt;
    //public List<GameObject> skillSlots;
    public SkillUI skillUI;

    private static Dictionary<Skill, Action> SkillDic = new Dictionary<Skill, Action>();

    private static Action<string> OnSkillRelease;
    private static Action<string> OnSkillUnlocked;
    private  static  Action<string,bool> OnSkillChangeAvalibility;
    public void Start()
    {
        EventHandler.NewGame += Init;
        EventHandler.LoadGame+= Init;
        OnSkillRelease += CoolSkill;
        OnSkillChangeAvalibility += SetAvaliable;
    }
    private void OnDestroy()
    {
        EventHandler.NewGame -= Init;
        EventHandler.LoadGame -= Init;
        OnSkillRelease -= CoolSkill;
        OnSkillChangeAvalibility -= SetAvaliable;
    }
    private void Init()
    {
        LoadSkills(skillList_SO.SkillsPrefab);
        displayCnt = 0;
        skillUI.InitUI(skillList_SO.SkillsPrefab);
    }

    private void CoolSkill(string name)
    {
        skillUI.CoolSkill(name);
    }

    public static void UnlockSkill(string name)
    {
        var skill = SearchSkill(name);
        skill.Unlocked = true;
        OnSkillUnlocked?.Invoke(name);
    }

    public static void SetSkillAvailable(string name,bool isAvaliable)
    {
        var skill = SearchSkill(name);
        skill.Avaliable = isAvaliable;
        OnSkillChangeAvalibility?.Invoke(name,isAvaliable);
    }

    public void SetAvaliable(string name,bool isAvaliable)
    {
        skillUI.ChangeSkillAvablility(name,isAvaliable);
    }

    public void RefreshBarUI()
    {

    }

    #region 技能目录
    public static void AddActionToSkillDic(Action action, string name, bool isAdd = true)
    {
        var skill = SearchSkill(name);
        if (skill == null)
        {
            Debug.LogError("没有在初始化中加入此技能");
            return;
        }
        if (isAdd)
            SkillDic[skill] = SkillDic[skill] + action;
        else if (action != null)
            SkillDic[skill] = SkillDic[skill] - action;
    }
    public static Skill SearchSkill(string name)
    {
        foreach (KeyValuePair<Skill, Action> kvp in SkillDic)
        {
            if (kvp.Key.Name == name)
            {
                return kvp.Key;
            }
        }
        return null;
    }
    public void LoadSkills(List<Skill> skills)
    {
        SkillDic = new Dictionary<Skill, Action>();
        for (int i = 0; i < skills.Count; i++)
        {
            SkillDic.Add(skills[i], null);
        }
    }
    #endregion
    public static void ReleaseSkill(string name)
    {
        var skill = SearchSkill(name);
        if (skill == null)
        {
            Debug.LogError($"没有在初始化中加入此技能{name}");
            return;
        }
        if (!skill.Avaliable || !skill.Unlocked) return;
        OnSkillRelease?.Invoke(name);
        SkillDic[skill]?.Invoke();
    }
    
    private void Update()
    {
        



    }
    void Refesh()
    {

    }
    private void DeInit()
    {

    }
}
