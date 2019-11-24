using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCards : MonoBehaviour
{

    AllCards cards;
    // Start is called before the first frame update
    void Start()
    {
        cards = new AllCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Card c = cards.getCard("minionCard");
            Debug.Log(string.Format(" title - {0}\n cost - {1}\n type - {2}\n description - {3}\n image - {4}\n attack - {5}\n life - {6}\n", 
                c.title, c.cost, c.type, c.description, c.image, c.attack, c.life));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Card c = cards.getCard("spellCard");
            Debug.Log(string.Format(" title - {0}\n cost - {1}\n type - {2}\n description - {3}\n image - {4}\n attack - {5}\n life - {6}\n",
                c.title, c.cost, c.type, c.description, c.image, c.attack, c.life));
        }
    }
}
