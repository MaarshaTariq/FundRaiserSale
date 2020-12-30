using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft;
using Newtonsoft.Json;

[Serializable]
public class Report
{
	#region variables
	private string gametitle;                   // Stores GameTitle
	private DateTime dateTime;                  // Stores the date and the time of the game, on start
	private DateTime firstReportSentDateTime;   // Stores the date and the time of firstreport sent

    private string associatedSkillTag;          // Stores the skills related to game
	private float playTime;                     // Total time the user has taken to complete the game
	private int responsiveness;                 // Total number of interations user performed with the game including both touching and dragging

	private double percentage;                  // Percentage of game the user has played in order to complete the game
	private string selectedMode;                // Which game mode user choosed to play.

	private string subtitle;                    // Stores subtitle of games
	private bool hasTutorial;
	private int CorrectAttempts;                // Number of correct Attempts by user
	private int IncorrectAttempts;              // Number of incorrect Attempts by user
    private string gameType = "";

	public int reportsSentCount = 0;

	private List<Keys> selectedKeys;            //Detailed reporting view of correct letter and incorrect letter typed per session in reporting view 

	#endregion
	#region GetterAndSetter

	public void SetGameType(string gameType)
	{
		this.gameType = gameType;
	}

	public string GetGameType()
	{
		return this.gameType;
	}
	public string getGametitle()
	{
		return this.gametitle;
	}

	public void setGametitle(string gametitle)
	{
		this.gametitle = gametitle;
	}

	public DateTime getDatetime()
	{
		return this.dateTime;
	}

	public void setDatetime(DateTime dateTime)
	{
		this.dateTime = dateTime;
	}

	public string getAssociatedskilltag()
	{
		return this.associatedSkillTag;
	}

	public void setAssociatedskilltag(string associatedSkillTag)
	{
		this.associatedSkillTag = associatedSkillTag;
	}

	public float getPlaytime()
	{
		return this.playTime;
	}

	public void setPlaytime(float playTime)
	{
		this.playTime = playTime;
	}

	public int getResponsiveness()
	{
		return this.responsiveness;
	}

	public void setResponsiveness(int responsiveness)
	{
		this.responsiveness = responsiveness;
		//Debug.Log("responsiveness: " + this.responsiveness);
	}

	public double getPercentage()
	{
		return this.percentage;
	}

	public void setPercentage(double currentLevel)
	{
        //this.percentage = (currentLevel/Toolbox.GameManager.gamePanels.Count)*100;//QA didnt want percentage
        this.percentage = currentLevel;
	}

	public string getSelectedmode()
	{
		return this.selectedMode;
	}

	public void setSelectedmode(string selectedMode)
	{
		this.selectedMode = selectedMode;
	}

	public string getSubtitle()
	{
		return this.subtitle;
	}

	public void setSubtitle(string subtitle)
	{
		this.subtitle = subtitle;
	}

	public void setFirstPostDateTime(DateTime firstReportSentDateTime)
	{
		this.firstReportSentDateTime = firstReportSentDateTime;
	}
	public DateTime GetFirstPostDateTime()
	{
		return this.firstReportSentDateTime;
	}


	public bool getHastutorial()
	{
		return this.hasTutorial;
	}

	public void isHastutorial(bool hasTutorial)
	{
		this.hasTutorial = hasTutorial;
	}

	public int getCorrectAttemps()
	{
		return this.CorrectAttempts;
	}

	public void setCorrectAttempts(int correctAtempts)
	{
		this.CorrectAttempts = correctAtempts;
	}

	public int getIncorrectAttempts()
	{
		return this.IncorrectAttempts;
	}

	public void setIncorrectAttempts(int IncorrectAttempts)
	{
		this.IncorrectAttempts = IncorrectAttempts;
	}

	public List<Keys> getSelectedkeys()
	{
		return this.selectedKeys;
	}

	public void setSelectedkeys(List<Keys> selectedKeys)
	{
		this.selectedKeys = selectedKeys;
	}
	#endregion
	#region function

	public void ResetReport()
	{
		gametitle = "";
		dateTime = System.DateTime.Now;
		associatedSkillTag = "";
		playTime = 0f;
		responsiveness = 0;
		percentage = 0f;
		selectedMode = "";
		subtitle = "";
		hasTutorial = false;
		CorrectAttempts = 0;
		IncorrectAttempts = 0;
		selectedKeys.Clear();

	}

	//   


	#endregion

	[Serializable]
	public class Keys
	{
		public string key;
		public int value;
	}


}
