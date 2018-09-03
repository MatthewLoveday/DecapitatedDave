using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Danger : ReactorBase 
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsEnabled)
        {
            if(collision.tag == "Player")
            {
                StartCoroutine(RestartScene());
            }
        }
    }
    

    IEnumerator RestartScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
