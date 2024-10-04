using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    public List<GameObject> panels;

    public void Show(int panelID)
    {
        for (int i = 0; i < panels.Count; ++i)
        {
            panels[i].SetActive(i == panelID);
        }
    }
}
