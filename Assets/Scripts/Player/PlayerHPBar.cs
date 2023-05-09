using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHPBar : HealthBar
{
    private void Awake()
    {
        EventHandler.NewGame += Init;
    }
    private void OnDestroy()
    {
        EventHandler.NewGame -= Init;
        EventHandler.DoDamage2Player -= DoDamage;
        //EventHandler.PlayerDie -= TurnToDeadScene;
    }
    public void Init()
    {
        HP = DataCollection.playerHP;
        MaxHP = DataCollection.playerMaxHP;
        healthBarUI.fillAmount = (float)HP / MaxHP;
        OnDie.AddListener(OnPlayerDie);
        EventHandler.PlayerDie += TurnToDeadScene;
        EventHandler.DoDamage2Player += DoDamage;
    }
    
    public void OnPlayerDie()
    {
        EventHandler.CallPlayerDie();
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
