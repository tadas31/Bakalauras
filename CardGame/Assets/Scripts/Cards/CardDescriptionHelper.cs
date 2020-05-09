using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CardDescriptionHelper : MonoBehaviour
{
    private TextMeshProUGUI descriptionText;    // Game object to display description

    // Start is called before the first frame update
    public void Start()
    {
        descriptionText = transform.GetChild(0).Find("Description").GetComponent<TextMeshProUGUI>();

        // Get all needed descriptions and display them
        updateDescription();
    }


    // Updates description of card
    public void updateDescription()
    {
        string newDescription = "";
        List<IDescription> iDescription = GetComponents<IDescription>().ToList();

        foreach (IDescription description in iDescription)
            newDescription += description.getDescription() + "\n";

        descriptionText.text = newDescription;
    }
}
