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
            highlight.rectTransform.anchoredPosition = new Vector3(0,0,0);

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
            highlight.rectTransform.localPosition = Vector3.zero;

            ShowCaptionAndSound(temp);
        }
    }
    public void SpacePressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            panelTriger.SelectionLogic(currentSelectables[currentListIndex].GetComponent<Collider2D>());

        }
    }

    public void IKeyPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            //InfoManager.Instance.SoundHandler();
        }
    }
    public void CKeyPressed()
    {
        if (Toolbox.GameManager.Accessibilty && !Toolbox.SoundManager.audioPlayer.isPlaying)
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
        if (temp == 0)//Option-1
        {
<<<<<<< Updated upstream
            Toolbox.SoundManager._playSoundWithAudioClip( Toolbox.SoundManager.getCurrentSelection("cupcakes"));
            CloseCaption.CCManager.instance.CreateCaption("cupcakes", Toolbox.SoundManager.basicSounds[1].length);
=======
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("cupcakes")));
            CloseCaption.CCManager.instance.CreateCaption("5 Cupcakes", Toolbox.SoundManager.getCurrentSelection("cupcakes").length);
>>>>>>> Stashed changes
        }
        if (temp == 1)//Option-2
        {
<<<<<<< Updated upstream
            Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("brownies"));
            CloseCaption.CCManager.instance.CreateCaption("brownies", Toolbox.SoundManager.basicSounds[0].length);
        } if (temp == 2)
        {
            Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("FR"));
            Debug.Log("FundraiserSale_Caption");
            CloseCaption.CCManager.instance.CreateCaption("cookies", Toolbox.SoundManager.basicSounds[0].length);
=======
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
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("FR")));
            CloseCaption.CCManager.instance.CreateCaption("Order Form", Toolbox.SoundManager.basicSounds[0].length);
        }
        if (temp == 4)//InfoBox
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("FR")));
            CloseCaption.CCManager.instance.CreateCaption("Complete the order by choosing the correct number of baked goods", Toolbox.SoundManager.basicSounds[0].length);
        }
        if (temp == 4)//CloseButton
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.getCurrentSelection("FR")));
            CloseCaption.CCManager.instance.CreateCaption("10 Cookies", Toolbox.SoundManager.basicSounds[0].length);
>>>>>>> Stashed changes
        }

    }
}
