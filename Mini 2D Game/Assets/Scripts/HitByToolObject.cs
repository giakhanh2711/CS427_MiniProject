using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByToolObject : MonoBehaviour
{
    public virtual void Hit()
    {

    }

    public virtual bool CanBeHit(List<ResourceNodeType> types)
    {
        return true;
    }
}
