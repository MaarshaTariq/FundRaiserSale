using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool flagHit;
    HighlightText highlightText;
    public static DragAndDrop dg;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 position;
    //public GameObject ObjectTogetHit;
    //public GameObject imageToBeDragged;
    public Vector3 initialTransform;

    private Vector2 lastMousePosition;

    public void Start()
    {
        dg = this;
        highlightText = new HighlightText();
        rectTransform = GetComponent<RectTransform>();
        initialTransform = transform.position;
        GetComponent<Button>().onClick.AddListener(OnOptionClick);
    }

    public void Awake()
    {

        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        //Debug.Log("OnBeginDrag" + canvasGroup.blocksRaycasts);
        canvasGroup.alpha = 1f;
        position = new Vector3 (rectTransform.anchoredPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }



    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition += eventData.delta;

        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
    }
    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
       // Debug.Log("Final Transform" + initialTransform);
       // Debug.Log("flagHit " + flagHit);
        if (flagHit)
        {
       
            
        }
        canvasGroup.alpha = 1f;
        rectTransform.localPosition = position;


        //If Conditions meet

    }
    

    public void OnOptionClick()
    {

#if UNITY_EDITOR
        GetComponentInParent<AccessibilityGameplay>().panelTriger.SelectionLogic(GetComponent<Collider2D>());
#endif
        StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection(GetComponent<Collider2D>().tag)));
    }
   
    

}

