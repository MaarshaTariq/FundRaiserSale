using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class AccessibilityGameplay : MonoBehaviour {

    public GameObject[] currentSelectables;
    private int currentListIndex = -1;
    public Image highlight;
    public TriggerChecking panelTriger;


    private void OnDisable()
    {

        if (Toolbox.GameManager.index + 1 < Toolbox.GameManager.gamePanels.Count)
        {
            Toolbox.GameManager.index++;
        }
    }
    private void Start()
    {
        Invoke( "SortCurrentSelectables", 2f);
        currentListIndex = 3;//Because OrderForm is on the last index of CurrentSelectables.
        MoveHighlighterTo();
    }
    private void SortCurrentSelectables()
    {
        //foreach ( GameObject g in currentSelectables)
        //{
        //    print(g.GetComponent<RectTransform>().localPosition.x);

        //}
        GameObject temp;
        for (int i = 0; i < currentSelectables.Length - 1; i++)

            // traverse i+1 to array length 
            for (int j = i + 1; j < currentSelectables.Length; j++)

                // compare array element with  
                // all next element 
                if (currentSelectables[i].GetComponent<RectTransform>().localPosition.x < currentSelectables[j].GetComponent<RectTransform>().localPosition.x)
                {

                    temp = currentSelectables[i];
                    currentSelectables[i] = currentSelectables[j];
                    currentSelectables[j] = temp;
                }
        //foreach (GameObject g in currentSelectables)
        //{
        //    print(g.GetComponent<RectTransform>().localPosition.x);

        //}
    }
    public void MoveHighlighterTo()
    {
        int temp = currentListIndex;
        if (Toolbox.GameManager.accessibilityCheck)
        {
            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.anchoredPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

        }

    }
    public void DownArrowPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            int temp = getDecIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;
            highlight.rectTransform.anchoredPosition = Vector3.zero;

            ShowCaptionAndSound(highlight.transform.parent.tag);
        }
    }

    public void UpArrowPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            int temp = getIncIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;
            highlight.rectTransform.anchoredPosition = Vector3.zero;

            ShowCaptionAndSound(highlight.transform.parent.tag);
        }
    }
    public void SpacePressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying&& currentListIndex!=-1)
        {
            if (currentSelectables[currentListIndex].GetComponent<Collider2D>() != null)
            {
                panelTriger.SelectionLogic(currentSelectables[currentListIndex].GetComponent<Collider2D>());
                //Debug.Log("On Drop  other incorrect");

            }
            else
            {
                StartCoroutine(panelTriger.HighlightAudioFromBtn(panelTriger.currentOrderIndex));
            }

        }
    }

    public void IKeyPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            InfoManager.instance.ShowAccessibilityInfoBox();

        }
    }
    public void CKeyPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            Toolbox.MenuManager.OnPressPauseGame();
        }
    }

    private int getIncIndex()
    {
        if (currentListIndex + 1 > currentSelectables.Length - 1)
        {
            currentListIndex = 0;
        }
        else
        {
            currentListIndex++;
        }
        return currentListIndex;
    }
    private int getDecIndex()
    {
        if (currentListIndex - 1 <= -1)
        {
            currentListIndex = currentSelectables.Length - 1;
        }
        else
        {
            currentListIndex--;
        }
        return currentListIndex;
    }

    private void ShowCaptionAndSound(string temp)
    {
        if (temp == "cupcakes")//Option-1
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("cupcakes")));
            CloseCaption.CCManager.instance.CreateCaption("5 Cupcakes", Toolbox.SoundManager.getCurrentSelection("cupcakes").length);
        }
        if (temp == "brownies")
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("brownies")));
            CloseCaption.CCManager.instance.CreateCaption("5 Brownies", Toolbox.SoundManager.getCurrentSelection("brownies").length);
        }
        if (temp == "FR")//OPtion-3
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("FR")));
            CloseCaption.CCManager.instance.CreateCaption("10 Cookies", Toolbox.SoundManager.getCurrentSelection("FR").length);
        }
        if (temp == "OrderForm")//OrderForm
        {
            //StartCoroutine(panelTriger.HighlightAudioFromBtn(panelTriger.currentOrderIndex));
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("Order Form or Is it ???? :p")));
            CloseCaption.CCManager.instance.CreateCaption("Order Form", Toolbox.SoundManager.basicSounds[0].length);
        }
    }
}
