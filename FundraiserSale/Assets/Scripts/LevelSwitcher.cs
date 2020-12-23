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

    public void Update()
    {
        StartCoroutine(activePanelAndTransition());
        //  StartCoroutine(TriggerChceking.tg .allCheckmarksActivation());
        //StartCoroutine(TriggerChceking.tg.deActivateTransitionPanels());
    }
    public IEnumerator activePanelAndTransition()
    {
       
        StartCoroutine(Toolbox.GameManager.FillTheFlask());
        yield return new WaitForSeconds(0.5f);

    }
}
