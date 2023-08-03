using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Container))]
public class ContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Container container = target as Container;
        if (GUILayout.Button("Clear container"))
        {
            for (int i = 0; i < container.slots.Count; ++i)
            {
                container.slots[i].Clear();
            }
        }
        DrawDefaultInspector();
    }
}
