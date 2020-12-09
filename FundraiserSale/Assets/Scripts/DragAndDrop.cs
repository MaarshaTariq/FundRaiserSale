using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool flagHit;
    public static DragAndDrop instance;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 position;
    public GameObject ObjectTogetHit;
    //public GameObject imageToBeDragged;
    public Vector3 initialTransform;
    public void Start()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        initialTransform = transform.position;
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
        canvasGroup.blocksRaycasts = true;
        Debug.Log("Final Transform" + initialTransform);
        Debug.Log("flagHit " + flagHit);
        if (flagHit)
        {
           // imageToBeDragged.SetActive(true);
           // imageToBeDragged.layer = 3;
            Debug.Log("OnEndDrag");

            
        }
        canvasGroup.alpha = 1f;
        transform.position = initialTransform;


        //If Conditions meet

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