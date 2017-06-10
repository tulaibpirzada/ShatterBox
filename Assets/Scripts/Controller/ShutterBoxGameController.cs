using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterBoxGameController : Singleton<ShutterBoxGameController> {

	ShutterBoxGameReferences shutterBoxGameRef;
	public SpriteRenderer backgroundImage;

	public bool ShouldAllowBoxMovement {
		get;
		set;
	}

	public bool IsBoxTouched {
		get;
		set;
	}

	public bool IsGamePaused {
		get;
		set;
	}

	public bool ResumeGame {
		get;
		set;
	}

	public bool ResumeNewBoxMovement {
		get;
		set;
	}

	public bool IsGameOver {
		get;
		set;
	}

	public int BoxCount {
		get;
		set;
	}

	public bool IsBombDestroyed {
		get;
		set;
	}

	public bool ShouldAllowBoxDestruction {
		get;
		set;
	}

	public bool IsFreezePressed {
		get;
		set;
	}

	public bool IsSlowPressed {
		get;
		set;
	}
	Coroutine lastRoutine=null;
	Coroutine freezeRoutine;

	//Shows first game start screen
	public void ShowShutterBoxGameScreen(ShutterBoxGameReferences shutterBoxGameReferences)
	{
		shutterBoxGameRef = shutterBoxGameReferences;
		shutterBoxGameRef.gameObject.SetActive (true);
		shutterBoxGameRef.playerScoreLabel.gameObject.SetActive (true);
		shutterBoxGameRef.pauseButton.gameObject.SetActive (true);
		GameModel.Instance.SetUpGameVariables ();
		UpdateScore ();
		BoxCount = 0;
		IsGameOver = false;
		ShouldAllowBoxMovement = true;
		ShouldAllowBoxDestruction = false;
		IsBoxTouched = false;
		lastRoutine=StartCoroutine(SpawnBoxes());
	}
		
	public void HideShutterBoxScreen()
	{
		IsGameOver = true;
		shutterBoxGameRef.gameObject.SetActive (false);
		shutterBoxGameRef.pauseButton.gameObject.SetActive (false);
		shutterBoxGameRef.playerScoreLabel.gameObject.SetActive (false);
		if(lastRoutine != null)
			StopCoroutine (lastRoutine);
	}

	IEnumerator SpawnBoxes()
	{
		yield return new WaitForSeconds(2.0f);
		while (!IsGameOver) {
			GameObject box = shutterBoxGameRef.box;
			float []x_values = { -2.3f,-2.0f,-1.5f,-1.0f,-0.5f,0.0f,0.5f,1.0f,1.5f,2.0f, 2.3f };
			float x_value = x_values [Random.Range(0, x_values.Length)];
			Vector3 spawnPosition = new Vector3(
				x_value,
				-5.5f,
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate(box, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(Random.Range(1.0f, 1.5f));
		}
	} 

	public void UpdateScore ()
	{
		shutterBoxGameRef.playerScoreLabel.text = "Score: " + GameModel.Instance.Score.ToString();
	}

	public void GamePause ()
	{
		GameModel.Instance.PauseButtonCounter++;
		if (GameModel.Instance.PauseButtonCounter % 2 != 0) {
			ResumeNewBoxMovement = false;
			ResumeGame = false;
			IsGamePaused = true;
			if (lastRoutine != null) {
				StopCoroutine (lastRoutine);
				lastRoutine = null;
			}
			    
		} else {
			ResumeGame = true;
			ResumeNewBoxMovement = true;
			IsGamePaused = false;
			lastRoutine=StartCoroutine(SpawnBoxes());
		}

	}

	public void StopBoxSpawningWhileFreeze ()
	{
		if (lastRoutine != null) {
			StopCoroutine (lastRoutine);
			lastRoutine = null;
		}
	}

	public void ResumeBoxSpawning ()
	{
		lastRoutine=StartCoroutine(SpawnBoxes());
	}

	public void EndBoxFreeze ()
	{
		freezeRoutine = StartCoroutine (WaitAndResumeSpawning());
	}

	public IEnumerator WaitAndResumeSpawning ()
	{
		yield return new WaitForSeconds (2.0f);
		this.IsFreezePressed = false;
		ResumeBoxSpawning ();
	}
}
