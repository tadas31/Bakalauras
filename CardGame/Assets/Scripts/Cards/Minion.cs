using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Minion", menuName = "Minion card")]
public class Minion : ScriptableObject
{
    public string cardName;     //card's name
    public string description;  //card's description
    public int cost;            //card's cost
    public Sprite image;        //card's image
    public string type;         //card's type
    public int attack;          //minion's attack
    public int life;            //minion's life
}    