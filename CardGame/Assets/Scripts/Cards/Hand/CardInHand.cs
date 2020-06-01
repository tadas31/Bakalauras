using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Used for cards in hand to have functionality. Uses interfaces that implement OnPointerClick, OnDrag, OnBeginDrag, OnEndDrag, OnPointerEnter, OnPointerExit.
/// </summary>
public class CardInHand : MonoBehaviour   
     , IDragHandler 
     , IBeginDragHandler
     , IEndDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
    
{
    //Set offset for how many it should lift up when you enter with the pointer.
    private static float CARD_ENTER_OFFSET = 50f;
    //Saves the last position of the card before changing its position
    private Vector3 lastPosition;
    //Says if the last position is saved.
    private bool setLastPos = false;
    //Says if the cards is being dragged
    private bool isDragged = false;
    //Reference to attack helper.
    private AttackHelper attackHelper;
    public static int placedCard;

    public void Start()
    {
        attackHelper = GameObject.Find("Board").GetComponent<AttackHelper>();
    }

    /// <summary>
    /// If the card is beginning to be dragged this method is called.
    /// </summary>
    /// <param name="eventData">Info about the event</param>
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (!attackHelper.isAttacking)
        {
            //Sets that the card is being dragged and sets the position of the card.
            isDragged = true;
            setLastPosition(this.transform.localPosition);
        }
           
    }

    /// <summary>
    /// If the card is dragged this method is called.
    /// </summary>
    /// <param name="eventData">Info about the event</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (!attackHelper.isAttacking)
        {
            Vector2 localPosition = Vector2.zero;
            //Transforms the position of the current pointer to world pointer
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPosition);
            //Set the position of the card to the pointer
            this.transform.position = this.transform.TransformPoint(localPosition);
        }
    }

    /// <summary>
    /// If the card ends to be dragged this method is called.
    /// </summary>
    /// <param name="eventData">Info about the event</param>
    public void OnEndDrag(PointerEventData eventData)
    { 
        if (!attackHelper.isAttacking)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //If the ray cast hits a board element.
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Board")
            {

                if (GameObject.Find("ClientManager") == null)
                {
                    Mana mana = GameObject.Find("Canvas/Player").GetComponent<Mana>();
                    if ( mana.canUseCard(gameObject.GetComponent<CardCostHelper>().getCost()) )
                    {
                        PuzzleGameManager.instance.PutOnTable(gameObject);
                        Destroy(this);
                    }
                    else
                    {
                        transform.localPosition = getLastPosition();
                    }
                }
                else
                {
                    Debug.Log("Putting card on the table");
                    placedCard = transform.GetSiblingIndex();
                    if (this.gameObject.name == "Fire ball")
                    {
                        ClassicGameManager.instance.PutOnTable(this.gameObject.name, true);
                    }
                    else
                    {
                        ClientSend.PutCardOnTable(this.gameObject.name);
                    }
                    isDragged = false;
                    this.transform.localPosition = getLastPosition();
                }
            }
            else
            {
                //Sets card to last position and ends drag.
                isDragged = false;
                this.transform.localPosition = getLastPosition();
            }
        }
    }

    /// <summary>
    /// If the pointer enters the card in hand this method is called.
    /// </summary>
    /// <param name="eventData">Info about the event</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragged)
        {
            //Sets the last position of card and puts card upward.
            setLastPosition(this.transform.localPosition);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y + CARD_ENTER_OFFSET);
        }
    }

    /// <summary>
    /// If the pointer exits the card in hand this method is called.
    /// </summary>
    /// <param name="eventData">Info about the event</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragged)
        {
            //Returns to the last position.
            this.transform.localPosition = getLastPosition();
        }
    }


    /// <summary>
    /// Sets the last position of the card that it should return to if there are no prior added.
    /// </summary>
    /// <param name="pos">Position that should be added.</param>
    private void setLastPosition(Vector3 pos)
    {
        if (!setLastPos)
        {
            lastPosition = pos;
            setLastPos = true;
        }
    }

    /// <summary>
    /// Gets the last position of the card and changes that the last position could be set.
    /// </summary>
    /// <returns>The last position of the card.</returns>
    private Vector3 getLastPosition()
    {
        if (setLastPos)
        {
            setLastPos = false;
        }
        return lastPosition;
    }
    
}
