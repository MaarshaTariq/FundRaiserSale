using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public class EndScreenManager : MonoBehaviour {


    [DllImport("__Internal")]
    private static extern void _OnGameStopped();
    [DllImport("__Internal")]
    private static extern void _ExitFullScreen();

    public AudioClip endingPanelClip;

    //Accessiblity Items
    public Image highlight;
    public List<GameObject> currentSelectables;
    private int currentListIndex = -1;
    [HideInInspector]
    public static EndScreenManager instance; 

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Debug.Log("EndScreen");
    }
    private void Start()
    {
        StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(endingPanelClip));
    }
    public void OnPressStop()
    {
        //Final
#if !UNITY_EDITOR
        _OnGameStopped();
#endif
    }

    public void OnPressAgain()
    {
        Time.timeScale = 1f;
        StartCoroutine(Toolbox.GameManager.LoadScene());
    }

    public IEnumerator CallStop()
    {
        yield return new WaitUntil(() => RestAPIHandler.Instance.UploadedSuccessfully == true);

#if !UNITY_EDITOR
        _OnGameStopped();
#endif

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

    public void EndScreenDownArrowPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            int temp = getIncIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.localPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

            ShowCaptionAndSound(temp);
        }
    }

    public void EndScreenUpArrowPressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            int temp = getDecIndex();

            highlight.transform.SetParent(currentSelectables[temp].transform);
            highlight.rectTransform.localPosition = Vector3.zero;
            highlight.rectTransform.sizeDelta = highlight.transform.parent.GetComponent<RectTransform>().sizeDelta;
            highlight.rectTransform.rotation = highlight.transform.parent.GetComponent<RectTransform>().rotation;

            ShowCaptionAndSound(temp);
        }
    }
    public void EndScreenSpacePressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            if (currentListIndex == 0)//On Again Button
            {
                this.OnPressAgain();
            }
            else//On Stop button
            {
                this.OnPressStop();
            }

        }
    }
    private void ShowCaptionAndSound(int temp)
    {
        if (temp == 0)//Play
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[4]));
            CloseCaption.CCManager.instance.CreateCaption("Again", Toolbox.SoundManager.basicSounds[4].length);
        }
        if (temp == 1)//Stop
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[2]));
            CloseCaption.CCManager.instance.CreateCaption("Stop", Toolbox.SoundManager.basicSounds[2].length);
        }
    }
}
