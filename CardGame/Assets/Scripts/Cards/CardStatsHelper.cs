using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardStatsHelper : MonoBehaviour
{
    public int startingAttack;          // Starting attack of card in case attack needs to be reset.
    public int startingLife;            // Starting life of card in case life needs to be reset.
    
    private int attack;                 // Current attack of card.
    private int life;                   // Current life of card.

    private TextMeshProUGUI statsText;  // Game object to display card stats.

    private void OnEnable()
    {
        attack = startingAttack;
        life = startingLife;

        statsText = transform.GetChild(0).Find("Stats").GetComponent<TextMeshProUGUI>();

        changeText();
    }

    // Changes stats text for card.
    private void changeText()
    {
        statsText.text = attack + " / " + life;
    }

    // Changes cards life.
    public void takeDamage(int damage)
    {
        life -= damage;
        changeText();
    }

    // Sets life for card.
    public void setLife(int life)
    {
        this.life = life;
        changeText();
    }

    // Gets card life.
    public int getLife()
    {
        return life;
    }

    // Sets attack for card.
    public void setAttack(int attack)
    {
        this.attack = attack;
        changeText();
    }

    // Gets card attack.
    public int getAttack()
    {
        return attack;
    }

    // Checks if minions life is less than one 
    // If it is minion gets destroyed.
    public void checkIfSitllAlive()
    {
        if (life < 1)
        {
            Destroy(gameObject);
        }
    }
}
