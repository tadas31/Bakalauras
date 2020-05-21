using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackForKill : MonoBehaviour, IDescription, ISpellDamage
{
    private Attack attack;                  // Reference to attack script.
    private AttackHelper attackHelper;      // Reference to attack helper script.
    private int attackToGain;               // Amount of attack card will gain on kill.
    private bool movingBack;                // Used to check when card stops moving.
    private int startCountOfEnemyCards;     // Count of enemy cards at start of the attack.
    private int endCountOfEnemyCards;       // Count of enemy cards at end of the attack.

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
        movingBack = false;
        startCountOfEnemyCards = 0;
        endCountOfEnemyCards = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets count of enemy cards
        if (attack.moveForward)
            startCountOfEnemyCards = attackHelper.getAllEnemyCards().Count;

        if (attack.moveBack)
            movingBack = true;

        if (movingBack && !attack.moveBack)
        {
            movingBack = false;
            endCountOfEnemyCards = attackHelper.getAllEnemyCards().Count;
            if (startCountOfEnemyCards - endCountOfEnemyCards > 0)
            {
                StartCoroutine(gainAttack());
            }
        }
    }

    /// <summary>
    /// Gain attack on kill
    /// </summary>
    /// <returns></returns>
    private IEnumerator gainAttack()
    {
        TextMeshProUGUI healthGained = transform.GetChild(0).Find("Image/AttackHealthRecieved").GetComponent<TextMeshProUGUI>();

        healthGained.text = "+" + attackToGain.ToString();
        CardStatsHelper cardStatsHelper = GetComponent<CardStatsHelper>();
        cardStatsHelper.setAttack(cardStatsHelper.getAttack() + attackToGain);
        yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
        healthGained.text = null;
    }

    // Returns description of effect granted by this script.
    public string getDescription()
    {
        string description = "Gain " + attackToGain + " attack after killing enemy card";
        return description;
    }

    // Sets amount of attack to gain on kill.
    public void setSpellDamage(int spellDamage)
    {
        attackToGain = spellDamage;
    }
}
