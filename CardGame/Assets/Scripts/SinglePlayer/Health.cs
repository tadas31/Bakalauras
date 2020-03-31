using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{

    public int health = 30;

    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayHealth()
    {
        healthText.text = "Health - " + health;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
