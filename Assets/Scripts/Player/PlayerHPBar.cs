using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHPBar : HealthBar
{
    public Text healthBarText;
   
    private void Awake()
    {
        EventHandler.NewGame += Init;
        EventHandler.LoadGame += Init;  
    }
    private void OnDestroy()
    {
        EventHandler.NewGame -= Init;
        EventHandler.LoadGame -= Init;
        EventHandler.DoDamage2Player -= DoDamage;
        //EventHandler.PlayerDie -= TurnToDeadScene;
    }
    public void Init()
    {
        HP = DataCollection.playerHP;
        MaxHP = DataCollection.playerMaxHP;
        healthBarUI.fillAmount = (float)HP / MaxHP;
        OnDie.AddListener(OnPlayerDie);
        AfterDie.AddListener(AfterPlayerDie);
        //EventHandler.PlayerDie += TurnToDeadScene;
        EventHandler.DoDamage2Player += DoDamage;
    }
    private void Update()
    {
        healthBarText.text = $"{HP}/{MaxHP}";
    }
    public void OnPlayerDie()
    {
        EventHandler.CallPlayerDie();
    }
    public void AfterPlayerDie()
    {
        TurnToDeadScene();
    }

    public override void DoDamage(int Damage)
    {
        base.DoDamage(Damage);
        DataCollection.playerMaxHP = MaxHP;
        DataCollection.playerHP = HP;
    }

    public void TurnToDeadScene()
    {
        SceneManager.LoadScene("GameOver");
    }


}
