using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

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
    // HP
    public Stat hp;
    [SerializeField] StatusBar hpBar;
    
    // ENERGY
    public Stat stamina;
    [SerializeField] StatusBar staminaBar;

    // STAR
    public Stat star;
    [SerializeField] StatusBar starBar;

    public int playerLevel = 1;
    [SerializeField] TextMeshProUGUI textLevel;
    
    public bool isDead;
    public bool isExhausted;

    DisableControl disableControl;
    PlayerRespawn playerRespawn;

    public int StartForNextLevel
    {
        get
        {
            return playerLevel * 500;
        }
    }

    private void Awake()
    {
        disableControl = GetComponent<DisableControl>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
        UpdateStarBar();
        UpdateTextLevel();
    }

    public void UpdateTextLevel()
    {
        textLevel.text = "Level " + playerLevel.ToString();
    }

    public void UpdateStarBar()
    {
        starBar.Set(star.curVal, star.maxVal);
    }

    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.curVal, stamina.maxVal);
    }

    private void UpdateHPBar()
    {
        hpBar.Set(hp.curVal, hp.maxVal);
    }

    public void ReceiveStar(int amount)
    {
        star.Add(amount);

        UpdateStarBar();

        if (star.curVal >= StartForNextLevel)
        {
            star.Subtract(StartForNextLevel);
            playerLevel += 1;
            star.maxVal = StartForNextLevel;
            
            UpdateStarBar();
            UpdateTextLevel();
        }
    }

    public int GetLevel()
    {
        return playerLevel;
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
