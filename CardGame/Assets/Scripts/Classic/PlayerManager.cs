using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int life 
    { 
        get { return this.GetComponent<Health>().health; } 
        set 
        {
            this.GetComponent<Health>().health = value;
        } 
    }
    public int mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            SetManaToUI();
        }
    }
    private int _mana;
    public int maxMana
    {
        get { return _maxMana; }
        set
        {
            _maxMana = value;
            SetManaToUI();
        }
    }
    private int _maxMana;
    public bool isTurn
    {
        get { return _isTurn; }
        set
        {
            _isTurn = value;
            UIManager.instance.endTurn.SetActive(_isTurn);
        }
    }
    private bool _isTurn;

    private void SetManaToUI()
    {
        transform.Find("Mana/ValueBackground/Value").GetComponent<TextMeshProUGUI>().text = _mana + "/" + _maxMana;
    }
}
