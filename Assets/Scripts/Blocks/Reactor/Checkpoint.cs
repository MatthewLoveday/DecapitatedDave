using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : ReactorBase 
{
    [Header("References")]
    [SerializeField]
    LevelManager levelManagerRef;

    [SerializeField]
    Vector2 offset;

    private void Awake()
    {
        if (!levelManagerRef)
        {
            levelManagerRef = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsEnabled)
        {
            if(collision.tag == "Player")
            {
                levelManagerRef.currentSpawnPosition = this;
            }
        }
    }

    public Vector2 GetSpawnPosition()
    {
        return transform.position + new Vector3(offset.x, offset.y);
    }
}
