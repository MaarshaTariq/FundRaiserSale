using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenBtn : MonoBehaviour {
    // Use this for initialization
    [DllImport("__Internal")]
    private static extern void _OnGameStopped();
    [DllImport("__Internal")]
    private static extern void _ExitFullScreen();

    public Sprite []fullScreenImg;
    public void MaximizeButtonPressed()
    {

        if (Screen.fullScreen)
        {
#if !UNITY_EDITOR
            _ExitFullScreen();
#endif
            Screen.fullScreen = false;
            GetComponent<Image>().sprite= fullScreenImg[0];
        }
        if (!Screen.fullScreen)
        {
            GetComponent<Image>().sprite = fullScreenImg[1  ];
            Screen.fullScreen = true;
        }
    }
}
