using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class External : MonoBehaviour
{
    [HideInInspector]
    public GameObject assetDownloader;//Not Being used in Fundraiser.
    public static string urlFromServer;
    public bool Preview = false;
	public static External Instance;

	string baseURL;
	[HideInInspector]
	public string BaseUrl
	{
		get { return baseURL; }
		set
		{
			print("Updating baseURL: " + value);
			baseURL = value;
		}
	}
	[HideInInspector]
	public string KeyNew;
	public GameStudentModel Model;

	bool IsAccessibility;
	public bool AccessibilityLocalTest;
	public bool isAccessibilityLocalTest;

	void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void OnEnable()
	{
		Instance = this;
		#if UNITY_EDITOR //for testing purposes in editor
		Invoke("ManagerAccessibility", 0);
		#endif
	}

	void ManagerAccessibility()
	{
		Debug.Log("External->OnEnable()");
		if (AccessibilityLocalTest)
		{
			EnableAccessibilty("true");
		}
	}


    void Start()
    {

        // GetUrlFromServer("https://l3skills.n2y-dev.com/api/GameAssetApi/GetGameAssets?gameId=72");
    }
    public void DisableFullScreen()
    {
        PlayerPrefs.SetInt("DisableFullScreen", 1);
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
        BaseUrl = uRL;
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
      //  Toolbox.GameManager.Accessibilty = ISAccessibilty;

		//Toolbox.GameManager.AccessibiltyObject.SetActive(ISAccessibilty);

		//Toolbox.GameManager.isExternalDone = true;
	}

	//bool isAccesibilityAssigned;
	public void EnableAccessibilty(string newValue)
	{
		// if (Toolbox.GameManager.AccessibiltyObject == null)
		// {
		// 	if (Toolbox.GameManager.AccessibiltyObject == null)
		// 	{
		// 		Debug.LogError("Accessibility is not implemented in this game. Please contact your provider.");
		// 		return;
		// 	}
		// }
		// PlayerPrefs.SetString ("Accessibility", newValue);
        // Toolbox.GameManager.gameObject.SetActive(true);
		// StartCoroutine(WaitToPerformFunctionality(newValue == "true"));
	}

	public void SetKey(string NewKey)//for local functionality
	{
		KeyNew = NewKey;
		Debug.Log("External->WaitToPerformFunctionality(NewKey: " + NewKey + ")");

      //  Toolbox.GameManager.Accessibilty = IsAccessibility;
       // Toolbox.GameManager.AccessibiltyObject.SetActive(IsAccessibility);

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

