using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : ReactorBase {
    


    [Header("Force Properties")]
    [Tooltip("The range at which the Attractor / Repulsor works within, anything outside of this range will not be effective")]
    [SerializeField]
    public float radius = 5f;

    [Tooltip("How much force is applied per frame in order to Attract / Repulse an object towards / away from it")]
    [SerializeField]
    float force = 10f;

    [Tooltip("Should this affector attract or repulse the player")]
    public ForceType forceType = ForceType.Attract;
    
    [Header("Graphical Effect")]
    [SerializeField]
    LineRenderer lRenderer;

    [SerializeField]
    Gradient attractGradient, repulseGradient;
    
    [SerializeField]
    GameObject animObject;
    [SerializeField]
    bool rotateAnim = true;
    
    [SerializeField]
    AudioSource source;

    //Internal Logic Variables
    private Collider2D[] colliders;
    private bool touchedPlayer = false;

    private void Start()
    {
        if(lRenderer == null)
        {
            lRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
        }

        if(animObject == null)
        {
            animObject = transform.GetChild(1).gameObject;
        }

        if(!source)
        {
            source = GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update () {
	
        if(IsEnabled)
        {
            colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius); 
        
            touchedPlayer = false;
           
            foreach(Collider2D col in colliders)
            {
                if(col.gameObject.tag == "Player")
                {
                    //Pull player towards me
                    Attract(col);

                    touchedPlayer = true;
                
                    lRenderer.SetPosition(0, transform.position);
                    lRenderer.SetPosition(1, col.transform.position);
                    animObject.SetActive(true);
                }
            }

            lRenderer.enabled = touchedPlayer;
            lRenderer.colorGradient = forceType == ForceType.Attract ? attractGradient : repulseGradient;

            if (touchedPlayer)
            {
                animObject.SetActive(true);
                if(IsEnabled)
                {
                    source.enabled = true;
                }
            }
            else
            {
                animObject.SetActive(false);
                source.enabled = false;
            }

        }
        else
        {
            lRenderer.enabled = false;
            source.enabled = false;
            animObject.SetActive(false);
        }



        if(rotateAnim) { animObject.transform.Rotate(new Vector3(0f,0f,force*8 * Time.deltaTime)); }
	}

    public void Attract(Collider2D col)
    {
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(
            Vector3.Normalize(transform.position - col.transform.position) * (forceType == ForceType.Attract ? force : -force), 
            ForceMode2D.Force);
    }
}


[System.Serializable]
public enum ForceType
{
    Attract, Repulse
}