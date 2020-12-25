using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemDrop : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name);
        this.transform.parent.GetComponentInChildren<TriggerChecking>().SelectionLogic(eventData.pointerPress.gameObject.GetComponent<Collider2D>());
        //Debug.Log("On Drop incorrect");
    }

    
}
