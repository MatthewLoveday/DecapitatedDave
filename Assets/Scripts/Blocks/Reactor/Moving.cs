using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : ReactorBase 
{
    [Header("Configuration")]
    [SerializeField]
    public Vector2[] points;

    //Internal State Variables
    int currentPoint;

    [SerializeField] 
    [Tooltip("How close does the platform have to be in order to start moving to the next point")]
    public float SnapRadius;

    [SerializeField]
    [Tooltip("This affects the dampening on the movement, Sharp is a consistent speed whereas smooth starts slow and ends slow, but is fast in the middle")]
    MoveType moveType;

    [SerializeField]
    float moveSpeed = 10f;

    public bool Loop = true;
    //if not looping we need to store the direction of travel
    private bool forward = true;

    private Vector2 initialPosition;
    public Vector2 InitialPosition { get { return initialPosition; }}

    private void Start()
    {
        initialPosition = transform.position;
        currentPoint = 0;
    }

    private void Update()
    {
        if(IsEnabled)
        {
            //Get Next Point
            if(Vector2.Distance(transform.position, points[currentPoint] + initialPosition) < SnapRadius)
            {
                if(Loop)
                {
                    currentPoint += 1;
                    if(currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }
                else
                {
                    if(forward)
                    {
                        currentPoint += 1;
                        if(currentPoint >= points.Length)
                        {
                            forward = false;
                            currentPoint--;
                        }
                    }
                    else
                    {
                        currentPoint -= 1;
                        if(currentPoint < 0)
                        {
                            forward = true;
                            currentPoint++;
                        }
                    }
                }
            }

            //Move towards point
            if(moveType == MoveType.Sharp)
            {
                Debug.Log("Movement Sharp");
                transform.position = Vector2.MoveTowards(transform.position, points[currentPoint] + initialPosition, moveSpeed * Time.deltaTime);
            }
            else if(moveType == MoveType.Smooth)
            {
                Debug.Log("Movement Smooth");
                transform.position = Vector3.Lerp(transform.position, points[currentPoint] + initialPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
}


public enum MoveType
{
    Sharp, Smooth
}