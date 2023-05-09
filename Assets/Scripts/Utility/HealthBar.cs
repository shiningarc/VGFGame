using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int HP = 1;
    protected int TargetHP;
    public int MaxHP = 100;
    public Image healthBarUI;
    public UnityEvent OnDie;
    void Start()
    {
        healthBarUI.fillAmount = 1;
    }

    public virtual void DoDamage(int Damage)
    {
        if(Damage > 0)
        {
            if (HP <= Damage)
            {
                TargetHP = 0;
    
            }
            else
            {
                TargetHP = HP - Damage;
            }    
        }
        else if(Damage < 0)
        {
            if(HP-Damage >= MaxHP)
            {
                TargetHP = HP - Damage;
            }
            else
            {
                TargetHP = MaxHP;
            }

        }
        StartCoroutine(ChangeHPBarUI());
    }
    IEnumerator ChangeHPBarUI()
    {
        
        while(HP!=TargetHP)
        {
            healthBarUI.fillAmount = (float)HP / (float)MaxHP;
            if (HP < TargetHP)
            {
                HP++;
            }
            else if (HP > TargetHP)
            {
                HP--;
            }
            yield return null;
        }
        if (HP == 0)
        {
            yield return new WaitForSeconds(1f);
            OnDie?.Invoke();
        }
            

    }

}
