using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mana : MonoBehaviour
{
    private AttackHelper attackHelper;      // Reference to attack helper script.

    public int currentMana
    {
        get { return _currentMana; }
        set { _currentMana = value; }
    }
    private int _currentMana;

    public int maxMana
    {
        get { return _maxMana; }
        set { _maxMana = value; }
    }
    private int _maxMana;

    public TextMeshProUGUI manaText;

    // Start is called before the first frame update
    void Start()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
    }

    public void displayMana()
    {
        manaText.text = _currentMana + "/" + _maxMana;
    }

    public void useMana(int usedMana)
    {
        _currentMana -= usedMana;
        displayMana();
    }

    public bool canUseCard(int manaCost)
    {
        if (_currentMana >= manaCost)
        {
            useMana(manaCost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseMana(int mana)
    {
        StartCoroutine(increaseMana(mana));
    }

    private IEnumerator increaseMana(int mana)
    {
        TextMeshProUGUI displayRestoredAmount = transform.Find("Mana/ValueBackground/Restore").GetComponent<TextMeshProUGUI>();
        displayRestoredAmount.gameObject.SetActive(true);
        displayRestoredAmount.text = "+" + mana;
        yield return new WaitForSeconds(attackHelper.TIME_TO_SHOW_DAMAGE_FROM_SPELLS);
        currentMana += mana;
        displayMana();
        displayRestoredAmount.text = null;
        displayRestoredAmount.gameObject.SetActive(false);
        attackHelper.isAttacking = false;
    }


}
