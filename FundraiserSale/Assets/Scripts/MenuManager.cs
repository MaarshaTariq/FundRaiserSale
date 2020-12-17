﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class MenuManager : MonoBehaviour {
	
	[DllImport("__Internal")]
	private static extern void _OnGameStopped();
	[DllImport("__Internal")]
	private static extern void _ExitFullScreen();

   
	public GameObject pauseScreen;
	
	public void OnPressPauseGame()
	{
		pauseScreen.SetActive (true);
        Time.timeScale = 0;
	}
	public void OnPressPlay()
	{
		pauseScreen.SetActive (false);
        Time.timeScale = 1;
	}
    public void OnPressStop()
    {
        //Check if Gameplay or Final
#if !UNITY_EDITOR
        _OnGameStopped();
#endif
    }
	
	public void OnPressAgain()
	{
		Time.timeScale = 1f;
		StartCoroutine(Toolbox.GameManager.LoadScene());
	}
	
	//public IEnumerator CallStop()
	//{
	//	yield return new WaitUntil(() => RestAPIHandler.Instance.UploadedSuccessfully==true);
	//	_OnGameStopped();
	//}

}
