using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : ReactorBase 
{
    [Header("References")]
    [SerializeField]
    private Rigidbody2D rBody;
    
    [Header("Weight Configuration")]
    public float weight = 10f;

    private void Start()
    {
        if(!rBody)
        {
            rBody = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if(IsEnabled)
        {
            rBody.mass = weight;
        }
        else
        {
            rBody.mass = 0;
        }
    }
}
