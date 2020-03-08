using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDescriptionHelper : MonoBehaviour
{
    public List<string> startingScripts;        // Holds all starting scripts of card

    private List<string> scripts;               // Current scripts

    private TextMeshProUGUI descriptionText;    // Game object to display description

    // Start is called before the first frame update
    void Start()
    {
        descriptionText = transform.GetChild(0).Find("Description").GetComponent<TextMeshProUGUI>();
        scripts = startingScripts;

        // Get all needed descriptions and display them
        updateDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Updates description of card
    public void updateDescription()
    {
        string newDescription = "";
        foreach (string s in scripts)
        {
            System.Type scriptType = System.Type.GetType(s + ",Assembly-CSharp");
            if (scriptType != null)
            {
                var iDescription = GetComponent(scriptType) as IDescription;
                if (iDescription != null)
                {
                    string description = iDescription.getDescription();
                    newDescription += description + "\n";
                }
                else
                {
                    continue;
                }  
            }
            else
            {
                continue;
            }
        }

        descriptionText.text = newDescription;
    }

    // Add new script for card description.
    public void addScript(string name)
    {
        scripts.Add(name);
        updateDescription();
    }

    // Remove script from card description.
    public void removeScript(string name)
    {
        scripts.Remove(name);
        updateDescription();
    }

    // Gets all card description scripts.
    public List<string> getScripts()
    {
        return scripts;
    }
}
