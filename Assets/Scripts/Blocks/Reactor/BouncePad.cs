using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {
    [Header("References")]
    [SerializeField]
    BoxCollider2D boxCollider;

    [SerializeField]
    public AudioSource source;
    [SerializeField]
    public AudioClip clip;

    [SerializeField]
    public Animator animator;

    [Header("Properties")]
    [SerializeField]
    float bounciness = 1.2f;

    

    private void Start()
    {
        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if(!source)
        {
            source = GetComponent<AudioSource>();
        }

    }

    private void Update()
    {
        if(boxCollider.sharedMaterial.bounciness != bounciness)
        {
            boxCollider.sharedMaterial.bounciness = bounciness;
            RefreshBody();
        }
    }

    void RefreshBody()
    {
        boxCollider.enabled = false;
        boxCollider.enabled = true;
    }

}
