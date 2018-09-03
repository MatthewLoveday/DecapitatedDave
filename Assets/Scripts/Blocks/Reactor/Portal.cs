using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : ReactorBase 
{
    [Header("References")]
    
    //The linked partner
    [SerializeField]
    public Portal partner;

    bool used;
    private float counter = 0f;
    public bool AcceptEntites = false;


    private void Start()
    {
        used = false;
    }

    private void Update()
    {
        if(IsEnabled)
        {
            if(used == true)
            {
                counter += Time.deltaTime;
            
                if(counter > .2f)
                {
                    used = false;
                    counter = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsEnabled)
        {
            return;
        }

        if(collision.tag == "Player"  || collision.tag == "Entity")
        {
            if(!AcceptEntites && collision.tag == "Entity")
            {
                return;

            }

            //Detect if player has just come out of portal
            if(partner.used)
            {
                return;
            }

            Collider2D player = collision;
            
            Vector2 ExitTransform = Vector2.zero;
            Vector2 ExitVelocity = Vector2.zero;

            //First figure out the new position for the ball to exit
            ExitTransform = transform.InverseTransformPoint(player.transform.position);
            
            ExitVelocity = transform.InverseTransformDirection(player.GetComponent<Rigidbody2D>().velocity);
            
            Debug.Log("Input Exit Transform: " + ExitTransform);

            ExitTransform = partner.transform.TransformPoint(ExitTransform);
            ExitVelocity = partner.transform.TransformDirection(ExitVelocity);

            Debug.Log("Output Exit Transform: " + ExitTransform);

            player.transform.position = ExitTransform;
            player.GetComponent<Rigidbody2D>().velocity = ExitVelocity;

            //Mark portal as used
            used = true;
        }
    }

  
}
