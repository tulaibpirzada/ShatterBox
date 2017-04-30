using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel: Singleton <GameModel>
{
	private int bestScore;

	public int Score
	{
		get;
		set;
	}

	public int BestScore {
		get{ return bestScore;}
		set{bestScore = value;}
	}

	public void SetUpGameVariables()
	{
		this.Score = 0;
		if (PlayerPrefs.HasKey ("BestScore")) {
			bestScore = PlayerPrefs.GetInt ("BestScore");
		}  else {
			bestScore = 0;
		}
	}
}
