using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField] int hpOpponent = 10;
    public void ApplyDamage(int damage)
    {
        hpOpponent -= damage;
    }

    public void CalculateDamage(ref int damage)
    {
        damage = 1;
    }

    public void CheckState()
    {
        if (hpOpponent <= 0)
        {
            Destroy(gameObject);
        }
    }
}
