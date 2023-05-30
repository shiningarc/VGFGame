using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Plot;
public class Session1 : SessionBase
{
    public override void Run()
    {
        SceneMoveThen("The Bank",() =>
        {
            
            MoveTo("BankEnter");
            SetSkillAvaliable("Dash", false);
            SetSkillAvaliable("Heal", true);
            SetSkillAvaliable("Chaos Storm", false);
            //Caption("序章");
            Word("得快去取钱");
            Arrival("BankOut", (msg) =>
            {
                SceneMove("The Modern City");
            }, "", "");
            BindSceneEvent("The Modern City", (msg) =>
            {
                MoveTo("CityEnter2");
                SetSkillAvaliable("Dash", true);
                SetSkillAvaliable("Heal", true);
                SetSkillAvaliable("Chaos Storm", true);
                Arrival("BankDoor", (msg) =>
                {
                    SceneMove("The Bank");
                }, "", "");
                Arrival("HomeDoor", (msg) =>
                {
                    SceneMove("Home");
                }, "", "");

            });
            BindSceneEvent("The Bank", (msg) =>
            {
                MoveTo("BankEnter");
                Arrival("BankOut", (msg) =>
                {
                    SceneMove("The Modern City");
                }, "", "");
                BindSceneEvent("The Modern City", (msg) =>
                {
                    SetSkillAvaliable("Dash", true);
                    SetSkillAvaliable("Heal", true);
                    SetSkillAvaliable("Chaos Storm", true);
                    MoveTo("CityEnter2");
                    Arrival("BankDoor", (msg) =>
                    {
                        SceneMove("The Bank");
                    }, "", "");
                    Arrival("HomeDoor", (msg) =>
                    {
                        SceneMove("Home");
                    }, "", "");
                });
            });
            BindSceneEvent("Home", (msg) =>
            {
                SetSkillAvaliable("Dash", false);
                SetSkillAvaliable("Heal", true);
                SetSkillAvaliable("Chaos Storm", false);
                MoveTo("HomeEnter");
                Arrival("HomeOut", (msg) =>
                {
                    SceneMove("The Modern City");
                }, "", "", false);
                BindSceneEvent("The Modern City", (msg) =>
                {
                    SetSkillAvaliable("Dash", true);
                    SetSkillAvaliable("Heal", true);
                    SetSkillAvaliable("Chaos Storm", true);
                    MoveTo("CityEnter1");
                    Arrival("BankDoor", (msg) =>
                    {
                        SceneMove("The Bank");
                    }, "", "");
                    Arrival("HomeDoor", (msg) =>
                    {
                        SceneMove("Home");
                    }, "", "");
                });
            });
            AutoSave();
        });
        
    }

    
}
