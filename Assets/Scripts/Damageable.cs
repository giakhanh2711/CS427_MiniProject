using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    IDamageable damageableI;
    internal void TakeDamage(int damageAmount)
    {
        if (damageableI == null)
            damageableI = GetComponent<IDamageable>();

        damageableI.CalculateDamage(ref damageAmount);
        damageableI.ApplyDamage(damageAmount);
        //Destroy(gameObject);
        //GameManager.instance.messageSystem.PostMessage(transform.position, damageAmount.ToString());
        //Debug.Log("Posted Message");
        damageableI.CheckState();
    }
}
