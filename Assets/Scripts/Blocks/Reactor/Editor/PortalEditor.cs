using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor 
{
    private Portal reference;

    public void OnSceneGUI()
    {
        reference = this.target as Portal;

        Handles.color = Color.green;
        Handles.RectangleHandleCap(0, reference.transform.position, Quaternion.Euler(reference.transform.forward), 1f, EventType.Repaint);

        if (reference.partner)
        {
            Handles.color = Color.blue;
            Handles.RectangleHandleCap(0, reference.partner.transform.position, Quaternion.Euler(reference.transform.forward), 1f, EventType.Repaint);

            Handles.color = Color.white;
            Handles.DrawDottedLine(reference.transform.position, reference.partner.transform.position, 5f);
        }
    }
}
