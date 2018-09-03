using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    //Page 
    public bool ShowPages = false;

    public int page = 0;
    public GameObject pagePrefab;

    public int pageCount;
    public int levelCount;

    int levelsPerPage = 5;

    public LinkedList<Page> pages;

    public RectTransform LeftButton, RightButton, PlayButton, CreditsButton;

    //Splash Screen
    public Image splashScreen;
    public float splashTimer = 2f;

    private void Start()
    {
        GenerateLevelIcons();

        //If the player preference ShowSplashScreen is set to 0 then set it to 1 and disable splash screen
        if(PlayerPrefs.HasKey("ShowSplashScreen") && PlayerPrefs.GetInt("ShowSplashScreen") == 0)
        {
            splashScreen.color = new Color(1,1,1,0);
            splashTimer = 6.0f;
            PlayerPrefs.SetInt("ShowSplashScreen", 1);
        }
        else
        {

        }
    }

    //Instantiate Level Select
    void GenerateLevelIcons()
    {
        pages = new LinkedList<Page>();

        levelCount = SceneManager.sceneCountInBuildSettings - 2; //minus one for the main menu and one for bootstrap
        
        pageCount = Mathf.CeilToInt((float)levelCount / (float)levelsPerPage);
        
        //Create Pages, these pages store a total of ten levels each, the levels are placed  in two rows with five on each row
        for (int i = 0; i < pageCount; i++)
        {
            pages.AddLast(Instantiate(pagePrefab, transform).GetComponent<Page>());
            pages.Last.Value.pageId = i;
            pages.Last.Value.GetComponent<RectTransform>().anchoredPosition +=  new Vector2(2000f, 0f);
        }
        
    }

    private void Update()
    {
        if(splashScreen)
        {
            splashTimer += Time.deltaTime;
            
            if(splashTimer > 6f)
            {
                splashScreen.color = Color.Lerp(splashScreen.color, new Color(1,1,1,0f), 0.08f);
                splashScreen.raycastTarget = false;
            }
            else
            {
                splashScreen.raycastTarget = true;
            }
        }

        if(ShowPages)
        {
            LeftButton.anchoredPosition = Vector2.Lerp(LeftButton.anchoredPosition, new Vector2(30f, -20f), 0.1f);
            RightButton.anchoredPosition = Vector2.Lerp(RightButton.anchoredPosition, new Vector2(30f, -20f), 0.1f);

            if(page == -pageCount)
            {
                RightButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1f);
            }
            else
            {
                RightButton.GetComponent<Image>().color = Color.white;
            }

            //Remove play button from view
            PlayButton.anchoredPosition = Vector2.Lerp(PlayButton.anchoredPosition, new Vector2(-1000, -112.9f), 0.1f);
            CreditsButton.anchoredPosition = Vector2.Lerp(CreditsButton.anchoredPosition, new Vector2(-1000, -112.9f), 0.1f);

        }
        else
        {
            LeftButton.anchoredPosition = Vector2.Lerp(LeftButton.anchoredPosition, new Vector2(-85, -550f), 0.1f);
            RightButton.anchoredPosition = Vector2.Lerp(RightButton.anchoredPosition, new Vector2(85, -550f), 0.1f);

            //Move Play Button into view
            PlayButton.anchoredPosition = Vector2.Lerp(PlayButton.anchoredPosition, new Vector2(0, 0f), 0.1f);
            CreditsButton.anchoredPosition = Vector2.Lerp(CreditsButton.anchoredPosition, new Vector2(0f, 0f), 0.1f);

        }
    }

    public void showPages()
    {
        ShowPages = true;
    }

    public void ChangePage(int pageChange)
    {
        page += pageChange;

        if(page > -1) { page = 0; ShowPages = false; }
        if(page < -pageCount) { page = -pageCount; }
    }

    public void ResetSplashScreen()
    {
        splashTimer = 0f;
        splashScreen.color = Color.white;
    }
}
