using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour {
    //TriggerChceking tg;
  //  public GameObject transitionPanel;
   public static LevelSwitcher levelSwitcher;
    public void Start()
    {
        levelSwitcher = this;
        StartCoroutine(activePanelAndTransition());
        //  tg = new TriggerChceking();

    }
    public void OnEnable()
    {
        StartCoroutine(activePanelAndTransition());
    }

    public void Update()
    {
       // StartCoroutine(activePanelAndTransition());
        //  StartCoroutine(TriggerChceking.tg .allCheckmarksActivation());
        //StartCoroutine(TriggerChceking.tg.deActivateTransitionPanels());
    }
    public IEnumerator activePanelAndTransition()
    {
       
        yield return StartCoroutine(Toolbox.GameManager.FillTheFlask());
        yield return new WaitForSeconds(2f);
       // yield return new WaitForSeconds(2f);
        Toolbox.GameManager.ActivatingPanels();
        TriggerChceking.tg.transitionPanel.SetActive(false);
        //Debug.Log("mmmmmmmmddddm");
        Toolbox.GameManager.flaskFiller.fillAmount = 0;

    }
}
