using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepMenu : MonoBehaviour
{
    [SerializeField] DisableControl disableControl;
    [SerializeField] GameObject iconHighLight;
    [SerializeField] DoorOpenClose house;

    private bool isSleep;
   
    public void YesSleep()
    {
        disableControl.DisablePlayerControl();
        //disableControl.gameObject.SetActive(false);
        iconHighLight.SetActive(false);
        gameObject.SetActive(false);
        house.CloseDoor();
        isSleep = true;
    }

    public void SetSleep(bool isSleep)
    {
        this.isSleep = isSleep;
    }

    public bool IsSleep()
    {
        return isSleep;
    }

    public void NoSleep()
    {
        gameObject.SetActive(false);
        house.CloseDoor();
        isSleep = false;
    }
}
