using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft;

public class EventController : MonoBehaviour
{
	#region  variables
	public string gameTitle;
	public string gameSubTitle;
	// public GameManager Manager;
	public bool[] totalNoOfLevels;
	public int totalPossibleCorrectGuesses = 0;
//	[HideInInspector] 
	public double levelCounter = 0;
	Report report;
	GameStudentModel Model;
	//public string gameTitle;
	int gameTimeCounter = 0;
	public static EventController instance;
	private string POSTAddUserURL = "";
	[HideInInspector]
	public int correctOptionSelectionCounter = 0, wrongOptionSelectionCounter = 0;
	public bool reportSent = false;
	#endregion

	#region  function
	void Start()
	{
		instance = this;
		report = new Report();
		InitializeCommonEvents();
	}

	void InitializeCommonEvents()
	{
		StartCoroutine(Gametimer(true));

		setDateTime();
		SetGameTitle(gameTitle);
		SetGameSubTitle(gameSubTitle);
	}

	public void SetLevelCounter(int level = 0)
	{
		report.setPercentage(levelCounter);
	}
	
	public void SetGameType(string gameType = "None")
	{
		report.SetGameType(gameType);
	}

	public string GetGameType()
	{
		return report.GetGameType();
	}
	public void IncreaseCorrectAnswerBy(int increment)
	{
		Debug.Log("EventController->IncreaseCorrectAnswerBy(" + increment + ")");
		int CA = report.getCorrectAttemps();
		Debug.Log("CA: " + CA);
		CA += increment;
		report.setCorrectAttempts(CA);
	}

	public void IncreaseIncorrectAnswerBy(int increment)
	{

		int ICA = report.getIncorrectAttempts();
		ICA += increment;
		//Debug.Log("ICA: " + ICA);
		report.setIncorrectAttempts(ICA);
	}
	public void IncreaseLevelCounterBy(int increment)
	{
		levelCounter += increment;
	}

	public void SetGameTitle(string title)
	{
		report.setGametitle(title);
	}

	public void SetGameSubTitle(string subtitle)
	{
		report.setSubtitle(subtitle);
	}
	void setDateTime()
	{
		report.setDatetime(System.DateTime.UtcNow);
	}
	public void setSkillTag(string tag)
	{
		report.setAssociatedskilltag(tag);
	}
	public IEnumerator Gametimer(bool isRunning)
	{
		while (isRunning)
		{
			yield return new WaitForSeconds(1f);
			gameTimeCounter++;
			report.setPlaytime(gameTimeCounter); // in case if user quits in game and we also wanna show the played time
		}
		report.setPlaytime(gameTimeCounter);
	}
	public void StopTime()
	{
		StartCoroutine(Gametimer(false));
	}
    public void CountScreenInteraction()
    {
		
            if (Input.GetMouseButtonUp(0) && !Toolbox.GameManager.accessibilityCheck)
            {
                report.setResponsiveness(report.getResponsiveness() + 1);
            }
        
	}
	public void DecreaseCountForPlayPause()
	{
		if (!Toolbox.ExternalHandler.Preview) {

			report.setResponsiveness (report.getResponsiveness () - 1);
			Debug.Log ("--");
		}
	}
	public void CountScreenInteractionWithoutCheck()
	{
		if(!Toolbox.ExternalHandler.Preview)
			report.setResponsiveness(report.getResponsiveness() + 1);
	}

	public void SetResponsiveness(int increaseBy = 1)
	{
		if(!Toolbox.ExternalHandler.Preview)
			report.setResponsiveness(report.getResponsiveness() + increaseBy);
	}
	public void GameMode(string mode)
	{
		report.setSelectedmode(mode);
	}
	public void gameSubtitle(string sub)
	{
		report.setSubtitle(sub);
	}
	void istutorial(bool status)
	{
		report.isHastutorial(status);
	}
	
	public void PrintReport()
	{
		printReport();
	}

	public void SetGamePercentage(double levelCounter)
	{
		//levelCounter++;

		report.setPercentage(levelCounter);
	}

