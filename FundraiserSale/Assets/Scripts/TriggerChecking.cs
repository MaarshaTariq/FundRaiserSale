using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TriggerChecking : MonoBehaviour
{

    private int indexCounter = 0;
    private bool firstOrderCompletion = false;
    [HideInInspector]
    public int currentOrderIndex = 0;

    public string[] incorrectableTags;
    public string[] tagName;
    public bool lastScoreImageActive = false;
    public bool highlighttext = false;

    public Image[] ImagesToBeHighlighted;

    public AudioClip[] highLightAudios;
    public AudioClip[] audiosFor1stOrder;
    public AudioClip[] audiosFor2ndOrder;

    public GameObject[] imageToBeDragged;
    public GameObject[] imagesToBeDragged1;
    public GameObject[] scoreBoardImages;
    public GameObject[] scoreBoardImages1;
    public GameObject[] checkMarks;
    public Button[] highlightAudioButtons;

    public string[] tags;

    public string[] tags1;

    HighlightText highlightText;
    public bool checkMarksTags = false;
    public int draggedObjectsCounter;

    private void Start()
    {
        highlightText = gameObject.AddComponent<HighlightText>();
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

        //SelectionLogic(other);


    }

    public void SelectionLogic(Collider2D other)
    {
        if (!firstOrderCompletion)
        {
            highlightAudioButtons[0].interactable=true;
            for (int i = indexCounter; i < tags.Length; i++)
            {
                if (other.gameObject.tag == tags[i])
                {
                    indexCounter++;
                    imageToBeDragged[i].SetActive(true);
                    if (ImagesToBeHighlighted[0].isActiveAndEnabled)
                    {
                        ImagesToBeHighlighted[0].color =Color.clear;
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
                        highlightAudioButtons[0].interactable=false;
                        highlightAudioButtons[1].interactable=true;
                    }
                    break;
                }
                else
                {
                    StartCoroutine(IncorrectAnswer(Toolbox.SoundManager.getCurrentSelection(other.tag)));
                }
            }
        }
        else
        {

            for (int i = indexCounter; i < tags1.Length; i++)
            {
                if (other.gameObject.tag == tags1[i])
                {
                    indexCounter++;
                    imagesToBeDragged1[i].SetActive(true);
                    if (ImagesToBeHighlighted[1].isActiveAndEnabled)
                    {
                        ImagesToBeHighlighted[1].color = Color.clear;
                    }

                    if (i > 0)
                    {
                        scoreBoardImages1[i - 1].SetActive(false);
                    }
                    scoreBoardImages1[i].SetActive(true);

                    StartCoroutine(CorrectAnswer(audiosFor2ndOrder[i]));
                    if (indexCounter == tags1.Length)
                    {
                        //indexCounter = 0;
                        firstOrderCompletion = true;
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
        //  highlightAudioButtons[1].SetActive(false);
    }

    public IEnumerator CorrectAnswer(AudioClip clip)
    {
        //Debug.Log("IndexCounter: "+indexCounter);
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(clip));


        if (firstOrderCompletion && indexCounter < 1)
        {
            yield return StartCoroutine(Toolbox.SoundManager.playcorrectAudio());
            yield return StartCoroutine(Toolbox.SoundManager.correctAudio());
            yield return StartCoroutine(HighlightAudio(1));//For Second Option
        }
        if (firstOrderCompletion && indexCounter >= tags1.Length && indexCounter != 0)
        {
            yield return StartCoroutine(Toolbox.SoundManager.playcorrectAudio());
            yield return StartCoroutine(Toolbox.SoundManager.correctAudio());
            //Activate next Panel
            InfoManager.instance.CloseInfoBox();
            yield return StartCoroutine(AllCheckmarksActivation());
        }
        else if (firstOrderCompletion && indexCounter >= tags1.Length)
        {
            InfoManager.instance.CloseInfoBox();

            yield return StartCoroutine(AllCheckmarksActivation());
        }
    }
    public IEnumerator IncorrectAnswer(AudioClip clip)
    {
        EventController.instance.wrongOptionSelectionCounter++;
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(clip));
        yield return StartCoroutine(Toolbox.SoundManager.playIncorrectAudio());
        yield return StartCoroutine(Toolbox.SoundManager.tryAgainAudio());
    }
    public IEnumerator HighlightAudio(int index)
    {
        if (!firstOrderCompletion && index == 0)
        {
            yield return StartCoroutine(Toolbox.SoundManager.playInitialAudio());
            StartCoroutine(highlightText._HighlightText(ImagesToBeHighlighted[index]));
            yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(highLightAudios[index]));
        }
        else
        {
            if (firstOrderCompletion && ImagesToBeHighlighted.Length > 1)//If Orders are more than 1
            {
                scoreBoardImages[scoreBoardImages.Length - 1].SetActive(false);
                StartCoroutine(highlightText._HighlightText(ImagesToBeHighlighted[index]));
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


    public IEnumerator AllCheckmarksActivation()
    {
        //Debug.Log("HERE" + Toolbox.GameManager.levelCounter);
        Toolbox.GameManager.DeavtivateAllActiveGamePanels();
        Toolbox.GameManager.DeactivateUiInteractions();
        Toolbox.GameManager.ActivateTransitionpanel();

        yield return new WaitForSeconds(0f);

    }


}

