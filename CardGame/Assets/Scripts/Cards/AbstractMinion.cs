using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMinion : AbstractCard
{
    public int attack { get; set; }     //minion's attack
    public int life { get; set; }       //minion's life

    public Sprite MINION_BACKGROUND;/* = Resources.Load<Sprite>("Cards/Backgrounds/Minion");*/
}
