using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class AccessibilityManager : MonoBehaviour
{
    #region Variables

    #region private
    bool checkActivity = false;
    #endregion
    #region public
    [HideInInspector] public bool backOfficeInitDone = false;
    public int gameNumber;
    public bool ShowPausePanel = true;//Can be used to check where we can Display Pause panel and Info box

    public GameObject YellowBox;
    public GameObject block;
    public GameObject LoadingScreen;
    public GameObject pausePanel;

    [HideInInspector]
    public float timeOut = 30.0f, timeOutTimer = 0.0f; // Counter to check for screen time out function

    #endregion

    #endregion

    #region  functions

    //populating the root
    void Start()
    {
        if (LoadingScreen)
        {
            //LoadingScreen.SetActive(true);//We can place the loading screen panel here and use that to wait while backend is fetching data.
        }
        ShowPausePanel = true;

        if (YellowBox != null)
            YellowBox.SetActive(true);
    }
    IEnumerator StartingAccessibilty()
    {
        yield return new WaitUntil(() => Toolbox.TextToSpeech.AudioDownloaded == Toolbox.TextToSpeech.TotalAudioToDownload);

        if (LoadingScreen)
            LoadingScreen.SetActive(false);
    }

    public void EndGame()
    {
        //EndGame Functionality 

    }



    #region userInput Fuction
    //<--------- User controls -------- called from index.html file ----------------->
    public void swipeUp()
    {
        if (Toolbox.MainMenuManager.gameObject.activeInHierarchy)//For Title Screen
            Toolbox.MainMenuManager.DownArrowPressed();
        else if (Toolbox.MenuManager.pauseScreen.gameObject.activeInHierarchy)//For Pause Screen During Gameplay
            Toolbox.MenuManager.PauseDownArrowPressed();
        else if (Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].gameObject.activeInHierarchy)//For Gameplay screen
            Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].GetComponent<AccessibilityGameplay>().DownArrowPressed();
        else if (Toolbox.EndScreenManager.gameObject.activeInHierarchy)
            Toolbox.EndScreenManager.EndScreenDownArrowPressed();

        checkActivity = true;
    }

    public void swipeDown()
    {
        if (Toolbox.MainMenuManager.gameObject.activeInHierarchy)//For Title Screen
            Toolbox.MainMenuManager.UpArrowPressed();
        else if (Toolbox.MenuManager.pauseScreen.gameObject.activeInHierarchy)//For Pause Screen During Gameplay
            Toolbox.MenuManager.PauseUpArrowPressed();
        else if (Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].gameObject.activeInHierarchy)//For Gameplay screen
            Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].GetComponent<AccessibilityGameplay>().UpArrowPressed();
        else if (Toolbox.EndScreenManager.gameObject.activeInHierarchy)
            Toolbox.EndScreenManager.EndScreenUpArrowPressed();
        checkActivity = true;
    }
    public void Info()
    {
        if (Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].activeInHierarchy)
            Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].GetComponent<AccessibilityGameplay>().IKeyPressed();

    }
    public void select()
    {
        if (Toolbox.MainMenuManager.gameObject.activeInHierarchy)
            Toolbox.MainMenuManager.SpacePressed();
        else if (Toolbox.MenuManager.pauseScreen.gameObject.activeInHierarchy)//For Pause Screen During Gameplay
            Toolbox.MenuManager.PauseSpacePressed();
        else if (Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].activeInHierarchy)
            Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].gameObject.GetComponent<AccessibilityGameplay>().SpacePressed();
        else if (Toolbox.EndScreenManager.gameObject.activeInHierarchy)
            Toolbox.EndScreenManager.EndScreenSpacePressed();

        checkActivity = true;

    }
    public void Close()
    {
        if (Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].activeInHierarchy)
            Toolbox.GameManager.gamePanels[Toolbox.GameManager.index].GetComponent<AccessibilityGameplay>().CKeyPressed();
    }

    // Variable to let info play while being controls are freezed
    bool isUrgentInfo = false;

    public void unSelect()//Not used in Game 112 Fundraiser sale
    {
    }

    public void Info(bool isUrgentInfo)
    {
        this.isUrgentInfo = isUrgentInfo;
        Info();
        this.isUrgentInfo = false;
    }
    #endregion
    //<-----------------------------------------------user controls------------------------------------------>
    void Update()
    {
        PlayerPrefs.SetString("clickable", "false");
        PlayerPrefs.SetInt("Click", 1);

        ScreenTimeoutNotifier();

        bool down = Input.GetKeyDown(KeyCode.DownArrow);
        bool goBack = Input.GetKeyDown(KeyCode.Backspace);
        bool Up = Input.GetKeyDown(KeyCode.UpArrow);
        bool space = Input.GetKeyDown(KeyCode.Space);
        bool info = Input.GetKeyDown(KeyCode.I);
        bool close = Input.GetKeyDown(KeyCode.C);

        if (down)
        {
            swipeUp();
            checkActivity = true;
            down = false;
        }
        if (Up)
        {
            swipeDown();
            checkActivity = true;
            Up = false;
        }
        if (space)
        {
            select();
            checkActivity = true;
            space = false;
        }
        if (goBack)
        {
            unSelect();
            checkActivity = true;
            goBack = false;
        }
        if (info && ShowPausePanel)
        {
            Info();
            checkActivity = true;
            info = false;
        }
        if (close && ShowPausePanel )
        {
            Close();
            close = false;
        }

    }


    public void ScreenTimeoutNotifier()
    {
        timeOutTimer += Time.deltaTime;
        // If screen is tapped, reset timer
        if (checkActivity)
        {
            timeOutTimer = 0.0f;
            checkActivity = false;
            //Dont active screensaver
        }
        if (timeOutTimer > timeOut )
        {
            timeOutTimer = -2.0f;
        }
    }
    #endregion
}