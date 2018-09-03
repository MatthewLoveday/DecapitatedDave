using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour 
{
    [SerializeField]
    ReactorBase linkedReactor;

    [SerializeField]
    SpriteRenderer renderer;

    [SerializeField]
    LineRenderer line;

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip press;

    bool MouseOver = false;

    [SerializeField]
    bool State;

    [SerializeField]
    Sprite enabled, disabled;


    private void Start()
    {
        if(linkedReactor)
        {
            linkedReactor.IsEnabled = State;
        }

        if(!renderer)
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        if(!line)
        {
            line = transform.GetChild(0).GetComponent<LineRenderer>();
        }

        if(!source)
        {
            source = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if(linkedReactor)
        {
            if(MouseOver)
            {
                //If hover then draw line from lever to affected sprite.
                line.SetPosition(0, transform.position  + new Vector3(0,0,-3f));
                line.SetPosition(1, linkedReactor.transform.position + new Vector3(0,0,-3f));
                
                //CHange State!
                if(Input.GetMouseButtonDown(0))
                {
                    State = !State;
                    linkedReactor.IsEnabled = State;
                    source.PlayOneShot(press);

                    if(State)
                    {
                        renderer.sprite = enabled;
                    }
                    else
                    {
                        renderer.sprite = disabled;
                    }
                }
            }
        }

        line.enabled = MouseOver;
    }

    private void OnMouseOver()
    {
        MouseOver = true;
    }

    private void OnMouseExit()
    {
        MouseOver = false;
    }
}
