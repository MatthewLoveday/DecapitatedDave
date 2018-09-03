using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Moving))]
public class MovingEditor : Editor 
{
    private Moving reference;

    public void OnSceneGUI()
    {
        reference = this.target as Moving;
        Vector2 refPosition = reference.transform.position;

        if(Application.isPlaying)
        {
            refPosition = reference.InitialPosition;
        }

        for (int i = 0; i < reference.points.Length; i++)
        {
            Handles.color = Color.red;

            Handles.DrawWireDisc(reference.points[i] + refPosition, reference.transform.forward, reference.SnapRadius);
            
            Handles.color = Color.white;

            if(i != 0)
            {
                Handles.DrawDottedLine(reference.points[i-1] + refPosition, reference.points[i] + refPosition, 5f);

                //Draw point from this one to start
                if(i == reference.points.Length - 1 && reference.Loop)
                {
                     Handles.DrawDottedLine(reference.points[i] + refPosition, reference.points[0] + refPosition, 5f);
                }
            }
        }

    }

}
