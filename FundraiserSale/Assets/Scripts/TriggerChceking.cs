using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerChceking : MonoBehaviour {
    public GameObject transitionPanel;
    public FlaskFilling fl;
    public bool flaskfill = false;
    public string[] tagName ;
    public bool lastScoreImageActive = false;
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
        fl = new FlaskFilling();
     //   manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tg = this;
        highlightText = new HighlightText();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        for (int j = 0; j < tagName.Length; j++) {
            if (imageToBeDragged.Length > 1) {
                if (other.gameObject.tag == tagName[j])

                {
                    
                    if (index < imageToBeDragged.Length)
                    {

                        Debug.Log("other.gameObject.tag" + other.gameObject.tag);
                        Debug.Log("tagName" + tagName[j]);
                        //   Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

                        DragAndDrop.dg.flagHit = true;
                        if (imageToBeDragged[index].tag == tagName[j])
                        {
                           //StartCoroutine( SoundManager.soundManager.playcorrectAudio());

                            //   if(tagName[j]==)
                            imageToBeDragged[index].SetActive(true);
                            //   scoreBoardImages[index].SetActive(true);
                            if (index > 0 && index != scoreBoardImages.Length)
                            {

                                Debug.Log(index - 1);
                                scoreBoardImages[index - 1].SetActive(false);
                                scoreBoardImages[index].SetActive(true);
                              StartCoroutine(  SoundManager.soundManager.PlaySoundOfSelectedItems(GameManager.gameManager.index, index));
                                StartCoroutine( SoundManager.soundManager.playcorrectAudio());

                                if (index == scoreBoardImages.Length - 1)
                                {
                                    checkMarks[j].SetActive(true);
                                }


                            }

                            else
                            {
                                if (index != scoreBoardImages.Length){
                                    scoreBoardImages[index].SetActive(true);
                                   StartCoroutine( SoundManager.soundManager.PlaySoundOfSelectedItems(index, index));
                                }

                            }

                            index = index + 1;
                            if (index < imageToBeDragged.Length && imageToBeDragged[index].tag != imageToBeDragged[index - 1].tag)
                            {
                                checkMarks[j].SetActive(true);
                                StartCoroutine(waitingTime());
                                StartCoroutine(highlightText.highlightText(ImagesToBeHighlighted[j + 1]));
                                StartCoroutine(SoundManager.soundManager.PlayHighlight_2(index));
                                // GameManager.instance.transitionActive();


                                //  ImagesToBeHighlighted[j + 1].fillAmount = fillAmountForImage * Time.time * 0.2f;
                                highlighttext = true;

                            }

                            Debug.Log(index);
                            draggedObjectsCounter++;

                            //  Debug.Log(tagName[index]);
                            Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.dg.flagHit);


                        }
                        else
                        {
                           StartCoroutine( SoundManager.soundManager.playIncorrectAudio());
                        }


                    }

                    Debug.Log(("checkmark kis ka hai" + j));
                    //  highlightText.abc(ImagesToBeHighlighted[j]);
                }
                
            }
            else if (imageToBeDragged.Length == 1)
            {
                if(other.gameObject.tag==tagName[0]){
                imageToBeDragged[index].SetActive(true);
                checkMarks[index].SetActive(true);
                scoreBoardImages[index].SetActive(true);
               StartCoroutine( SoundManager.soundManager.playcorrectAudio());
                }
                else{
                      StartCoroutine( SoundManager.soundManager.playIncorrectAudio());
                }
            }

        }
       
        
    }
    public void Update()
    {
        //highlightText.fillAmountForImage += 1 *0.2f;
       StartCoroutine( allCheckmarksActivation());

    }
   public  IEnumerator allCheckmarksActivation()
    {
      
        if (scoreBoardImages[scoreBoardImages.Length-1].activeInHierarchy)
        {
          //  Debug.Log(checkMarks[1].name);
            yield return new WaitForSeconds(3f);
            lastScoreImageActive = true;
             yield return new WaitForSeconds(2);
            // manager.gamePanels[].SetActive(false);
            //currentPanel.SetActive(false);
            // if(GameManager.instance.levelCounter<73){
            Toolbox.GameManager.deactiveCurrentPanel();
            flaskfill = true;
            transitionPanel.SetActive(true);
           
           
        
            yield return new WaitForSeconds(1f);
            transitionPanel.SetActive(false);
          
        
            //}
           
           // transitionPanel.SetActive(true);
           
       //StartCoroutine (GameManager.instance.transitionActive());
       //Debug.Log("abc");
            // GameManager.instance.ActivatingPanels();
           // StartCoroutine(deActivateTransitionPanels());
        }
    }
    
    public IEnumerator deActivateTransitionPanels()
    {
        yield return new WaitForSeconds(2f);
        transitionPanel.SetActive(false);
        Toolbox.GameManager.ActivatingPanels();
    
    }
    public IEnumerator waitingTime(){
        yield return new WaitForSeconds(2);
    }
}
    
