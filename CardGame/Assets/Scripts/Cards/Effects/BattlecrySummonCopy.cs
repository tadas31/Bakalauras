using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecrySummonCopy : MonoBehaviour, IDescription
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (gameObject.transform.parent != null && gameObject.transform.parent.name == "PlayerBoard")
        {
            PuzzleGameManager.instance.PutOnTable(gameObject, true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getDescription()
    {
        return "Battlecry summons copy of this card";
    }
}
