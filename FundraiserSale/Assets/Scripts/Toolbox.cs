using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(SoundManager))]
public class Toolbox : MonoBehaviour
{

    private static GameManager _gameManager;
    private static SoundManager _soundManager;
    private static MenuManager _menuManager;
    private static GamePlayController _gameplayController;
    private static TextToSpeech _textToSpeech;
    private static External _externalHandler;

    public static GameManager GameManager
    {
        get { return _gameManager; }
    }

    public static TextToSpeech TextToSpeech
    {
        get { return _textToSpeech; }
    }
    public static SoundManager SoundManager
    {
        get { return _soundManager; }
    }
    public static MenuManager MenuManager
    {
        get { return _menuManager; }
    }
    public static GamePlayController GamePlayController
    {
        get { return _gameplayController; }
    }
    public static External ExternalHandler
    {
        get { return _externalHandler; }
    }
    void Awake()
    {
        _gameManager = GetComponent<GameManager>();
        _soundManager = GetComponent<SoundManager>();
        _menuManager = GetComponent<MenuManager>();
        _textToSpeech = GetComponent<TextToSpeech>();

        DontDestroyOnLoad(gameObject);
    }

    public static void Set_GameplayController(GamePlayController game)
    {
        _gameplayController = game;
    }
    public static void Set_ExternalHandler(External _external)
    {
        _externalHandler = _external;
    }

}
