using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Selected card class that shows selected information about the card.
/// </summary>
public class SelectedCard : MonoBehaviour
{
    //Instance of the selected card element.
    public static SelectedCard Instance { get; private set; }
    //The selected card game object.
    private GameObject _selected;
    public GameObject Selected {
        //It can be get only in this class.
        private get { return _selected; }
        //When the selected card is set.
        set 
        {
            //Destroy the previous card.
            Destroy(_selected);
            //Create new card.
            _selected = Instantiate(value, this.transform);
            //Remove the CardInHand script.
            Destroy(_selected.GetComponent<CardInHand>());
            //Set the position to the local position.
            _selected.transform.localPosition = new Vector3();
        } 
    }

    private void Awake()
    {
        //If the instance is not created create it.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destroy the new game object that is created
        else 
        {
            Destroy(gameObject);
        }
    }

}
