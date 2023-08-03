using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int maxVal;
    public int curVal;

    public Stat(int cur, int maxVal)
    {
        this.maxVal = maxVal;
        this.curVal = cur;
    }

    internal void Subtract(int amount)
    {
        curVal -= amount;
    }

    internal void Add(int amount)
    {
        curVal += amount;

        if (curVal > maxVal)
            curVal = maxVal;
    }

    internal void SetToMax()
    {
        curVal = maxVal;
    }
}

public class Character : MonoBehaviour, IDamageable
{
    public Stat hp;
    [SerializeField] StatusBar hpBar;

    public Stat stamina;
    [SerializeField] StatusBar staminaBar;
    
    public bool isDead;
    public bool isExhausted;

    DisableControl disableControl;
    PlayerRespawn playerRespawn;

    private void Awake()
    {
        disableControl = GetComponent<DisableControl>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.curVal, stamina.maxVal);
    }

    private void UpdateHPBar()
    {
        hpBar.Set(hp.curVal, hp.maxVal);
    }

    public void TakeDamage(int amount)
    {
        if (isDead == true)
            return;

        hp.Subtract(amount);

        if (hp.curVal <= 0)
        {
            Dead();
        }

        UpdateHPBar();
    }

    private void Dead()
    {
        isDead = true;
        disableControl.DisablePlayerControl();
        playerRespawn.StartRespawn();
    }

    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHPBar();
    }

    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHPBar();
    }

    public void GetTired(int amount)
    {
        stamina.Subtract(amount);

        if (stamina.curVal <= 0)
            Exhausted();

        UpdateStaminaBar();
    }

    private void Exhausted()
    {
        isExhausted = true;
        disableControl.DisablePlayerControl();
        playerRespawn.StartRespawn();
    }

    public void GetRest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    public void GetFullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetTired(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GetRest(10);
        }
    }

    public void CalculateDamage(ref int damage)
    {
        
    }

    public void ApplyDamage(int damage)
    {
        TakeDamage(damage);
    }

    public void CheckState()
    {
        
    }
}
