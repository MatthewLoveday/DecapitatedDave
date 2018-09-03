using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEffects : MonoBehaviour {

    [SerializeField]
    AudioSource source;

    [SerializeField]
    Rigidbody2D rBody;


    [SerializeField]
    AudioClip groundHit;

    [SerializeField]
    GameObject dustPrefab;

    private void Start()
    {
        if(!source)
        {
            source = GetComponents<AudioSource>()[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Play collision audio
        //only play if impact is above certain amount.
        if(rBody.velocity.magnitude > 1.2f)
        {
            //Create Dust Effect

            GameObject dust = Instantiate(dustPrefab);
            dustPrefab.transform.position = transform.position;
            dustPrefab.transform.rotation = Quaternion.Euler(0,0,0);

            StartCoroutine(DestroyDust(dust));

            float vol = source.volume;
            source.volume = Mathf.Clamp(rBody.velocity.magnitude / 80, 0, 1);

            source.PlayOneShot(groundHit);
        }
    }

    IEnumerator DestroyDust(GameObject dust)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(dust);
    }
}
