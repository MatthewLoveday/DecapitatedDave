using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    [SerializeField]
    Transform Head;

    private Vector3 initialHeadPosition = Vector3.zero;
    private Vector3 initialCameraPosition = Vector3.zero;

    bool LockY = false;

    private void Start()
    {
        if(Head == null)
        {
            Head = GameObject.FindGameObjectWithTag("Player").transform;
        
            initialHeadPosition = Head.position;
        }

        initialCameraPosition = transform.position;

        transform.position = new Vector3(initialHeadPosition.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        Vector3 targetPosition; 

        if (LockY)
        {
            targetPosition = new Vector3(Head.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(Head.position.x, Head.position.y, transform.position.z);
        }

        if(targetPosition.x < initialHeadPosition.x)
        {
            //Don't move further back than x 0
            targetPosition.x = initialHeadPosition.x;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);


    }


}
