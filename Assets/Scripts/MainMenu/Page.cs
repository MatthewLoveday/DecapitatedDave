using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour 
{
    public int pageId;

    public MainMenuManager managerReference;

    public RectTransform rTransform;

    float pageWidth = 1300f;

    public LevelIcon[] icons;
    public int levelCount;
    private void Start()
    {
        managerReference = GameObject.FindGameObjectWithTag("DefaultUI").GetComponent<MainMenuManager>();

        rTransform = GetComponent<RectTransform>();
        rTransform.anchorMin = new Vector2(0, 0.15269f);
        rTransform.anchorMax = new Vector2(1, 0.7596f);

        if(icons.Length == 0)
        {
            icons = new LevelIcon[5];

            for (int i = 0; i < 5; i++)
            {
                icons[i] = transform.GetChild(i).GetComponent<LevelIcon>();
            }
        }

        levelCount = (managerReference.levelCount - (pageId * 5)) - 1;

        //Setup level icons
        for (int i = 0; i < 5; i++)
        {
            icons[i].levelID = (((pageId) * 5) + i) + 2; //add one for main menu scene
            icons[i].text.text = icons[i].levelID.ToString();

            if(i > levelCount)
            {
                icons[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        //Set position based on page viewing number and pageID

        Vector2 intendedPosition = Vector2.zero;

        intendedPosition.x = (managerReference.page+1) * pageWidth + (pageId * pageWidth);


        rTransform.anchoredPosition = Vector2.Lerp(rTransform.anchoredPosition, intendedPosition, 0.1f);

    }
}
