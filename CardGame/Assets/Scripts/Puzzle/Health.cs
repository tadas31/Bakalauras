using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public AttackHelper attackHelper;      // Reference to attack helper.

    public int health
    {
        get { return _health; }
        set { _health = value; displayHealth(); }
    }
    private int _health;

    public TextMeshProUGUI healthText;

    public void displayHealth()
    {
        healthText.text = _health.ToString();
    }

    // Takes damage.
    public void takeDamage(int damage)
    {
        _health -= damage;
        displayHealth();
        StartCoroutine( TakeDamage(damage) );
    }

    private IEnumerator TakeDamage(int damage)
    {
        TextMeshProUGUI displayDamageAmount = transform.Find("Health/ValueBackground/Damage").GetComponent<TextMeshProUGUI>();
        displayDamageAmount.gameObject.SetActive(true);
        displayDamageAmount.text = "-" + damage;
        yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
        displayDamageAmount.text = null;
        displayDamageAmount.gameObject.SetActive(false);
    }

    // Restore health.
    public void restoreHealth(int retsore)
    {
        _health += retsore;
        displayHealth();
        StartCoroutine( RestoreHealth(retsore) );
    }

    private IEnumerator RestoreHealth(int retsore)
    {
        TextMeshProUGUI displayRestoredAmount = transform.Find("Health/ValueBackground/Restore").GetComponent<TextMeshProUGUI>();
        displayRestoredAmount.gameObject.SetActive(true);
        displayRestoredAmount.text = "+" + retsore;
        yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
        displayRestoredAmount.text = null;
        displayRestoredAmount.gameObject.SetActive(false);
    }
}
