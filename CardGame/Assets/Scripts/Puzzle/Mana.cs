using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mana : MonoBehaviour
{

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
