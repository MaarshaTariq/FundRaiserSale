using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TriggerChceking : MonoBehaviour {
    

    private int indexCounter=0;
    private bool firstOrderCompletion=false;
    private int currentOrderIndex=0;

    public GameObject transitionPanel;
    public FlaskFilling fl;
    public bool flaskfill = false;
    public string[] incorrectableTags;
    public string[] tagName ;
    public bool lastScoreImageActive = false;
    public bool highlighttext = false;
    public static TriggerChceking tg;

    public Image[] ImagesToBeHighlighted;
    public float maxFillAmount = 400f;
    public static float fillAmountForImage;

    GameManager manager;
    public AudioClip[] highLightAudios;
    public AudioClip[] audiosFor1stOrder;
    public AudioClip[] audiosFor2ndOrder;

    public GameObject[] imageToBeDragged;
    public GameObject[] imagesToBeDragged1;
    public GameObject[] scoreBoardImages;
    public GameObject[] scoreBoardImages1;
    public GameObject[] checkMarks;
   
    public string[] tags ;

    public string[] tags1;

    HighlightText highlightText;
    int index = 0;
    int indexForTags = 0;
    public bool checkMarksTags = false;
    public int draggedObjectsCounter;
    public void Start()
    {
        fl = new FlaskFilling();
        tg = this;
        highlightText = new HighlightText();
    }
    private void OnEnable()
    {
        firstOrderCompletion = false;
        StartCoroutine(HighlightAudio(0));
        Toolbox.GameManager.ActivateUiInteractions();
    }
    private void OnDisable()
    {
    }
    public void OnTriggerExit2D(Collider2D other)
    {

         if(!firstOrderCompletion)
         {
            for(int i=indexCounter; i<tags.Length; i++)
            {
                    if (other.gameObject.tag == tags[i])
                    {
                        indexCounter++;
                        imageToBeDragged[i].SetActive(true);
                    if (ImagesToBeHighlighted[0].isActiveAndEnabled)
                    {
                        ImagesToBeHighlighted[0].enabled = false;
                    }
                    if (i > 0)
                        {
                            scoreBoardImages[i - 1].SetActive(false);
                        }
                        scoreBoardImages[i].SetActive(true);
                        StartCoroutine(CorrectAnswer(audiosFor1stOrder[i]));
                        if (indexCounter == tags.Length)
                        {
                            currentOrderIndex = 1;
                            indexCounter = 0;
                            firstOrderCompletion = true;
                            checkMarks[0].SetActive(true);
                        }
                        break;
                    }
                    else
                    {
                       StartCoroutine(IncorrectAnswer( Toolbox.SoundManager.getCurrentSelection(other.tag)));
                    }
            }
         }
        else
        {
            for(int i=indexCounter; i<tags1.Length; i++)
            {
                if(other.gameObject.tag== tags1[i])
                {
                     indexCounter++;
                     imagesToBeDragged1[i].SetActive(true);
                    if (ImagesToBeHighlighted[1].isActiveAndEnabled)
                    {
                        ImagesToBeHighlighted[1].enabled = false;
                    }

                    if (i>0)
                     {
                         scoreBoardImages1[i-1].SetActive(false);
                     }
                     scoreBoardImages1[i].SetActive(true);

                     StartCoroutine(CorrectAnswer(audiosFor2ndOrder[i]));
                     if(indexCounter==tags1.Length)
                     {
                        firstOrderCompletion=true;
                        checkMarks[1].SetActive(true);
                     }
                        break;
                }
                else
                {
                    StartCoroutine(IncorrectAnswer(Toolbox.SoundManager.getCurrentSelection(other.tag)));
                }
            }
        }
    }
    public IEnumerator CorrectAnswer(AudioClip clip)
    {
        Debug.Log("IndexCounter: "+indexCounter);
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(clip));
        yield return StartCoroutine(Toolbox.SoundManager.playcorrectAudio());
        yield return StartCoroutine(Toolbox.SoundManager.correctAudio());

        if (firstOrderCompletion && indexCounter < 1)
        {
            yield return StartCoroutine(HighlightAudio(1));//For Second Option
        }
        if (firstOrderCompletion && indexCounter >= tags1.Length)
        {
            //Activate next Panel
            InfoManager.instance.CloseInfoBox();
            yield return AllCheckmarksActivation();
            
            Toolbox.GameManager.ActivatingPanels();
        }
    }
    public IEnumerator IncorrectAnswer(AudioClip clip)
    {
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(clip));
        yield return StartCoroutine(Toolbox.SoundManager.playIncorrectAudio());
        yield return StartCoroutine(Toolbox.SoundManager.tryAgainAudio());
    }
    public IEnumerator HighlightAudio(int index)
    {
        if (!firstOrderCompletion && index==0)
        {
            yield return StartCoroutine(Toolbox.SoundManager.playInitialAudio());
            StartCoroutine(highlightText.highlightText(ImagesToBeHighlighted[index]));
            yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[index]));
        }
        else
        {
            if (firstOrderCompletion&& ImagesToBeHighlighted.Length>1)//If Orders are more than 1
            {
                scoreBoardImages[scoreBoardImages.Length - 1].SetActive(false);
                StartCoroutine(highlightText.highlightText(ImagesToBeHighlighted[index]));
                yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[index]));
            }
        }
    }
    public IEnumerator HighlightAudioFromBtn(int index)
    {
        if (index == 0)
        {
            yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[index]));
        }
        else
        {
            yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[index]));
        }
    }
    public void PlayHighlightAudio()//Being Called from Orders button
    {
        StartCoroutine(HighlightAudioFromBtn(currentOrderIndex));
    }
        
   
   public  IEnumerator AllCheckmarksActivation()
    {
      
            yield return new WaitForSeconds(3f);
            lastScoreImageActive = true;
            yield return new WaitForSeconds(2);
            Toolbox.GameManager.deactiveCurrentPanel();
            flaskfill = true;
            Toolbox.GameManager.DeactivateUiInteractions();
            transitionPanel.SetActive(true);
    
        
            yield return new WaitForSeconds(1f);
            transitionPanel.SetActive(false);




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
    
