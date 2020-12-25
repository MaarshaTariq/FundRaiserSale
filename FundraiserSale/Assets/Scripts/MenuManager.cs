using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	
	[DllImport("__Internal")]
	private static extern void _OnGameStopped();
	[DllImport("__Internal")]
	private static extern void _ExitFullScreen();

    public GameObject introPanel;
	public GameObject pauseScreen;
    public Image highlight;
    public List<GameObject> currentSelectables;
    private int currentListIndex = -1;

    public void OnPressPauseGame()
	{
		pauseScreen.SetActive (true);
        Time.timeScale = 0;
	}
	public void OnPressPlay()
	{
		pauseScreen.SetActive (false);
        Time.timeScale = 1;
	}
    public void OnPressMainScreenPlay()
    {
        
        StartCoroutine(MainScreenPlay());
        
    }
    public void OnPressStop()
    {
        //Check if Gameplay or Final
#if !UNITY_EDITOR
        _OnGameStopped();
#endif
    }


    private IEnumerator MainScreenPlay()
    {
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[0]));//Play Sound
        if (Toolbox.GameManager.levelCounter == 0)
        {
            introPanel.SetActive(true);
            yield return new WaitForSeconds(4f);
        }
        Toolbox.GameManager.ActivatingPanels();

        yield return new WaitForSeconds(1f);
        introPanel.SetActive(false);
    }

    public void OnPressAgain()
	{
		Time.timeScale = 1f;
		StartCoroutine(Toolbox.GameManager.LoadScene());
    }

    public IEnumerator CallStop()
    {
        yield return new WaitUntil(() => RestAPIHandler.Instance.UploadedSuccessfully==true);

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
    
    public void PauseDownArrowPressed()
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

    public void PauseUpArrowPressed()
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
    public void PauseSpacePressed()
    {
        if (Toolbox.GameManager.accessibilityCheck && !Toolbox.SoundManager.audioPlayer.isPlaying)
        {
            if (currentListIndex == 0)//On Play Button
            {
                Toolbox.MenuManager.OnPressPlay();
            }
            else//On Stop button
            {
#if !UNITY_EDITOR
                _OnGameStopped();
#endif
            }

        }
    }
    private 
        void ShowCaptionAndSound(int temp)
    {
        if (temp == 0)//Play
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[1]));
            CloseCaption.CCManager.instance.CreateCaption("Play", Toolbox.SoundManager.basicSounds[1].length);
        }
        if (temp == 1)//Stop
        {
            StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(Toolbox.SoundManager.basicSounds[2]));
            CloseCaption.CCManager.instance.CreateCaption("Stop", Toolbox.SoundManager.basicSounds[2].length);
        }

    }

}
