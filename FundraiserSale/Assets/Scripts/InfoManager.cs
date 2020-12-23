using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public GameObject infoBox;
	public Text txt;
	public static InfoManager instance;

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (Toolbox.GameManager.accessibilityCheck)//Removing Close btn for Info box in Accessiblity mode.
        {
            infoBox.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ShowInfoBox()
	{
        infoBox.SetActive(true);
        StartCoroutine(Toolbox.SoundManager.playInitialAudio());
        //Play Sound of Drag correct stuff here
	}
    public void ShowAccessibilityInfoBox()
	{
        StartCoroutine(this.AccessibilityInfoBoxToggle());
    }

	public void CloseInfoBox()
	{
        infoBox.SetActive(false);
	}

    IEnumerator AccessibilityInfoBoxToggle()
    {
        infoBox.SetActive(true);
        yield return StartCoroutine(Toolbox.SoundManager.playInitialAudio());
        infoBox.SetActive(false);
    }
}