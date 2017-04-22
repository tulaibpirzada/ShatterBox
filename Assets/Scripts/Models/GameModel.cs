using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel: Singleton <GameModel>
{
	public int Score
	{
		get;
		set;
	}

	public void SetUpGameVariables()
	{
		this.Score = 0;
	}
}
