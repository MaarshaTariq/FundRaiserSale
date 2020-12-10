using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerChceking : MonoBehaviour {
    public GameObject transitionPanel;
    public string[] tagName ;
    public bool highlighttext = false;
    public static TriggerChceking tg;

    public Image[] ImagesToBeHighlighted;
    public float maxFillAmount = 400f;
    public static float fillAmountForImage;

    GameManager manager;
    public GameObject[] imageToBeDragged;
    public GameObject[] scoreBoardImages;
    public GameObject[] checkMarks;
    HighlightText highlightText;
    int index = 0;
    int indexForTags = 0;
    public bool checkMarksTags = false;
    public int draggedObjectsCounter;
    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tg = this;
        highlightText = new HighlightText();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        for(int j=0; j<tagName.Length; j++){
            if (other.gameObject.tag == tagName[j])

            {
                if (index < imageToBeDragged.Length)
                {

                    Debug.Log("other.gameObject.tag" + other.gameObject.tag);
                    Debug.Log("tagName" + tagName[j]);
                    //   Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

                    DragAndDrop.instance.flagHit = true;
                    if (imageToBeDragged[index].tag == tagName[j])
                    {
                        imageToBeDragged[index].SetActive(true);
                        //   scoreBoardImages[index].SetActive(true);
                        if (index > 0 && index != scoreBoardImages.Length)
                        {

                            Debug.Log(index - 1);
                            scoreBoardImages[index - 1].SetActive(false);
                            scoreBoardImages[index].SetActive(true);
                            if (index == scoreBoardImages.Length - 1)
                            {
                                checkMarks[j].SetActive(true);
                            }


                        }

                        else
                        {
                            if (index != scoreBoardImages.Length)
                                scoreBoardImages[index].SetActive(true);
                        }

                        index = index + 1;
                        if (index < imageToBeDragged.Length && imageToBeDragged[index].tag != imageToBeDragged[index - 1].tag)
                        {
                            checkMarks[j].SetActive(true);
                            highlightText.increaseFillAmount(ImagesToBeHighlighted[j+1]);

                            //  ImagesToBeHighlighted[j + 1].fillAmount = fillAmountForImage * Time.time * 0.2f;
                            highlighttext = true;
                        }

                        Debug.Log(index);
                        draggedObjectsCounter++;

                        //  Debug.Log(tagName[index]);
                        Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);


                    }


                }
                Debug.Log(("checkmark kis ka hai" + j));
                //  highlightText.abc(ImagesToBeHighlighted[j]);
            }
            
     }
       
        
    }
    public void Update()
    {
        highlightText.fillAmountForImage += 1 *0.2f;
       StartCoroutine( allCheckmarksActivation());
    }
    IEnumerator allCheckmarksActivation()
    {
      
        if (checkMarks[1].activeInHierarchy)
        {
            Debug.Log(checkMarks[1].name);
            yield return new WaitForSeconds(3f);
           // manager.gamePanels[].SetActive(false);
            transitionPanel.SetActive(true);
        }
    }
}
    
