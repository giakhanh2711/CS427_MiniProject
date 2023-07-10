using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : HitByToolObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject log; // Log prefab
    [SerializeField] int logCount = 5;
    [SerializeField] float spreadRadius = 1f;

    public override void Hit()
    {
        while (logCount > 0)
        {
            --logCount;

            Vector3 position = transform.position;
            position.x -= spreadRadius * UnityEngine.Random.value - spreadRadius / 2;
            position.y += spreadRadius * UnityEngine.Random.value - spreadRadius / 2;

            // Instantiate prefab

            GameObject logItem = Instantiate(log, position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
