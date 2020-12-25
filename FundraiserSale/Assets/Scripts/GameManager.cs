using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //<<<<<<< HEAD
    [DllImport("__Internal")]
    private static extern void _OnGameStarted();
    [DllImport("__Internal")]
    private static extern void _OnGameStopped();
    [DllImport("__Internal")]
    private static extern void _ExitFullScreen();

    public string SceneName;//To Load Scene again on pressAgain.
    public GameObject progressBars;

    public GameObject closeButton;
    public GameObject uiInteractions;

    public GameObject infoHandler;
    public GameObject menuManager;
    public GameObject endingPanel;
    public GameObject transitionPanel;
    public List<GameObject> gamePanels;

    public Image fullScreenImg;
    public int levelCounter;
    public int index;//ForAccessibility Stuff

    public Sprite[] FullScreenIMG;

    public int gameSpeed;//For testing purposes
    [HideInInspector]
    public int transitionCounter = 0;

    //Accessibility items
    private bool accessibilty = false;
    public bool isExternalDone = false;
    public bool accessibilityCheck;

    public void Awake()
    {
    }
    
    private void Start()
    {

#if !UNITY_EDITOR
        _OnGameStarted();   
#endif
#if UNITY_EDITOR
        Time.timeScale = gameSpeed;
#endif

        ShuffleGamePanels();
    }


    private void ShuffleGamePanels()
    {
        for (int i = 0; i < gamePanels.Count; i++)
        {
            int randIndex = Random.Range(0, gamePanels.Count);
            GameObject temp = gamePanels[randIndex];
            gamePanels.RemoveAt(randIndex);
            gamePanels.Add(temp);

        }
        //foreach (GameObject g in gamePanels)
        //{
        //    Debug.Log(g.name);
        //}
    }

    public void ActivatingPanels()
    {
        transitionPanel.SetActive(false);
        menuManager.SetActive(false);
        infoHandler.SetActive(true);

        if (levelCounter < gamePanels.Count)
        {
            gamePanels[levelCounter].SetActive(true);
            SetProgress(levelCounter + 1);
        }
        else
        {
            endingPanel.SetActive(true);
        }
                       
        levelCounter++;
    }
    public void ActivateTransitionpanel()
    {
        this.transitionPanel.SetActive(true);
    }
    
    public void DeavtivateAllActiveGamePanels()
    {
        foreach (GameObject g in gamePanels)
        {
            g.SetActive(false);
        }

    }

    void SetProgress(int count)
    {
        for (int i = 0; i < count * 2.5; i++)
        {
            progressBars.transform.GetChild(i).gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
    public void OnPressAgain()//Being called from Again button on FinalScreen
    {
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        if (Screen.fullScreen)
        {
#if !UNITY_EDITOR
            _ExitFullScreen();
#endif
            Screen.fullScreen = !Screen.fullScreen;
        }
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
    public void DeactivateUiInteractions()
    {
        uiInteractions.SetActive(false);

    }
    public void ActivateUiInteractions()
    {
        uiInteractions.SetActive(true);
        closeButton.SetActive(true);
        progressBars.transform.parent.gameObject.SetActive(true);
    }
    public void MaximizeButtonPressed()
    {
        if (Screen.fullScreen)
        {
#if !UNITY_EDITOR
            _ExitFullScreen();
#endif
            Screen.fullScreen = false;
            fullScreenImg.sprite = FullScreenIMG[0];
        }
        if (!Screen.fullScreen)
        {
            fullScreenImg.sprite = FullScreenIMG[1];
            Screen.fullScreen = true;
        }
    }
    

    public void CloseButtonPressed()
    {
#if !UNITY_EDITOR
        _OnGameStopped();
#endif
    }
}