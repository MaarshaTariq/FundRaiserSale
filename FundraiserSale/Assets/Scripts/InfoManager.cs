using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public GameObject infoBox;
	public Text txt;
	public static InfoManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	public void ShowInfoBox()
	{
        infoBox.SetActive(true);
        //Play Sound of Drag correct stuff here
	}

	public void CloseInfoBox()
	{
        infoBox.SetActive(false);

	}
	
}