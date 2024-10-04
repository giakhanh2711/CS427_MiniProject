using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] GameObject respawnPointPosition;
    //[SerializeField] string spawnScene;
    internal void StartRespawn()
    {

        Vector3 pos = respawnPointPosition.transform.position;
        GameManager.instance.Respawn(pos);
    }
}
