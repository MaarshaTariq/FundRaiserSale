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
	public void ShowInfoBox()
	{
        infoBox.SetActive(true);
        StartCoroutine(Toolbox.SoundManager.playInitialAudio());
        //Play Sound of Drag correct stuff here
	}

	public void CloseInfoBox()
	{
        infoBox.SetActive(false);

	}
	
}