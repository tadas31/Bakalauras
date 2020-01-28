using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInHand : MonoBehaviour, IPointerClickHandler   //interface for OnPointerClick
     , IDragHandler                                             //interface for OnDrag
     , IBeginDragHandler                                        //interface for OnBeginDrag
     , IEndDragHandler                                          //interface for OnEndDrag
     , IPointerEnterHandler                                     //interface for OnPointerEnter
     , IPointerExitHandler                                      //interface for OnPointerExit
{

    static float CARD_ENTER_OFFSET = 50f;
    private Vector3 lastPosition;
    private bool setLastPos = false;
    private bool isDraged = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y + CARD_ENTER_OFFSET);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraged = true;
        setLastPosition(this.transform.localPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPosition = Vector2.zero;
        //transforms the position of the current pointer to world pointer
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPosition);
        //set the position
        this.transform.position = this.transform.TransformPoint(localPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraged = false;
        this.transform.localPosition = getLastPosition();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDraged)
        {
            setLastPosition(this.transform.localPosition);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y + CARD_ENTER_OFFSET);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDraged)
        {
            this.transform.localPosition = getLastPosition();
        }
    }


    private void setLastPosition(Vector3 pos)
    {
        if (!setLastPos)
        {
            lastPosition = pos;
            setLastPos = true;
        }
    }

    private Vector3 getLastPosition()
    {
        if (setLastPos)
        {
            setLastPos = false;
        }
        return lastPosition;
    }
}
