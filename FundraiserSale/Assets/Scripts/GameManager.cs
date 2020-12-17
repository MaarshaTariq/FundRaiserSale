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
    public AudioClip flaskFillinfSound;
   
    public GameObject closeButton;
    public GameObject uiInteractions;
    public static FlaskFilling flaskFilling;
    public float fillAmountNumber=0;
    public float temp;
    public float waitTime = 10f;
    public Image flaskFiller;
    public GameObject menuManager;
    public GameObject introPanel;
    public GameObject endingPanel;
    public GameObject transitionPanel;
    public GameObject infoHandler;
    public int levelCounter;
    public int soundCounter;
    public int startingIndex=0;
    public int index;
    public int endingIndex;
    private List<int> VisitedlevelHistory;
    public static GameManager gameManager;

    public GameObject[] gamePanels;

    public bool isExternalDone = false;
    private bool accessibilty = false;

    public bool Accessibilty;
    

    public void Awake()
    {
    }
    private void Start()
    {
        gameManager = this;
        fillAmountNumber = 0.125f;
        temp = fillAmountNumber;
        VisitedlevelHistory = new List<int>();
        endingIndex = gamePanels.Length-1 ;
        //ActivatingPanels();
    }
    public void Update()
    {
       
    }
    public IEnumerator transitionActive(){
        yield return new WaitForSeconds(2);
         transitionPanel.SetActive(true);
            yield return new WaitForSeconds(3);
                        flaskFiller.fillAmount += 0.125f;

    }
    public IEnumerator randomizePanels(float sec)
    {
        yield return new WaitForSeconds(sec);
         index = Random.Range(startingIndex, endingIndex + 1);

         
        if (VisitedlevelHistory.Contains(index) == false)
        {
            levelCounter++;

            gamePanels[index].SetActive(true);
            LevelFinish(index);
            VisitedlevelHistory.Add(index);
        }
        else{
            if (levelCounter <= 8 && VisitedlevelHistory.Count != 8)
            {
                
                ActivatingPanels();
            }
            else
            {
                
                endingPanel.SetActive(true);
            }
        }
    }
    public void ActivatingPanels()
    {

        menuManager.SetActive(false);
        infoHandler.SetActive(true);
        if(levelCounter==0){
            //Debug.Log("Maarsha");
           //StartCoroutine( IntroActive());//Fix this in later build.
        }

        SetProgress(levelCounter+1);
        StartCoroutine(randomizePanels(0f));
    }

    public IEnumerator IntroActive()
    {
        introPanel.SetActive(true);
        yield return new WaitForSeconds(10);
        introPanel.SetActive(false);
    }
    public IEnumerator SwitchPanels(int indexForLevel, float seconds) {
        yield return new WaitForSeconds(seconds);

        for (int i=0; i<gamePanels.Length; i++)
        {
            if (i == indexForLevel)
            {
                gamePanels[i].SetActive(true);
                soundCounter = i;
            }
            else
            {
                gamePanels[i].SetActive(false);
            }
        }
    }
    

    public void LevelFinish(int ind)
    {
        StartCoroutine(SwitchPanels(ind, 0f));
    }
    
     IEnumerator checkvartrue(GameObject currentPanel, int index)
    {
        Debug.Log("Last image active  "+TriggerChceking.tg.lastScoreImageActive);
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
    public void deactiveCurrentPanel()
    {
        gamePanels[index].SetActive (false); 
       // ActivatingPanels();
    }
    public IEnumerator FillTheFlask()
    {
        temp = levelCounter * fillAmountNumber;
        while (flaskFiller.fillAmount <= temp)
        {
            Toolbox.SoundManager.audioPlayer.clip = flaskFillinfSound;
            Toolbox.SoundManager.audioPlayer.Play();
            flaskFiller.fillAmount +=fillAmountNumber / waitTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    void SetProgress(int count)
    {
        for (int i = 0; i < count*2.5; i++)
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
    

}
 
    

/*=======


>>>>>>> d0c85a0664eeb8e82beef658dcff4f54fc578e0e
    [DllImport("__Internal")]
    private static extern void _OnGameStarted();
    [DllImport("__Internal")]
    private static extern void _OnGameStopped();
    [DllImport("__Internal")]
    private static extern void _ExitFullScreen();

    [Header("Use these to keep track of GameNumber and Version")]
    public string _gameNumber;
    public float versionNumber;
    public bool unityLogger;


    public GameObject[] gamePanelsList;
    public GamePanels[] gamePlayLevelsList;
    public GameObject progressBars;


    public string SceneName;
    [HideInInspector]
    public int gamePlayLevelCounter = 0;
    [HideInInspector]
    public int gamePanelsCounter = 0;

    [Header("For Game testing purposes")]
    public float gameSpeed = 1;
    public bool changeGameSpeed;
    public float levelDelay;

    public GameObject AccessibiltyObject;
    public bool isExternalDone = false;
    private bool accessibilty = false;
    public bool Accessibilty
    {
        set
        {
            accessibilty = value;
        }
        get
        {
            return accessibilty;
        }
    }


    void OnEnable()
    {
        PlayerPrefs.SetInt("Click", 0);
        //Turning off Unity Debugger for removing logs.
        print("Game Number: " + _gameNumber);
        print("Version Number: " + versionNumber);
        Debug.unityLogger.logEnabled = unityLogger;
#if !UNITY_EDITOR
        _OnGameStarted();
#endif

    }

    void Update()
    {
        if (changeGameSpeed)
        {
            changeGameSpeed = false;
#if UNITY_EDITOR //for testing purposes in editor
            Time.timeScale = gameSpeed;
#endif
        }
    }

    void SetProgress(int count)
    {
        //Debug.Log ("count " + count * 2);
        for (int i = 0; i < count * 2; i++)
        {
            progressBars.transform.GetChild(i).gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void ClickOn(float delay = 0)
    {
        StartCoroutine(ClickHandle(delay, 1));
    }

    public void ClickOff(float delay = 0)
    {
        StartCoroutine(ClickHandle(delay, 0));
    }

    public bool CanClick()
    {
        bool check = false;
        if (PlayerPrefs.GetInt("Click") == 0)
            check = false;
        else if (PlayerPrefs.GetInt("Click") == 1)
            check = true;
        return check;

    }

    IEnumerator ClickHandle(float delay, int state)
    {
        yield return new WaitForSeconds(delay);
        PlayerPrefs.SetInt("Click", state);
    }

    public void OnButtonClicked(string BtnName)
    {
        if (BtnName == "FullScrren")
        {
            Debug.Log("Full Screen");
            if (Screen.fullScreen)
            {
                _ExitFullScreen();
                Screen.fullScreen = false;
                FullScreenBtn.Instance.IMG.sprite = FullScreenBtn.Instance.FullScreenIMG[0];
            }
            else
            {
                FullScreenBtn.Instance.IMG.sprite = FullScreenBtn.Instance.FullScreenIMG[1];
                Screen.fullScreen = true;
            }
        }

    }

    public IEnumerator LoadNextLevel(float sec = 0)
    {
        //Toolbox.GamePlayController.gameObject.SetActive(true);

        if (sec != 0)
        {
            levelDelay = sec;
        }
        yield return new WaitForSeconds(levelDelay);

        //Only Activating the current required panels
        for (int i = 0; i < gamePanelsList.Length; i++)
        {
            if (i == gamePanelsCounter)
            {
                gamePanelsList[i].SetActive(true);
            }
            else
            {
                gamePanelsList[i].SetActive(false);
            }
        }

        switch (gamePanelsCounter)
        {
            case 0:     //For Gameplay Panels

                if (gamePlayLevelCounter < gamePlayLevelsList.Length)
                {
                    Toolbox.GamePlayController.LoadPanel(gamePlayLevelsList[gamePlayLevelCounter]);
                }
                if (gamePlayLevelCounter == gamePlayLevelsList.Length - 1)
                {
                    //Final Gameplay level
                    gamePanelsCounter++;
                }

                SetProgress(gamePlayLevelCounter + 1);
                gamePlayLevelCounter++;
                break;

            case 1:     //For Animation Panels
                Debug.Log("Activated Animation panel");
                gamePanelsCounter++;

                break;

            case 2:     //For Final Screen
                Debug.Log("In Final Panel");
                break;
        }
    }

   

} */