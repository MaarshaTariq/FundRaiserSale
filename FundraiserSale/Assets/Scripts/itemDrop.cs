﻿using System.Collections;
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
        //Debug.Log(eventData.pointerDrag.name);
        this.transform.parent.GetComponentInChildren<TriggerChceking>().SelectionLogic(eventData.pointerPress.gameObject.GetComponent<Collider2D>());
        Debug.Log("On Drop incorrect");
    }

    
}
