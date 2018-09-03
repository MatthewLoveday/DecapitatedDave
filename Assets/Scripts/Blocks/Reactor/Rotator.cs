using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : ReactorBase 
{
    //Used for buttons
    [Header("References")]
    [SerializeField]
    Transform platform;

    [Header("Configuration")]
    [SerializeField]
    public bool manualRotator = true;

    [SerializeField]
    Direction direction = Direction.Right;

    [SerializeField]
    float rotatorSpeed = 80f;

    [SerializeField]
    float minimumAngleLimit, maxmimumAngleLimit;

    private void Update()
    {
        if(manualRotator)
        {
            float intendedRotation = platform.rotation.eulerAngles.z;

            if(Input.GetKey(KeyCode.Q))
            {
                intendedRotation += (rotatorSpeed * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.E))
            {
                intendedRotation += -(rotatorSpeed * Time.deltaTime);
            }

            if(minimumAngleLimit != 0 || maxmimumAngleLimit != 0)
            {
                intendedRotation = ClampAngle(intendedRotation, minimumAngleLimit, maxmimumAngleLimit);
            }

            platform.eulerAngles = new Vector3(0f,0f, intendedRotation);

        }
        else
        {
            float intendedRotation = platform.rotation.eulerAngles.z;

            if(IsEnabled)
            {
                //Input overrides Switch or anything
                intendedRotation += (rotatorSpeed * Time.deltaTime) * (direction == Direction.Right ? 1f : -1f);
            }
            else
            {
                //Rotate in reverse direction
                intendedRotation += (rotatorSpeed * Time.deltaTime) * (direction == Direction.Right ? -1f : 1f);
            }

            if(minimumAngleLimit != 0 || maxmimumAngleLimit != 0)
            {
                intendedRotation = ClampAngle(intendedRotation, minimumAngleLimit, maxmimumAngleLimit);
            }

            platform.eulerAngles = new Vector3(0f,0f, intendedRotation);
        }
    }

     public static float ClampAngle(float angle, float min, float max)
     {
         angle = Mathf.Repeat(angle, 360);
         min = Mathf.Repeat(min, 360);
         max = Mathf.Repeat(max, 360);
         bool inverse = false;
         var tmin = min;
         var tangle = angle;

         if(min > 180)
         {
             inverse = !inverse;
             tmin -= 180;
         }

         if(angle > 180)
         {
             inverse = !inverse;
             tangle -= 180;
         }

         var result = !inverse ? tangle > tmin : tangle < tmin;
         if(!result)
             angle = min;

         inverse = false;
         tangle = angle;
         var tmax = max;

         if(angle > 180)
         {
             inverse = !inverse;
             tangle -= 180;
         }

         if(max > 180)
         {
             inverse = !inverse;
             tmax -= 180;
         }
 
         result = !inverse ? tangle < tmax : tangle > tmax;

         if(!result)
             angle = max;
         return angle;
     }
}

public enum Direction
{
    Left, Right
}
