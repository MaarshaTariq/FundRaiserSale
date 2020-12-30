using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;

public class External : MonoBehaviour
{
    [HideInInspector]
    public GameObject assetDownloader;//Not Being used in Fundraiser.
    public static string urlFromServer;
    public bool Preview = false;

    public GameObject accessibilityInputBlocker;
    public GameObject fullscreenBtn;


    [HideInInspector]
    public string baseURL =" ";
    [HideInInspector]
	public string KeyNew;
	public GameStudentModel Model;

	bool IsAccessibility;
	public bool AccessibilityLocalTest;

	void Awake()
	{
        Toolbox.Set_ExternalHandler(this);
        EnableAccessibilty(PlayerPrefs.GetString("Accessibility"));
	}
	// Use this for initialization
	void OnEnable()
	{
		#if UNITY_EDITOR //for testing purposes in editor
		Invoke("ManagerAccessibility", 0);
		#endif
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            EnableAccessibilty("true");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EventController.instance != null && !Toolbox.ExternalHandler.Preview)
                EventController.instance.currentGamePercentage();
        }
    }
    void ManagerAccessibility()
	{
		//Debug.Log("External->OnEnable()");
		if (AccessibilityLocalTest)
		{
			EnableAccessibilty("true");
		}
	}


    void Start()
    {

    }
    public void DisableFullScreen()
    {
        Destroy(fullscreenBtn);
    }

    public void PlayUnityScene()
    {
        Toolbox.GameManager.StartCoroutine(Toolbox.GameManager.LoadScene());

    }
    public void GetUrlFromServer(string url)
    {
        Debug.Log("The given url is :  " + url);
        urlFromServer = url;
        //StartCoroutine(assetDownloader.GetComponent<SoundDownloader> ().GetJsonData());
    }

	IEnumerator GetManagerAfter(float delay)
	{
		yield return new WaitForSeconds(delay);
		print("On Level was loaded worked");
		EnableAccessibilty("true");
	}
    public void GetBaseUrl(string uRL)
    {
        Debug.Log("The Base Url from server is : " + uRL);
        baseURL = uRL;
    }


	public void GetGameAndStudentForScoring(string Json)
	{
		Model = new GameStudentModel();
		Debug.Log("The Json from server is : " + Json);

        if (Json.Contains("\"StudentId\":null"))
		{
			string output = Json.Replace("\"StudentId\":null", "\"StudentId\":0");
			Debug.Log("OutPut changed because student ID is Null : " + output);
			Model = JsonConvert.DeserializeObject<GameStudentModel>(output);
		}
		else
		{
			Model = JsonConvert.DeserializeObject<GameStudentModel>(Json);
		}

        Preview = Model.GetPreviewBool();

        Debug.Log("Game ID : " + Model.GetGameId());
		Debug.Log("Student ID : " + Model.GetStudentId());
		Debug.Log("IsPreview : " + Model.GetPreviewBool());
    }


	IEnumerator WaitToPerformFunctionality(bool ISAccessibilty)
	{
		yield return new WaitForSeconds(0.15f);

        Toolbox.GameManager.accessibilityCheck = ISAccessibilty;
		Toolbox.GameManager.isExternalDone = true;
	}

	public void EnableAccessibilty(string newValue)
	{
        if (newValue.Contains("true"))
        {
            PlayerPrefs.SetString("Accessibility","true");
            accessibilityInputBlocker.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("Accessibility","false");
            accessibilityInputBlocker.gameObject.SetActive(false);
        }

        StartCoroutine(WaitToPerformFunctionality(PlayerPrefs.GetString("Accessibility") == "true"));
    }

	public void SetKey(string NewKey)//for local functionality
	{
		KeyNew = NewKey;
		Debug.Log("New Key Added : " + NewKey);

	}
}

[Serializable]
public class GameStudentModel
{
    public int GameId;
    public int StudentId;
    public bool IsPreview;

    public int GetGameId()
    {
        return this.GameId;
    }

    public int GetStudentId()
    {
        return this.StudentId;
    }

    public bool GetPreviewBool()
    {
        return this.IsPreview;
    }

    public void SetPreviewBool(bool isPre)
    {
        this.IsPreview = isPre;
    }

    public void SetGameId(int ID)
    {
        this.GameId = ID;
    }

    public void SetStudentId(int StID)
    {
        this.StudentId = StID;
    }
	

}

