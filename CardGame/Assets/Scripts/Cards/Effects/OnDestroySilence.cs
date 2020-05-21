using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class OnDestroySilence : MonoBehaviour, IDescription
{
    private AttackHelper attackHelper;      // Reference to attack helper script.

    // Start is called before the first frame update
    void Start()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
    }

    private void OnDestroy()
    {
        if (transform.parent != null && transform.parent.name.ToLower().Contains("board"))
        {
            // Gets all enemy cards.
            List<Transform> enemyCards = attackHelper.getAllEnemyCards();
            if (enemyCards.Count > 0)
            {
                // Picks random enemy card.
                Transform cardToSilence = enemyCards[Random.Range(0, enemyCards.Count)].parent;

                // Removes all effects scripts from card.
                foreach (MonoBehaviour script in cardToSilence.GetComponents<MonoBehaviour>())
                {
                    if (!script.GetType().Name.ToLower().Contains("helper") &&
                        script.GetType().Name != "Attack" &&
                        script.GetType().Name != "OnCardDestroy" &&
                        script.GetType().Name != "OnCardHoverInGame")
                    {
                        Destroy(script);
                    }
                }

                // Updates card description.
                cardToSilence.GetChild(0).Find("Description").GetComponent<TextMeshProUGUI>().text = null;
            }
        }
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "On death silence one random enemy";
        return description;
    }
}
