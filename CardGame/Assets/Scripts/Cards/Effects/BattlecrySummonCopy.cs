using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecrySummonCopy : MonoBehaviour, IDescription
{

    private void OnEnable()
    {
        if (gameObject.transform.parent != null && gameObject.transform.parent.name == "PlayerBoard")
        {
            PuzzleGameManager.instance.PutOnTable(gameObject, true);
        }
    }

    public string getDescription()
    {
        return "Battlecry summons copy of this card";
    }
}
