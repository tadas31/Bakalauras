using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGraveyard : MonoBehaviour
{
    // Closes graveyard
    public void onCLosePress()
    {
        gameObject.SetActive(false);
    }
}
