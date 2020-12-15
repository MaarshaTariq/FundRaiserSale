using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public GameObject infoBox;
	public GameObject closeBlock;
	public Text txt;
	public static InfoManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	public void InfoHandler()
	{
			StartCoroutine (OpenCloseInfo ());	
	}
	IEnumerator OpenCloseInfo()
	{
		if (!infoBox.activeInHierarchy) {
			infoBox.SetActive (true);
			closeBlock.SetActive (true);
            //PlayAudioClip of instructions here.

		} else {
			yield return new WaitForSeconds (0.6f);
			infoBox.SetActive (false);
			closeBlock.SetActive (false);
		}
	}
}