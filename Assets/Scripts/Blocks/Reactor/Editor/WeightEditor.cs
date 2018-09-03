using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weight))]
public class WeightEditor : Editor 
{
    private Weight reference;

    public override void OnInspectorGUI()
    {
        reference = this.target as Weight;
        
        DrawDefaultInspector();
        
        reference.weight = Mathf.Max(0, reference.weight);
    }
}
