using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabControler : MonoBehaviour
{
    public List<TabButton> tabButtons;  // List off all tabs.

    // Makes all tabs look like they are not clicked.
    public void undoClick()
    {
        foreach (var tab in tabButtons)
        {
            tab.isClicked = false;
            tab.GetComponent<Image>().color = Color.gray;
        }
            
    }
}
