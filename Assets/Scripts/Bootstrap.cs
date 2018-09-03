using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour 
{
    float timer = 0.5f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
