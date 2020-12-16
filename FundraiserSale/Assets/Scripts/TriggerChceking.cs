using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TriggerChceking : MonoBehaviour {
    
    public AudioClip initialAudio;

    private int startingIndex=0;
    private bool firstOrderCompletion=false;
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
     //   manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tg = this;
        highlightText = new HighlightText();
    }
    private void OnEnable()
    {
        // StartCoroutine(Toolbox.SoundManager.playInitialAudio());

           StartCoroutine(initialHighlightAudio(0));

    }

    public void OnTriggerExit2D(Collider2D other)
    {

     if(!firstOrderCompletion)
     {
        for(int i=startingIndex; i<tags.Length; i++)
        {
            if(other.gameObject.tag== tags[i])
            {
             startingIndex++;
             imageToBeDragged[i].SetActive(true);
             if(i>0){
             scoreBoardImages[i-1].SetActive(false);
             }
             scoreBoardImages[i].SetActive(true);
             StartCoroutine(correctAnswer(audiosFor1stOrder[i]));
             if(startingIndex==tags.Length)
             {
                startingIndex=0;
                firstOrderCompletion=true;
                Array.Clear(tags, 0, tags.Length);
                 StartCoroutine(initialHighlightAudio(1));

                checkMarks[0].SetActive(true);
                
               // StartCoroutine(initialHighlightAudio(1));
                Debug.Log("abcccccccccccccccccc");
             }
                break;
            }
            else if(other.gameObject.tag== incorrectableTags[i])
            {
              StartCoroutine(IncorrectAnswer(Toolbox.SoundManager.getCurrentSelection(incorrectableTags[i])));
            }
        }
     }
else
{
        for(int i=startingIndex; i<tags1.Length; i++)
        {
            if(other.gameObject.tag== tags1[i])
            {
             startingIndex++;
             imagesToBeDragged1[i].SetActive(true);
             if(i>0){
                 scoreBoardImages[scoreBoardImages.Length-1].SetActive(false);
             scoreBoardImages1[i-1].SetActive(false);
             }
             scoreBoardImages1[i].SetActive(true);
             StartCoroutine(correctAnswer(audiosFor1stOrder[i]));
             if(startingIndex==tags1.Length-1){
                startingIndex=0;
                firstOrderCompletion=true;
                checkMarks[1].SetActive(true);

             }
                break;
            }
             else if(other.gameObject.tag== incorrectableTags[i])
            {
              StartCoroutine(IncorrectAnswer(Toolbox.SoundManager.getCurrentSelection(incorrectableTags[i])));
            }
        }
    }
}
    public IEnumerator correctAnswer(AudioClip clip){

    yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(clip));
    yield return StartCoroutine(Toolbox.SoundManager.playcorrectAudio());
    yield return StartCoroutine(Toolbox.SoundManager.correctAudio());
    if(firstOrderCompletion){
        if(ImagesToBeHighlighted[1]!=null){
         highlightText.highlightText(ImagesToBeHighlighted[1]);
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[1]));
        }
    }


    }
    public IEnumerator IncorrectAnswer(AudioClip clip){

    yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(clip));
    yield return StartCoroutine(Toolbox.SoundManager.playIncorrectAudio());
    yield return StartCoroutine(Toolbox.SoundManager.tryAgainAudio());


    }
    public IEnumerator initialHighlightAudio(int index)
    {
        if(!firstOrderCompletion){
          yield return StartCoroutine(Toolbox.SoundManager.playInitialAudio());
        }
        if(ImagesToBeHighlighted[index]!=null){
        Debug.Log("adfddddddddddd");
         highlightText.highlightText(ImagesToBeHighlighted[index]);
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[index]));
        }

    }
        
        // for (int j = 0; j < tagName.Length; j++) {
        //     if (imageToBeDragged.Length > 1) {
        //         if (other.gameObject.tag == tagName[j])

        //         {
                    
        //             if (index < imageToBeDragged.Length)
        //             {

        //                 Debug.Log("other.gameObject.tag" + other.gameObject.tag);
        //                 Debug.Log("tagName" + tagName[j]);
        //                 //   Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

        //                 DragAndDrop.dg.flagHit = true;
        //                 if (imageToBeDragged[index].tag == tagName[j])
        //                 {
        //                    //StartCoroutine( SoundManager.soundManager.playcorrectAudio());

        //                     //   if(tagName[j]==)
        //                     imageToBeDragged[index].SetActive(true);
        //                     //   scoreBoardImages[index].SetActive(true);
        //                     if (index > 0 && index != scoreBoardImages.Length)
        //                     {

        //                         Debug.Log(index - 1);
        //                         scoreBoardImages[index - 1].SetActive(false);
        //                         scoreBoardImages[index].SetActive(true);
        //                         // StartCoroutine(  SoundManager.soundManager.PlaySoundOfSelectedItems(GameManager.gameManager.index, index));
        //                         StartCoroutine(waitTillLength());
        //                         StartCoroutine(correctSound());
                                
        //                      //   StartCoroutine( SoundManager.soundManager.playcorrectAudio());
        //                        // StartCoroutine( SoundManager.soundManager.correctAudio());

        //                         if (index == scoreBoardImages.Length - 1)
        //                         {
        //                             checkMarks[j].SetActive(true);
        //                         }


        //                     }

        //                     else
        //                     {
        //                         if (index != scoreBoardImages.Length){
        //                             scoreBoardImages[index].SetActive(true);
        //                           // StartCoroutine( SoundManager.soundManager.PlaySoundOfSelectedItems(index, index));
        //                         }

        //                     }

        //                     index = index + 1;
        //                     if (index < imageToBeDragged.Length && imageToBeDragged[index].tag != imageToBeDragged[index - 1].tag)
        //                     {
        //                         checkMarks[j].SetActive(true);
        //                         StartCoroutine(waitingTime());
        //                         StartCoroutine(highlightText.highlightText(ImagesToBeHighlighted[j + 1]));
        //                         StartCoroutine(SoundManager.soundManager.PlayHighlight_2(index));
        //                         // GameManager.instance.transitionActive();


        //                         //  ImagesToBeHighlighted[j + 1].fillAmount = fillAmountForImage * Time.time * 0.2f;
        //                         highlighttext = true;

        //                     }

        //                     Debug.Log(index);
        //                     draggedObjectsCounter++;

        //                     //  Debug.Log(tagName[index]);
        //                     Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.dg.flagHit);


        //                 }
        //                 else
        //                 {
        //                    StartCoroutine( SoundManager.soundManager.playIncorrectAudio());
        //                   //  StartCoroutine(SoundManager.soundManager.tryAgainAudio());

        //                 }


        //             }

        //             Debug.Log(("checkmark kis ka hai" + j));
        //             //  highlightText.abc(ImagesToBeHighlighted[j]);
        //         }
                
        //     }
        //     else if (imageToBeDragged.Length == 1)
        //     {
        //         if(other.gameObject.tag==tagName[0]){
        //         imageToBeDragged[index].SetActive(true);
        //         checkMarks[index].SetActive(true);
        //         scoreBoardImages[index].SetActive(true);
        //        StartCoroutine( SoundManager.soundManager.playcorrectAudio());
        //         }
        //         else{
        //               StartCoroutine( SoundManager.soundManager.playIncorrectAudio());
        //         }
        //     }

        // }
       
        
    
    public void Update()
    {
        //highlightText.fillAmountForImage += 1 *0.2f;
      // StartCoroutine( allCheckmarksActivation());

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
    public IEnumerator waitTillLength()
    {
        StartCoroutine(SoundManager.soundManager.playcorrectAudio());

        yield return new WaitForSeconds(SoundManager.soundManager.audioPlayer.clip.length);
    }
    public IEnumerator correctSound()
    {
        StartCoroutine(SoundManager.soundManager.correctAudio());

        yield return new WaitForSeconds(SoundManager.soundManager.audioPlayer.clip.length);
    }
}
    
