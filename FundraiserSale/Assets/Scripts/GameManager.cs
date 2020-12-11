using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
    public Image flaskFiller;
    public GameObject transitionPanel;
    public int levelCounter;
    public int startingIndex=0;
    int index;
    public int endingIndex;
    private List<int> VisitedlevelHistory;

    // TriggerChceking tg;
    public static GameManager instance;
    public GameObject[] gamePanels;
   

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        instance = this;
       // tg = new TriggerChceking();
        VisitedlevelHistory = new List<int>();
        endingIndex = gamePanels.Length - 1;
     //   ActivatingPanels();
    }
    public void Update()
    {
       // if (TriggerChceking.tg.lastScoreImageActive)
       // {
        //    repeat(gamePanels[index], index);
         //   ActivatingPanels();
        //}
        //checkvartrue();
      // repeat (gamePanels[index], index);
    }
    public IEnumerator randomizePanels(float sec)
    {
        yield return new WaitForSeconds(sec);
         index = Random.Range(startingIndex, endingIndex + 1);
        if (VisitedlevelHistory.Contains(index) == false)
        {
            levelCounter++;


           // Debug.LogError("index ix " + index + " " + levelCounter);

            gamePanels[index].SetActive(true);
            transitionPanel.SetActive(false);
            // tg.allCheckmarksActivation();
            //if (tg.checkMarks[1].activeInHierarchy)
            //{
              //  transitionPanel.SetActive(true);
            //}
            flaskFiller.fillAmount += 0.125f;


            LevelFinish(index);
          
            VisitedlevelHistory.Add(index);
           // manager.levelCounter = index;
            //checker = true;
        }
    }
    public void ActivatingPanels()
    {
        StartCoroutine(randomizePanels(2f));
    }
    public IEnumerator switchPanels(int indexForLevel, float seconds) {
        yield return new WaitForSeconds(seconds);

        for (int i=0; i<gamePanels.Length; i++)
        {
            if (i == indexForLevel)
            {
                gamePanels[i].SetActive(true);
            }
            else
            {
                gamePanels[i].SetActive(false);
            }
            levelCounter++;
        }
    }
    

    public void LevelFinish(int ind)
    {
        
        StartCoroutine(switchPanels(ind, 1.1f));
       
    }
    
     IEnumerator checkvartrue(GameObject currentPanel, int index)
    {
        Debug.Log("last image active  "+TriggerChceking.tg.lastScoreImageActive);
       // if (TriggerChceking.tg.lastScoreImageActive)
        //{
            yield return new WaitForSeconds(2f);

            currentPanel.SetActive(false);
            transitionPanel.SetActive(true);
          //  yield return new WaitForSeconds(2f);


        //}
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
    }
}
 /*   

=======


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

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        if (Screen.fullScreen)
        {
            _ExitFullScreen();
            Screen.fullScreen = !Screen.fullScreen;
        }
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

}