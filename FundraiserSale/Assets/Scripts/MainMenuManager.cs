using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public List<GameObject> currentSelectables;
    private int currentListIndex = -1;
    public Image highlight;

    private bool playButtonPressedCheck = true;
    public void Awake()
    {
        Toolbox.Set_MainMenuManger(this);
    }
    public void DownArrowPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && playButtonPressedCheck)
        {
            int temp = getIncIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.localPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

            ShowCaptionAndSound(temp);
        }
    }

    public void UpArrowPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && playButtonPressedCheck)
        {
            int temp = getDecIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.localPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

            ShowCaptionAndSound(temp);
        }
    }
    public void SpacePressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && playButtonPressedCheck && currentListIndex!=-1)
        {
            if (currentListIndex == 0)
            {
                Toolbox.MenuManager.OnPressMainScreenPlay();
                playButtonPressedCheck = false;
            }

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
        if (temp == 0)
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[1]));
            CloseCaption.CCManager.instance.CreateCaption("Play", Toolbox.SoundManager.basicSounds[1].length);
        }
        if (temp == 1)
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[0]));
            CloseCaption.CCManager.instance.CreateCaption("Fundraiser Sale", Toolbox.SoundManager.basicSounds[0].length);
        }

    }
}
