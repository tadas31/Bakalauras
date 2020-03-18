using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWinCondition : MonoBehaviour
{

    public GameObject winCondition;
    public TextMeshProUGUI winConditionText;

    private IWinCondition iWinCondition;

    // Start is called before the first frame update
    void Start()
    {
        iWinCondition = GetComponent<IWinCondition>();
        winConditionText.text = iWinCondition.winCondition();
        StartCoroutine("displayWinCondition");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator displayWinCondition()
    {
        winCondition.SetActive(true);
        yield return new WaitForSeconds(3f);
        winCondition.SetActive(false);
    }
}
