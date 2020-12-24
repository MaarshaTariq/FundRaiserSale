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

    public string SceneName;

    public GameObject progressBars;
   
    public AudioClip endingPanelSound;

    public GameObject closeButton;
    public GameObject uiInteractions;
    public Sprite[] FullScreenIMG;
    public static FlaskFilling flaskFilling;
    public float fillAmountNumber = 0;
    public float temp;
    
   
    public GameObject menuManager;
    public GameObject endingPanel;
    public GameObject transitionPanel;
    public GameObject infoHandler;
    public Image fullScreenImg;
    public int levelCounter;
    public int soundCounter;
    public int startingIndex = 0;
    public int index;
    public int endingIndex;
    [HideInInspector]
    public List<int> VisitedlevelHistory;
    public static GameManager gameManager;

    public List<GameObject> gamePanels;
    public int gameSpeed;
    [HideInInspector]
    public int transitionCounter = 0;

    public bool isExternalDone = false;
    private bool accessibilty = false;

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

        gameManager = this;
        fillAmountNumber = 0.125f;
        temp = fillAmountNumber;
        VisitedlevelHistory = new List<int>();

        ShuffleGamePanels();
        gamePanels.Add(endingPanel);
    }
   

    private void ShuffleGamePanels()
    {
        for (int i = 0; i < gamePanels.Count; i++)
        {
            int randIndex = Random.Range(0,gamePanels.Count);
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

        if (levelCounter > 7)
        {
            Debug.Log("LC"+ levelCounter);
            Debug.Log("GPC"+ gamePanels.Count);
            
        }

        gamePanels[levelCounter].SetActive(true);
        SetProgress(levelCounter + 1);




        levelCounter++;
    }
    


    IEnumerator checkvartrue(GameObject currentPanel, int index)
    {
        Debug.Log("Last image active  " + TriggerChceking.tg.lastScoreImageActive);
        // if (TriggerChceking.tg.lastScoreImageActive)
        //{
        yield return new WaitForSeconds(2f);

        currentPanel.SetActive(false);
    }
    public IEnumerator checkvartruecoroutine(GameObject currentPanel, int index)
    {
        StartCoroutine(checkvartrue(currentPanel, index));
        yield return new WaitForSeconds(1f);

        // ActivatingPanels();
    }
    public void repeat(GameObject currentPanel, int index)
    {
        StartCoroutine(checkvartruecoroutine(currentPanel, index));
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
    public void ChangeMaximizeButtonSprite(int index)
    {
        //  img.sprite = FullScreenBtn.InstanceOfFullScreen.FullScreenIMG[index];
    }

    public void CloseButtonPressed()
    {
#if !UNITY_EDITOR
        _OnGameStopped();
#endif
    }
}