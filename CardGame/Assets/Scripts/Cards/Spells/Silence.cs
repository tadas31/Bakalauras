using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Silence : MonoBehaviour, IDescription
{

    private AttackHelper attackHelper;      // Reference to attack helper script.
    private bool silencing;                 // If player pressed on card to attack true else false.


    // Start is called before the first frame update
    void Start()
    {
        silencing = false;
    }

    // Executes when script is enabled
    public void OnEnable()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        // Enables arrow drawing and sets coordinates.
        attackHelper.arrowOrigin = new Vector3(0, -4, 6);
        attackHelper.arrowTarget = new Vector3(0, 0, 6);
        attackHelper.cachedLineRenderer.enabled = true;

        // Prevents defending card from being selected for attacking.
        attackHelper.isAttacking = true;
    }

    // Executes when script is disabled
    public void OnDisable()
    {
        attackHelper.cachedLineRenderer.enabled = false;
        attackHelper.isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; // Distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        attackHelper.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);

        if (!silencing)
        {
            Transform defendingCard = attackHelper.getDefendingCard(null);

            // If player hovers over cards in hand spell returns to hand
            if (defendingCard != null && defendingCard.position == new Vector3(2000, 2000, 2000))
            {
                attackHelper.moveCardBackToHand(gameObject);
                silencing = false;
                attackHelper.isAttacking = false;
                Destroy(defendingCard.gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                silencing = true;
                if (defendingCard != null && defendingCard.position != new Vector3(2000, 2000, 2000))
                    castSpell(defendingCard);
                else
                {
                    attackHelper.moveCardBackToHand(gameObject);
                    silencing = false;
                    attackHelper.isAttacking = false;
                }
            }
        }
    }

    /// <summary>
    /// Silences selected card.
    /// </summary>
    /// <param name="defendingCard"></param>
    private void castSpell(Transform defendingCard)
    {
        attackHelper.cachedLineRenderer.enabled = false;
        
        // Removes all effects scripts from card.
        foreach (MonoBehaviour script in defendingCard.parent.GetComponents<MonoBehaviour>())
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
        defendingCard.Find("Description").GetComponent<TextMeshProUGUI>().text = null;

        attackHelper.isAttacking = false;
        Destroy(gameObject);
    }

    // Returns description of effect granted by this script
    public string getDescription()
    {
        string description = "Silence";
        return description;
    }
}
