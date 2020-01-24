using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell card")]
public class Spell : ScriptableObject
{
    public string cardName;
    public string description;
    public int cost;
    public Sprite image;
    public string type;
}
