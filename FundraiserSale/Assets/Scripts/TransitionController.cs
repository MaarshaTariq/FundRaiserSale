using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{

    private float temp;
    public float fillAmountNumber;
    public AudioClip flaskFillinfSound;
    public Image flaskFiller;
    private float waitTime ;
    public void OnEnable()
    {
        waitTime = flaskFillinfSound.length + 0.5f; 
        StartCoroutine(ActivePanelAndTransition());
    }
    private void Start()
    {
        temp = fillAmountNumber;
    }
    public IEnumerator ActivePanelAndTransition()
    {
        yield return StartCoroutine(FillTheFlask());
        flaskFiller.fillAmount = 0;
    }

    public IEnumerator FillTheFlask()
    {
        temp = Toolbox.GameManager.levelCounter * fillAmountNumber;
        Toolbox.SoundManager.audioPlayer.clip = flaskFillinfSound;
        Toolbox.SoundManager.audioPlayer.Play();
        while (flaskFiller.fillAmount <= temp)
        {

            flaskFiller.fillAmount += temp / waitTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);
        Toolbox.GameManager.ActivatingPanels();
    }
}
