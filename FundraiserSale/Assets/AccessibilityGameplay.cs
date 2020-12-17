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
        if (Toolbox.GameManager.Accessibilty && playButtonPressedCheck)
        {
            int temp = getDecIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.localPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

            ShowCaptionAndSound(temp);
        }
    }

    public void UpArrowPressed()
    {
        if (Toolbox.GameManager.Accessibilty && playButtonPressedCheck)
        {
            int temp = getIncIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.localPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

            ShowCaptionAndSound(temp);
        }
    }
    public void SpacePressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.inputBlocker.activeInHierarchy)
        {
            panelTriger.SelectionLogic(currentSelectables[currentListIndex].GetComponent<Collider2D>());

        }
    }

    public void IKeyPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.inputBlocker.activeInHierarchy)
        {
            //InfoManager.Instance.SoundHandler();
        }
    }
    public void CKeyPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.inputBlocker.activeInHierarchy)
        {
            //GameManager.Instance.OpenPauseScreen();
            //checkButtonAnimation = false;
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
            Debug.Log("Play_Caption");
            CloseCaption.CCManager.instance.CreateCaption("cupcakes", Toolbox.SoundManager.basicSounds[1].length);
        }
        if (temp == 1)
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[0]));
            Debug.Log("FundraiserSale_Caption");
            CloseCaption.CCManager.instance.CreateCaption("brownies", Toolbox.SoundManager.basicSounds[0].length);
        } if (temp == 2)
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[0]));
            Debug.Log("FundraiserSale_Caption");
            CloseCaption.CCManager.instance.CreateCaption("cookies", Toolbox.SoundManager.basicSounds[0].length);
        }

    }
}
