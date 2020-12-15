using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool flagHit;
    HighlightText highlightText;
    public static DragAndDrop dg;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 position;
    public GameObject ObjectTogetHit;
    //public GameObject imageToBeDragged;
    public Vector3 initialTransform;
    public void Start()
    {
        dg = this;
        highlightText = new HighlightText();
        rectTransform = GetComponent<RectTransform>();
        initialTransform = transform.position;
        positionOftransform();
    }

    public void Awake()
    {

        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        Debug.Log("OnBeginDrag" + canvasGroup.blocksRaycasts);
        canvasGroup.alpha = 1f;
        position = new Vector3 (rectTransform.anchoredPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }



    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //startingValue = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition += eventData.delta;
        positionOftransform();

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
        canvasGroup.blocksRaycasts = true;
        Debug.Log("Final Transform" + initialTransform);
        Debug.Log("flagHit " + flagHit);
        if (flagHit)
        {
           // highlightText.fillAmountForImage -= 400;
            //highlightText.increaseFillAmount(eventData.pointerDrag.GetComponent<Image>());
            // imageToBeDragged.SetActive(true);
            // imageToBeDragged.layer = 3;
            Debug.Log("OnEndDrag");

            
        }
        canvasGroup.alpha = 1f;
        rectTransform.localPosition = position;


        //If Conditions meet

    }
    public void positionOftransform(){
      // position = rectTransform.position;
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