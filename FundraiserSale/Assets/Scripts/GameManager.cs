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
    public AudioClip endingPanelSound;

    public GameObject closeButton;
    public GameObject uiInteractions;
    public Sprite[] FullScreenIMG;
    public static FlaskFilling flaskFilling;
    public float fillAmountNumber = 0;
    public float temp;
    public float waitTime = 10f;
    public Image flaskFiller;
    public GameObject menuManager;
    public GameObject endingPanel;
    public GameObject transitionPanel;
    public GameObject infoHandler;
    public Image img;
    public int levelCounter;
    public int soundCounter;
    public int startingIndex = 0;
    public int index;
    public int endingIndex;
    private List<int> VisitedlevelHistory;
    public static GameManager gameManager;

    public GameObject[] gamePanels;
    public int gameSpeed;

    public bool isExternalDone = false;
    private bool accessibilty = false;

    public bool Accessibilty;

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
        endingIndex = gamePanels.Length - 1;
        //ActivatingPanels();
    }
    public void Update()
    {

    }
    public IEnumerator transitionActive()
    {
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
            EventController.instance.levelCounter++;
            gamePanels[index].SetActive(true);
            LevelFinish(index);
            VisitedlevelHistory.Add(index);
        }
        else
        {
            if (levelCounter <= 8 && VisitedlevelHistory.Count != 8)
            {

                ActivatingPanels();
            }
            else
            {
                endingPanel.SetActive(true);
                yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(endingPanelSound));

            }
        }
    }
    public void ActivatingPanels()
    {

        menuManager.SetActive(false);
        infoHandler.SetActive(true);
        if (levelCounter == 0)
        {
            //StartCoroutine( IntroActive());//Fix this in later build.
        }
        if (levelCounter <= 7)
        {
            SetProgress(levelCounter + 1);
        }
        StartCoroutine(randomizePanels(0f));
    }

   
    public IEnumerator SwitchPanels(int indexForLevel, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        for (int i = 0; i < gamePanels.Length; i++)
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
    public void deactiveCurrentPanel()
    {
        gamePanels[index].SetActive(false);
        // ActivatingPanels();
    }
    public IEnumerator FillTheFlask()
    {
        temp = levelCounter * fillAmountNumber;
        while (flaskFiller.fillAmount <= temp)
        {
            Toolbox.SoundManager.audioPlayer.clip = flaskFillinfSound;
            Toolbox.SoundManager.audioPlayer.Play();
            flaskFiller.fillAmount += fillAmountNumber / waitTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
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
            img.sprite = FullScreenIMG[0];
        }
        if (!Screen.fullScreen)
        {
            img.sprite = FullScreenIMG[1];
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