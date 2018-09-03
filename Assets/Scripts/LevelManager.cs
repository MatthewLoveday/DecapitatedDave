using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
    [Header("Prefabs")]
    [SerializeField]
    GameObject defaultUIPrefab;

    [Header("References")]
    [SerializeField]
    Canvas UI;

    [SerializeField]
    TextMeshProUGUI adviceText;

    [Header("Spawning")]
    public Checkpoint currentSpawnPosition;

    [SerializeField]
    GameObject head;

    //Public Shared Data
    [SerializeField]
    public bool HoldingObject;
    public GameObject HeldObject;

    public GameObject HeadPREFAB;
    //Internal Tracker Variables
    int deaths;
    Vector2 previousPlayerPosition;

    float inactiveTimer = 0f;

    public void SetLevelComplete()
    {
        Debug.Log("Set Level " + SceneManager.GetActiveScene().buildIndex + " Complete");
        PlayerPrefs.SetInt("Level " + SceneManager.GetActiveScene().buildIndex, 1);
    }
    
    private void Awake()
    {
        if(!UI)
        {
            if (GameObject.FindGameObjectWithTag("DefaultUI"))
            {
                UI = GameObject.FindGameObjectWithTag("DefaultUI").GetComponent<Canvas>();
            }
            else
            {
                UI = Instantiate(defaultUIPrefab).GetComponent<Canvas>();
            }
        }

        if(!adviceText)
        {
            adviceText = UI.transform.GetComponentInChildren<TextMeshProUGUI>();
        }


        if(!currentSpawnPosition)
        {
            GameObject spawnpoint = GameObject.FindGameObjectWithTag("Spawn");
            
            if(spawnpoint)
            {
                currentSpawnPosition = spawnpoint.GetComponent<Checkpoint>();
                //If no head exists then create
                
                if(GameObject.FindGameObjectWithTag("Player") == null)
                {
                    head = Instantiate(HeadPREFAB);
                    head.transform.position = currentSpawnPosition.GetSpawnPosition();
                }
            }
            else
            {
                GameObject spawn = Instantiate(new GameObject("Spawnpoint"), transform, true);
                spawn.tag = "Spawn";
                currentSpawnPosition = spawn.AddComponent<Checkpoint>();
                //If no head exists then create
                if(GameObject.FindGameObjectWithTag("Player") == null)
                {
                    head = Instantiate(HeadPREFAB);
                }

                currentSpawnPosition.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
        }
        
        if(!head)
        {
            Debug.Log("Found Head");
            head = GameObject.FindGameObjectWithTag("Player");
            head.transform.position = currentSpawnPosition.GetSpawnPosition();
        }


    }

    private void Update()
    {
        
        Color textColor = Color.white;
        //If player has moved for over 8 seconds
        if(inactiveTimer > 14f)
        {
            //Player has died a lot on this checkpoint.
            adviceText.text = "Restart Level [R]";
            
    
            //Fade text in
            textColor.a = 1;
        }
        else
        {
            //Fade text out
            textColor.a = 0;
        }

        adviceText.color = Color.Lerp(adviceText.color, textColor, 0.08f);

        if(Vector3.Distance(Vector3.zero, head.transform.position) < 1000f)
        {
            if(Vector2.Distance(previousPlayerPosition, new Vector2(head.transform.position.x, head.transform.position.y)) > 0.05f)
            {
                inactiveTimer = 0.0f;
            }
            else
            {
                inactiveTimer += Time.deltaTime;
            }
        }
        else if(Vector2.Distance(previousPlayerPosition, new Vector2(head.transform.position.x, head.transform.position.y)) > 0.05f) //Moving and outside Level
        {
            inactiveTimer += Time.deltaTime * 4;
        }

        previousPlayerPosition = head.transform.position;

        if(Input.GetKeyDown(KeyCode.R))
        {
             StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }

    public IEnumerator LoadScene(int buildIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
