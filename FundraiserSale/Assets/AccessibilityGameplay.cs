using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessibilityGameplay : MonoBehaviour {

    public List<GameObject> currentSelectables;
    private int currentListIndex = -1;
    public Image highlight;
    public TriggerChceking panelTriger;

    private bool playButtonPressedCheck = true;
    public void Awake()
    {
        //Toolbox.Set_MainMenuManger(this);
    }
    public void DownArrowPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            int temp = getDecIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;
            highlight.rectTransform.anchoredPosition = Vector3.zero;

            ShowCaptionAndSound(temp);
        }
    }

    public void UpArrowPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            int temp = getIncIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;
            highlight.rectTransform.anchoredPosition = Vector3.zero;

            ShowCaptionAndSound(temp);
        }
    }
    public void SpacePressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            if (currentSelectables[currentListIndex].GetComponent<Collider2D>() != null)
            {
                panelTriger.SelectionLogic(currentSelectables[currentListIndex].GetComponent<Collider2D>());
                Debug.Log("On Drop  other incorrect");

            }
            else
            {
                StartCoroutine(panelTriger.HighlightAudioFromBtn(panelTriger.currentOrderIndex));
            }

        }
    }

    public void IKeyPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            InfoManager.instance.ShowAccessibilityInfoBox();

        }
    }
    public void CKeyPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            Toolbox.MenuManager.OnPressPauseGame();
        }
    }

    private int getIncIndex()
    {
        if (currentListIndex + 1 > currentSelectables.Count - 1)
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
            currentListIndex = currentSelectables.Count - 1;
        }
        else
        {
            currentListIndex--;
        }
        return currentListIndex;
    }

    private void ShowCaptionAndSound(int temp)
    {
        if (temp == 0)//Option-1
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("cupcakes")));
            CloseCaption.CCManager.instance.CreateCaption("5 Cupcakes", Toolbox.SoundManager.getCurrentSelection("cupcakes").length);
        }
        if (temp == 1)
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("brownies")));
            CloseCaption.CCManager.instance.CreateCaption("5 Brownies", Toolbox.SoundManager.getCurrentSelection("brownies").length);
        }
        if (temp == 2)//OPtion-3
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("FR")));
            CloseCaption.CCManager.instance.CreateCaption("10 Cookies", Toolbox.SoundManager.getCurrentSelection("FR").length);
        }
        if (temp == 3)//OrderForm
        {
            //StartCoroutine(panelTriger.HighlightAudioFromBtn(panelTriger.currentOrderIndex));
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("Order Form or Is it ???? :p")));
            CloseCaption.CCManager.instance.CreateCaption("Order Form", Toolbox.SoundManager.basicSounds[0].length);
        }
    }
}
