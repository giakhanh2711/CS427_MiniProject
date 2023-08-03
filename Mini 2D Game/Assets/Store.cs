using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : InteractableObject
{
    public Container storeContainer;
    public float buyFromPlayerMulti = 1f;
    public float sellToPlayerMulti = 1.5f;
    public override void BeInteracted(Character character)
    {
        Trading trading = character.GetComponent<Trading>();
        
        if (trading == null)
            return;
        
        trading.BeginTrading(this);
    }
}
