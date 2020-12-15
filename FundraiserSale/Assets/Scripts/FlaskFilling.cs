using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskFilling : MonoBehaviour {
    /* public static FlaskFilling flaskFilling;
     public float fillAmountNumber;
     public Image flaskFiller;

     public void Start()
     {
         flaskFilling = this;
         fillAmountNumber = 0;
     }
     public void FillTheFlask()
     {
         if (PlayerPrefs.GetFloat("fillAmount") < 8)
         {
             fillAmountNumber += 0.125f;
             PlayerPrefs.SetFloat("fillAmount", fillAmountNumber);
             flaskFiller.fillAmount += fillAmountNumber;
         }
     }
     */
    public void Start()
    {
        StartCoroutine(transitionPanelDeactivate());
    }
    public void Update()
    {
        if (TriggerChceking.tg.transitionPanel.activeInHierarchy)
        {
            StartCoroutine(transitionPanelDeactivate());
        }
    }
    public IEnumerator transitionPanelDeactivate()
    {
        yield return new WaitForSeconds(5f);
        Toolbox.GameManager.ActivatingPanels();
        TriggerChceking.tg.transitionPanel.SetActive(false);
        Toolbox.GameManager.flaskFiller.fillAmount = 0;
        
    }
}
