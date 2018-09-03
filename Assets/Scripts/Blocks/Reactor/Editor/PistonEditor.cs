using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Piston))]
public class PistonEditor : Editor 
{
    private Piston reference;

    public void OnSceneGUI()
    {
        reference = this.target as Piston;

        
        Handles.color = Color.red;
        
        Handles.DrawLine(reference.transform.position, reference.transform.position + (reference.transform.right * reference.extendedDistance));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        reference = this.target as Piston;

        if(GUILayout.Button("Exend"))
        {
            reference.IsEnabled = true;
        }

        if(GUILayout.Button("Retract"))
        {
            reference.IsEnabled = false;
        }
    }
}
