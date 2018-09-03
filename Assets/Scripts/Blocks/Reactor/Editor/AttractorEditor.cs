using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Attractor))]
public class AttractorEditor : Editor 
{
    private Attractor reference;

    public void OnSceneGUI()
    {
        reference = this.target as Attractor;

        
        Handles.color = reference.forceType == ForceType.Attract ? Color.green : Color.red;
        Handles.DrawWireDisc(reference.transform.position, reference.transform.forward, reference.radius);
    }

    public override void OnInspectorGUI()
    {
        reference = this.target as Attractor;
        DrawDefaultInspector();
        
        reference.radius = Mathf.Max(0, reference.radius);
    }
}
