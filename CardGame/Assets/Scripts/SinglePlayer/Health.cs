using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{

    public int health = 30;

    public TextMeshProUGUI healthText;

    public void displayHealth()
    {
        healthText.text = "Health - " + health;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
