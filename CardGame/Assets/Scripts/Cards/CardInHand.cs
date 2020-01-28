using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInHand : MonoBehaviour, IPointerClickHandler // 2
     , IDragHandler
     , IEndDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
{

    static float CARD_ENTER_OFFSET = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y + CARD_ENTER_OFFSET);
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
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y + CARD_ENTER_OFFSET);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - CARD_ENTER_OFFSET);
    }

}
