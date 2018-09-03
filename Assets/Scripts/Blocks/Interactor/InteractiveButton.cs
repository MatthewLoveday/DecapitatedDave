using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour {

    [SerializeField]
    ReactorBase linkedReactor;
    [SerializeField]
    ReactorBase extraReactor;

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
    bool State = false;
    private float timer;

    [SerializeField]
    float ActiveTime = 3f;

    [SerializeField]
    Sprite enabled, disabled;

	 private void Start()
    {
        if(linkedReactor)
        {
            linkedReactor.IsEnabled = State;
        }

        if(extraReactor)
        {
            extraReactor.IsEnabled = State;
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
        if(State)
        {
            timer -= Time.deltaTime;

            if(timer < 0f)
            {
                State = false;
            }
            renderer.sprite = enabled;
        }
        else
        {
            renderer.sprite = disabled;
        }
        
        if(MouseOver && Input.GetMouseButtonDown(0))
        {
            State = true;
            timer = ActiveTime;
            source.PlayOneShot(press);
        }

        line.SetPosition(0, transform.position + new Vector3(0,0,-3f));
        line.SetPosition(1, linkedReactor.transform.position + new Vector3(0,0,-3f));

        if(extraReactor)
        {
            extraReactor.IsEnabled = State;
        }
        linkedReactor.IsEnabled = State;
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