	public void currentGamePercentage()
	{

		//levelCounter++;
		if(reportSent)
            return;

		reportSent = true;

        if (Toolbox.GameManager.levelCounter > Toolbox.GameManager.gamePanels.Count)
        {
            Toolbox.GameManager.levelCounter = Toolbox.GameManager.gamePanels.Count;
        }
        levelCounter = Toolbox.GameManager.levelCounter;
        report.setPercentage(levelCounter);

		PrintReport();
	}
	public void GussedAnswer()
	{
		report.setCorrectAttempts(correctOptionSelectionCounter);
		report.setIncorrectAttempts(wrongOptionSelectionCounter);
	}
	
	void Update()
	{
		CountScreenInteraction();
	}
	
	public void printReport()
	{
		Debug.Log ("Uploading Report");

		report.reportsSentCount++;

		if(report.reportsSentCount == 1)
			report.setFirstPostDateTime(System.DateTime.UtcNow);
		
		PlayerGameScore playerGameScore = new PlayerGameScore ();
		playerGameScore.Complete =(int)report.getPercentage();
		playerGameScore.CreatedBy = (Toolbox.ExternalHandler.Model.GetGameId()).ToString();
		playerGameScore.CreatedDate = report.GetFirstPostDateTime ();
		playerGameScore.Duration = (int)report.getPlaytime ();
		playerGameScore.GameId = Toolbox.ExternalHandler.Model.GetGameId();
		playerGameScore.IncorrectAttempts = report.getIncorrectAttempts();
		playerGameScore.InstanceId = 0;
		playerGameScore.IsInAccessibilityMode = Toolbox.GameManager.accessibilityCheck;
		playerGameScore.ModifiedBy = (Toolbox.ExternalHandler.Model.GetGameId()).ToString();
		playerGameScore.ModifiedDate = System.DateTime.UtcNow;
		playerGameScore.PlayType = GetGameType();
		//playerGameScore.Responsiveness = report.getResponsiveness();//Previous Logic
		playerGameScore.Responsiveness = null;//Because Game_4 Fundraiser has No responsiveness
		playerGameScore.StudentId = Toolbox.ExternalHandler.Model.GetStudentId ();
		playerGameScore.Timestamp = report.getDatetime();


        //print("Percentage completed in game:" + playerGameScore.Complete);
        //print("Game Created By :" + playerGameScore.CreatedBy);
        //print("Game Created date :" + playerGameScore.CreatedDate);
        //print("Total time played is: " + playerGameScore.Duration);
        //print("Game Id is : " + playerGameScore.GameId);
        //print("Total Incorrect Attempts : " + playerGameScore.IncorrectAttempts);
        //print("Instance Id Of the game : " + playerGameScore.InstanceId);
        //print("InAccessibilityMode Of the game : " + playerGameScore.IsInAccessibilityMode);
        //print("Game Modified By : " + playerGameScore.ModifiedBy);
        //print("Game Modified Date : " + playerGameScore.ModifiedDate);
        //print("Game play type : " + playerGameScore.PlayType);
        //print("Total number of interaction in game (responsiveness):" + playerGameScore.Responsiveness);
        //print("Student Id in Game :" + playerGameScore.StudentId);
        //print("Date and time  is: " + playerGameScore.Timestamp);

        string JsonString = JsonConvert.SerializeObject(playerGameScore);
		Debug.Log (JsonString);

		string Url= Toolbox.ExternalHandler.baseURL + "api/PlayerScoreApi/SavePlayerScore";
		Debug.Log ("URl used for posting : "+Url);
		RestAPIHandler.Instance.StartCoroutine(RestAPIHandler.Instance.PostRequest(Url,JsonString));

	}


	#endregion
}
[Serializable]
public class PlayerGameScore
{
    public int Complete { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Duration { get; set; }
    public int GameId { get; set; }
    public int? IncorrectAttempts { get; set; }
    public int InstanceId { get; set; }
    public bool IsInAccessibilityMode { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string PlayType { get; set; }
    public int? Responsiveness { get; set; }
    public int StudentId { get; set; }
    public DateTime Timestamp { get; set; }

}
