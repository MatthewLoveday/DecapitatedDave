using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelIcon : MonoBehaviour 
{
    public bool MouseOver;
    public int levelID;

    public Image img;
    public TextMeshProUGUI text; 

    bool valid = false;

    private void Start()
    {
        if(!img)
        {
            img = GetComponent<Image>();
        }
        if(!text)
        {
            text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        text.text = (levelID-1).ToString();

        //Can this be pressed?
        if(levelID == 2)
        {
            valid = true;
        }
        else
        {
            Debug.Log(levelID + ": " + PlayerPrefs.GetInt("Level " + levelID));
            if(PlayerPrefs.GetInt("Level " + levelID.ToString()) == 1)
            {
                Debug.Log("Set " + levelID + " valid");
                valid = true;
            }
            else
            {
                valid = false;
            }
        }

        img.color = valid ? Color.white : new Color(.3f,.3f,.3f,1f);

    }

    private void Update()
    {
        if(MouseOver && Input.GetMouseButtonDown(0) && valid)
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelID);
    }

    public void MouseEnter()
    {
        MouseOver = true;
        img.color = valid ? new Color(.7f,.7f,.7f,1f) : new Color(.3f,.3f,.3f,1f);;
    }

    public void MouseExit()
    {
        MouseOver = false;
        img.color = valid ? Color.white : new Color(.3f,.3f,.3f,1f);
    }

}
