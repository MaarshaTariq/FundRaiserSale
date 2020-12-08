using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemDrop : MonoBehaviour, IDropHandler
{
    public GameObject checkmark;
    public GameObject scoreBoard;

    public void OnDrop(PointerEventData eventData)
    {
       if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<Transform>().position;
            checkmark.SetActive(true);
            scoreBoard.SetActive(true);
        }  
            
                }

    
}
