using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemDrop : MonoBehaviour, IDropHandler
{
    HighlightText highlightText;
    private void Start()
    {
        highlightText = new HighlightText();
    }
    public void OnDrop(PointerEventData eventData)
    {
       if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<Transform>().position;
            
        }  
            
                }

    
}
