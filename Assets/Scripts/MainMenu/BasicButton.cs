using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicButton : MonoBehaviour {

    public bool mouseOver;
    public Image img;

    private void Start()
    {
        if(!img) { img = GetComponent<Image>(); }
    }

    public void MouseOver()
    {
        mouseOver = true;
        GetComponent<Image>().color = new Color(.8f,.8f,.8f,1f);
        Debug.Log("Color Change");
    }

    public void MouseExit()
    {
        mouseOver = false;
        img.color = Color.white;
    }
}
