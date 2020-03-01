using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattlecryDamageSingleTarget : MonoBehaviour
{

    private AttackHelper attackHelper;      // Reference to attack helper.

    private int damage;                     // Amount of damage to deal.
    private bool attacking;                 // Prevents battlecry from activating multiple times.

    private Transform defendingCard;        // Defending card.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        damage = 1;
        defendingCard = null;

        // Enables arrow drawing and sets coordinates.
        float xPos = transform.position.x < 0 ? transform.position.x / 1.14f : transform.position.x / 1.1f;
        float yPos = transform.position.y < 0 ? -0.6f : 3.45f;
        attackHelper.arrowOrigin = new Vector3(xPos, yPos, 6);
        attackHelper.arrowTarget = new Vector3(xPos, yPos, 6);
        attackHelper.cachedLineRenderer.enabled = true;

        // Prevents another card to be selected to attack
        attackHelper.isAttacking = true;
       
        attacking = false;
    }

    private void OnDisable()
    {
        attackHelper.cachedLineRenderer.enabled = false;
        attackHelper.isAttacking = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; // Distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        attackHelper.arrowTarget = new Vector3(mousePos.x, mousePos.y, 6);

        defendingCard = attackHelper.getDefendingCard(transform.GetChild(0));

        if (Input.GetMouseButtonUp(0) && !attacking && defendingCard != null)
        {
            StartCoroutine("battlecry");
            attacking = true;
        }
           
    }

    private IEnumerator battlecry()
    {
        attackHelper.cachedLineRenderer.enabled = false;

        CardStatsHelper defendingCardStats = defendingCard.GetComponentInParent<CardStatsHelper>();
        defendingCardStats.takeDamage(damage);

        // Displays damage dealt to defending card
        TextMeshProUGUI defendingCardDamageTaken = defendingCard.transform.Find("Image").transform.Find("DamageTaken").GetComponent<TextMeshProUGUI>();
        defendingCardDamageTaken.text = "-" + damage;
        yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
        defendingCardDamageTaken.text = null;

        // Destroys card if its dead
        defendingCardStats.checkIfSitllAlive();


        GetComponent<BattlecryDamageSingleTarget>().enabled = false;

    }
}
