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

    public int targetCounter = 0, destinationCounter = 0;

    private bool flag = true;

    [HideInInspector] public GameObject selectedTargetObject; // holds info of the obj which was selected when state machine goes from target to destination state ..in order to give the info to game.
    #endregion
    #region public
    [HideInInspector] public bool backOfficeInitDone = false;
    public int gameNumber;
    public int LevelCounter = 0;
    public int completedLevels = 0;
    public bool[] totalLevels;
    public bool gameWasPaused;
    [HideInInspector]
    public bool LastCheck;
    public float switchWait = 10f;
    public bool isSpaceEnabled;
    public bool enablepause = true;
    public bool isSingleDestination = false;
    public bool DragnDrop = false;
    public bool ShowPausePanel = true;
    public bool autoExitGameplay;

    public GameObject GreenBox;
    public GameObject block;
    public GameObject LoadingScreen;
    public GameObject pausePanel;

    [HideInInspector]
    public float timeOut = 30.0f, timeOutTimer = 0.0f; // countr to check for screen time out func

    public static AccessibilityManager instance;
    #endregion

    #endregion

    #region  functions

    //populating the root
    void Start()
    {
        if (LoadingScreen)
            LoadingScreen.SetActive(true);

        LastCheck = true;
        //CanCheckInfo = true;
        ShowPausePanel = true;
        //ShowPausePanel = true;

        // Blocking all inputs from mouse
        if (block != null)
            block.SetActive(true);

        if (GreenBox != null)
            GreenBox.SetActive(true);


        PlayerPrefs.SetString("clickable", "false");
        PlayerPrefs.SetInt("Click", 1);

        instance = this;
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
        if (FreezeControlsHandler.Instance.isControllsFreezed)
            return;

        Debug.Log("Next");
        EventController.instance.CountScreenInteractionWithoutCheck();
        checkActivity = true;
    }

    public void swipeUp(int dummy)
    {
        if (FreezeControlsHandler.Instance.isControllsFreezed)
            return;

        Debug.Log("Next");
        checkActivity = true;
    }
    public void swipeDown()
    {
        if (FreezeControlsHandler.Instance.isControllsFreezed)
            return;

        Debug.Log("Prev");
        EventController.instance.CountScreenInteractionWithoutCheck();
        checkActivity = true;
    }
    public void select()
    {
        if (FreezeControlsHandler.Instance.isControllsFreezed)
            return;

        EventController.instance.CountScreenInteractionWithoutCheck();
        checkActivity = true;

    }
    public void unSelect()
    {
    }
    public void Close()
    {
        if (FreezeControlsHandler.Instance.isControllsFreezed)
            return;

        try
        {
           
        }
        catch (Exception ex)
        {
            Debug.LogError("Targetscript nor found: " + ex);
        }

        print("AccessibilityManager->Close() called");
        EventController.instance.CountScreenInteractionWithoutCheck();
    }

    // Variable to let info play while being controls are freezed
    bool isUrgentInfo = false;

    public void Info()
    {
        if (!isUrgentInfo && FreezeControlsHandler.Instance.isControllsFreezed)
            return;

        
        if(!isUrgentInfo)
            EventController.instance.CountScreenInteractionWithoutCheck();

        
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

        if (FreezeControlsHandler.Instance.isControllsFreezed)
            return;

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
        if (space && isSpaceEnabled)
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
        if (close && ShowPausePanel && LastCheck)
        {
            Debug.Log("AccessibilityManager->c pressed");
            Close();
            // GameManager.Instance.Stop();
            //  ToggleNaviagtion(false);
            //  pausePanel.SetActive(true);

            close = false;
        }

    }


    public void ScreenTimeoutNotifier()
    {
        //Debug.Log("AccessibilityManager->ScreenTimeoutNotifier() called: " + timeOutTimer);

        timeOutTimer += Time.deltaTime;
        // If screen is tapped, reset timer
        if (checkActivity)
        {
            timeOutTimer = 0.0f;
            checkActivity = false;
            //Dont active screensaver
        }
        // If timer reaches zero, start screensaver
        if (timeOutTimer > timeOut && LastCheck)
        {
            Debug.Log("AccessibilityManager->ScreenTimeoutNotifier() timeout");
            timeOutTimer = -2.0f;
        }
    }
    
    #endregion

}