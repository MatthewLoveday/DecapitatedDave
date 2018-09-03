using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : ReactorBase {

    [Header("References")]
    [SerializeField]
    GameObject pistonHead;
    
    [SerializeField]
    Rigidbody2D headRBody;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip;
    
    [Header("Piston Properties")]
    [SerializeField]
    public float extendedDistance = 0.8f;

    [SerializeField]
    float extendSpeed = 1f;

    bool previousState;
    

    private void Start()
    {
        if(pistonHead == null) { pistonHead = transform.GetChild(0).gameObject; }
        if(headRBody == null) { headRBody = pistonHead.GetComponent<Rigidbody2D>(); }
        if(!source) { source = GetComponent<AudioSource>(); }
    }

    private void FixedUpdate()
    {
        if(IsEnabled)
        {
            Extend();
        }
        else
        {
            Retract();
        }

        if(IsEnabled != previousState)
        {
            source.PlayOneShot(clip);
        }

        previousState = IsEnabled;
    }

    public void Extend()
    {
        Vector2 newPosition;
        newPosition = transform.position + (transform.right * extendedDistance);
        headRBody.MovePosition(Vector2.Lerp(pistonHead.transform.position, newPosition, extendSpeed * Time.deltaTime));
    }

    public void Retract()
    {
        Vector2 newPosition;
        newPosition = transform.position;
        headRBody.MovePosition(Vector2.Lerp(pistonHead.transform.position, newPosition, extendSpeed * Time.deltaTime));
    }

}
