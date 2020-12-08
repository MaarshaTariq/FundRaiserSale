using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DelayToPlaySound : MonoBehaviour {
	
	public int AudioToPlay;
	public float delayBeforeInitialAudio = 1f;

	// Use this for initialization

	void OnEnable()
	{
			StartCoroutine (PlayInitialSound ());
	}
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator PlayInitialSound()
	{
		yield return new WaitForSeconds (delayBeforeInitialAudio);
        Toolbox.SoundManager.PlaySound(AudioToPlay);
	}
}
