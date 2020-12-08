using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

    public GameObject[] answers;
    public Transform[] answerTransforms;

    public AudioClip correctAudio;
    public AudioClip tryAgainAudio;

    [Header ("Different for each Panel")]
    public Image background;
    public Image option1;
    public Image option2;
    public Image option3Correct;
    public Image RecipeCard;
    public Image IngredientToMeasure;
    public Text infoText;
    private AudioClip option1Audio;
    private AudioClip option2Audio;
    private AudioClip option3CorrectAudio;
    private AudioClip instructionAudio;

    private Fade fade;

	public float levelEndDelay;
	public int soundIndex;
	public InfoManager infoMngr;

    

    void Awake()
    {
        Toolbox.Set_GameplayController(this.GetComponent<GamePlayController>());
        fade = this.GetComponent<Fade>();
        //this.GetComponent<Fade>().FadeIn = true;
    }
    // Use this for initialization
    void Start ()
    {
        infoMngr.SetSoundIndex (soundIndex);
        this.gameObject.SetActive(false);
		RandomizeAnswers ();
	}

    

    void RandomizeAnswers()
	{

        for (int i = 0; i < answerTransforms.Length; i++)
        {
            //Shuffling Answers list
            int rnd = Random.Range(0, answerTransforms.Length);
            Transform tempGO = answerTransforms[rnd];
            answerTransforms[rnd] = answerTransforms[i];
            answerTransforms[i] = tempGO;
            
        }

        if (answers.Length == answerTransforms.Length)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].gameObject.transform.localPosition = answerTransforms[i].gameObject.transform.localPosition;
            }

        }
        else
        {
            Debug.LogError("Answers and AnswersTransform Length doesnt matches.");

        }

        #region PreviousLogic
        //      int random;
        //for (int i = 0; i < 3 ;) {
        //	random = Random.Range (0, 3);
        //	if (CheckRandom (random)) {
        //		answers [i].gameObject.transform.localPosition = answerTransforms [random].gameObject.transform.localPosition;
        //		answers [i].transform.SetSiblingIndex (random);
        //		//Debug.Log ("Random " + random +" on "+i);
        //		randomCheck [random] = true;
        //		i++;
        //	}
        //}
        #endregion
    }

    
    public void ButtonClick(int ind)
    {
		
            switch (ind)
            {
                case 1://Incorrect option Selection
                StartCoroutine(InCorrectAnswer(ind));
                    break;

                case 2://Incorrect option Selection

                    StartCoroutine(InCorrectAnswer(ind));

                    break;

                case 3://Correct option Selection

                StartCoroutine(CorrectAnswer());

                break;

            default:
                    Debug.Log("The Selection is neither Correct nor Incorrect");
                    break;

            }
		
	}

    private IEnumerator CorrectAnswer()
    {
        answers[2].GetComponent<Image>().enabled = true;
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(option3CorrectAudio));//Ingredient or Measurement Audio
        yield return StartCoroutine(Toolbox.SoundManager._playSound(15));//Ding
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(correctAudio));//Correct

        ProgressLevel() ;
    }
    private IEnumerator InCorrectAnswer(int _option)
    {
        if (_option == 1)
        {
            yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(option1Audio));//Ingredient or Measurement Audio
        }
        else if (_option == 2)
        {
            yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(option2Audio));//Ingredient or Measurement Audio
        }

        yield return StartCoroutine(Toolbox.SoundManager._playSound(16));
        yield return StartCoroutine(Toolbox.SoundManager._playSoundWithAudioClip(tryAgainAudio));//Incorrect Try again
    }

    public void LoadPanel(GamePanels panel)
    {
        fade.FadeIn = true;
        answers[2].GetComponent<Image>().enabled = false;//Disabling the Glow.

        this.background.sprite = panel.background;
        this.option1.sprite = panel.option1;
        this.option1Audio = panel.option1Audio;
        this.option2.sprite = panel.option2;
        this.option2Audio = panel.option2Audio;
        this.option3Correct.sprite = panel.option3Correct;
        this.option3CorrectAudio = panel.option3Audio;
        //Alternating between Recipe Panel and Mixing Panel
        if (Toolbox.GameManager.gamePlayLevelCounter % 2 == 0)
        {
            this.RecipeCard.sprite = panel.RecipeOrIngredient;
            this.RecipeCard.gameObject.SetActive(true);
            this.IngredientToMeasure.gameObject.SetActive(false);
        }
        else
        {

            this.IngredientToMeasure.sprite = panel.RecipeOrIngredient;
            this.IngredientToMeasure.gameObject.SetActive(true);
            this.RecipeCard.gameObject.SetActive(false);

        }
        this.infoText.text = panel.infoText;
        this.instructionAudio = panel.instructionAudio;

        fade.gameObject.GetComponent<CanvasGroup>().alpha = 0;//Resetting to black for later Fading in.
        Toolbox.SoundManager.PlaySoundWithAudioClip(instructionAudio);

    }
    void ProgressLevel()
	{
        //Perform tasks here before loading next level

        StartCoroutine(Toolbox.GameManager.LoadNextLevel(levelEndDelay));//Sending Delay 0 because we ar eusing EndDelay before caling this function.
	}
    public AudioClip getInstructionAudio()
    {
        return instructionAudio;
    }
    public void OnDisable()
    {
        //this.GetComponent<Fade>().Fadeout = true;
    }
}
