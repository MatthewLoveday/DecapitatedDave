using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : ReactorBase 
{
    [Header("References")]
    [SerializeField]
    Rigidbody2D rBody;

    [SerializeField]
    TargetJoint2D tJoint;

    [SerializeField]
    LevelManager levelManager;

    [Header("Mouse Tracking")]
    [SerializeField]
    bool mouseOver;

    [SerializeField]
    bool Holding;

    //Can optionally only enable certain things when being dragged
    [Header("OPTIONAL")]
    [SerializeField]
    ReactorBase Linked;

    private void Start()
    {
        if(!rBody)
        {
            rBody = GetComponent<Rigidbody2D>();
        }

        if(!tJoint)
        {
            if(!GetComponent<TargetJoint2D>())
            {
                //Initialize Target Joint
                tJoint = gameObject.AddComponent<TargetJoint2D>();

                tJoint.maxForce = 1000;
                tJoint.dampingRatio = 1;
                tJoint.frequency = 5;
                tJoint.autoConfigureTarget = false;
            }
            else
            {
                tJoint = GetComponent<TargetJoint2D>();
            }

        }

        if(!levelManager)
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }
    }

    private void Update()
    {
        if(!IsEnabled)
        {
            return;
        }

        if(!rBody)
        {
            Debug.LogError("Error! Draggable must have rigidbody");
            return;
        }


        if(mouseOver && Input.GetMouseButton(0) && !levelManager.HoldingObject)
        {
            Holding = true;
            levelManager.HoldingObject = true;
            levelManager.HeldObject = gameObject;
        }
        else if(!(Holding && Input.GetMouseButton(0))) //Check to see if is still held down
        {
            Holding = false;
            if(levelManager.HeldObject == gameObject)
            {
                levelManager.HoldingObject = false;
                levelManager.HeldObject = null;
            }
        }

        if(Holding)
        {
            tJoint.enabled = true;
            tJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
        else
        {
            tJoint.enabled = false;
        }

        if(Linked)
        {
            Linked.IsEnabled = Holding; 
        }
        

    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
