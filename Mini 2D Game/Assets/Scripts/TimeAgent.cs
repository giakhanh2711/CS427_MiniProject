using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick; // To spawn method every time tick

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        GameManager.instance.dayNightController.Subscribe(this);
    }

    public void Invoke()
    {
        onTimeTick?.Invoke();
    }

    private void OnDestroy()
    {
        GameManager.instance.dayNightController.Unsubscribe(this);
    }
}
