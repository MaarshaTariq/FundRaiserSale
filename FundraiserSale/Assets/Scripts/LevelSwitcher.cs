using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwitcher : MonoBehaviour
{

    private float temp;
    public float fillAmountNumber = 0;
    public AudioClip flaskFillinfSound;
    public Image flaskFiller;
    public float waitTime = 10f;
    public void OnEnable()
    {
        StartCoroutine(activePanelAndTransition());
    }
    private void Start()
    {
        fillAmountNumber = 0.125f;
        temp = fillAmountNumber;
    }
    public IEnumerator activePanelAndTransition()
    {
        yield return StartCoroutine(FillTheFlask());
        yield return new WaitForSeconds(2f);

        Toolbox.GameManager.ActivatingPanels();
        flaskFiller.fillAmount = 0;
    }

    public IEnumerator FillTheFlask()
    {
        temp = Toolbox.GameManager.levelCounter * fillAmountNumber;
        Toolbox.SoundManager.audioPlayer.clip = flaskFillinfSound;
        Toolbox.SoundManager.audioPlayer.Play();
        while (flaskFiller.fillAmount <= temp)
        {

            flaskFiller.fillAmount += fillAmountNumber / waitTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("TC" + Toolbox.GameManager.transitionCounter);

    }
}
