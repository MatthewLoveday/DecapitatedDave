using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour 
{
    LevelManager levelManager;

    [SerializeField]
    bool proceedToNextLevel = true;
    
    [SerializeField]
    int levelID;


    private void Start()
    {
        if(!levelManager)
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }

        levelID = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("End Game");
            levelManager.SetLevelComplete();
             GetComponent<AudioSource>().Play();
            
            //Is there a scene after this?
            if(levelID < SceneManager.sceneCountInBuildSettings - 2)
            {
                //Load next scene
                StartCoroutine(playLevel(levelID + 1));
            }
            else
            {
                //Load Main Menu
                PlayerPrefs.SetInt("ShowSplashScreen", 0);
                StartCoroutine(playLevel(1));
            }
        }
    }

    IEnumerator playLevel(int id)
    {
        yield return new WaitForSeconds(1f);
        loadNext(id);
    }

    void loadNext(int id)
    {
        StartCoroutine(levelManager.LoadScene(id));
    }

}
