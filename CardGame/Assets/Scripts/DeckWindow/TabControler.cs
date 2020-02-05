using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabControler : MonoBehaviour
{
    public List<TabButton> tabButtons;  // List off all tabs.

    // Start is called before the first frame update
    void Start()
    {
        // Makes default tab look like it's clicked.
        foreach (var tab in tabButtons)
        {
            if (tab.transform.name == "All")
            {
                tab.GetComponent<Image>().color = tab.clickedColor;
                tab.isClicked = true;
            }
            else
            {
                tab.isClicked = false;
            }
                
        }
    }

    // Makes all tabs look like they are not clicked.
    public void undoClick()
    {
        foreach (var tab in tabButtons)
        {
            tab.isClicked = false;
            tab.GetComponent<Image>().color = tab.notClickedCollor;
        }
            
    }
}
