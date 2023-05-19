using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataCollection 
{
    public static int playerHP;
    public static int playerMaxHP;
    public static Dictionary<Skill, Action> SkillDic = new Dictionary<Skill, Action>();
    public static void AddActionToSkillDic(Action action,string name,bool isAdd = true)
    {
        var skill = SearchSkill(name);
        if (skill == null)
        {
            Debug.LogError("没有在初始化中加入此技能");
            return;
        }
        if(isAdd)
            SkillDic[skill] = SkillDic[skill] + action;
        else if(action != null)
            SkillDic[skill] = SkillDic[skill] - action;
    }
    public static Skill SearchSkill(string name)
    {
        foreach(KeyValuePair<Skill, Action> kvp in SkillDic)
        {
            if(kvp.Key.Name == name)
            {
                return kvp.Key;
            }
        }
        return null;
    }
    public static void ReleaseSkill(string name)
    {
        var skill = SearchSkill(name);
        if (skill == null)
        {
            Debug.LogError("没有在初始化中加入此技能");
            return;
        }
        EventHandler.CallOnSkillRelease(name);
        SkillDic[skill]?.Invoke();
    }
    public static void LoadSkills(List<Skill> skills)
    {
        SkillDic = new Dictionary<Skill, Action>();
        for(int i = 0; i < skills.Count; i++)
        {
            SkillDic.Add(skills[i], null);
        }
    }
}
