using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour {
    //TriggerChceking tg;
    public void Start()
    {
      //  tg = new TriggerChceking();
       
    }

    public void Update()
    {
        if (TriggerChceking.tg.flaskfill == true)
        {
            //GameManager.instance.fillAmountNumber += PlayerPrefs.GetFloat("fillAmount") + 0.125f;

            StartCoroutine(GameManager.instance.FillTheFlask());
        }
      //  StartCoroutine(TriggerChceking.tg .allCheckmarksActivation());
        //StartCoroutine(TriggerChceking.tg.deActivateTransitionPanels());
    }
}
