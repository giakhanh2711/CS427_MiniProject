using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistant
{
    public string Read();
    public void Load(string jsonString);
}
