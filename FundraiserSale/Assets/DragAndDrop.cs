using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 position;
    public GameObject imageToBeDragged;
    public Transform initialTransform;
    public void Awake()
    {

        

        rectTransform = GetComponent<RectTransform>();
        initialTransform = rectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //startingValue = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition += eventData.delta;
        position = rectTransform.position;
        
        //rectTransform.anchoredPosition = startingValue;

        /*if (position.x > 890 && position.x < 1735 )
        {
            imageToBeDragged.SetActive(true);
            rectTransform.anchoredPosition = startingValue;
        }
        else
        {
            rectTransform.anchoredPosition = startingValue;
        }
       */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Final Transform" + initialTransform.localPosition);
        rectTransform.anchoredPosition = initialTransform.localPosition;
        //If Conditions meet
        imageToBeDragged.SetActive(true);
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}

/*StartCoroutine(waitingTime());
imageToBeDragged.SetActive(true);
        StartCoroutine(waitingTime());
rectTransform.anchoredPosition = startingValue;
        StartCoroutine(waitingTime());*/

  //  && position.y > 540 && position.y< 7