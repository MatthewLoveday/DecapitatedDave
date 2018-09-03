using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadAnimationTrigger : MonoBehaviour {

    [SerializeField]
    BouncePad pad;


	// Use this for initialization
	void Start () {
	    if(!pad)
        {
            pad = transform.parent.GetComponent<BouncePad>();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Entity")
        {
            pad.source.PlayOneShot(pad.clip);
            pad.animator.SetTrigger("Bounce");
        }
    }
}
