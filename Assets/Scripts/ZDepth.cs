using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepth : MonoBehaviour
{
    Transform transform;
    [SerializeField] bool stationary = true;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = pos.y * 0.0001f;
        transform.position = pos;

        if (stationary)
        {
            Destroy(this);
        }
    }
}
