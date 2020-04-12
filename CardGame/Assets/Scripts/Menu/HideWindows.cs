using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideWindows : MonoBehaviour, IPointerClickHandler
{
    public GameObject puzzleLevels;     // Gets puzzle levels window
    public GameObject options;          // Gets options window

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        puzzleLevels.SetActive(false);
        options.SetActive(false);
    }

}
