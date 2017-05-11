﻿using System.Collections;
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

	//Shows first game start screen
	public void ShowShutterBoxGameScreen(ShutterBoxGameReferences shutterBoxGameReferences)
	{
//		ResumeGame = true;
		shutterBoxGameRef = shutterBoxGameReferences;
//		if (GameModel.Instance.SelectedThemeChoice == 0) {
//			
//			backgroundImage.sprite = Resources.Load ("blueBackground", typeof(Sprite)) as Sprite;
//		} else if (GameModel.Instance.SelectedThemeChoice == 1) {
//
//			backgroundImage.sprite = Resources.Load ("greenBackground", typeof(Sprite)) as Sprite;
//		} else if (GameModel.Instance.SelectedThemeChoice == 2) {
//
//			backgroundImage.sprite = Resources.Load ("purpleBackground", typeof(Sprite)) as Sprite;
//		} else if (GameModel.Instance.SelectedThemeChoice == 3) {
//
//			backgroundImage.sprite = Resources.Load ("maroonBackground", typeof(Sprite)) as Sprite;
//		}
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

	//Hide first game start screen
	public void HideShutterBoxScreen()
	{
		IsGameOver = true;
		shutterBoxGameRef.gameObject.SetActive (false);
		shutterBoxGameRef.pauseButton.gameObject.SetActive (false);
		if(lastRoutine != null)
			StopCoroutine (lastRoutine);
	}

	IEnumerator SpawnBoxes()
	{
		yield return new WaitForSeconds(1.0f);
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
			Debug.Log ("Resume Game");
		}

	}

	public void StopBoxSpawningWhileFreeze ()
	{
		Debug.Log ("While Freeze");
		if (lastRoutine != null) {
			StopCoroutine (lastRoutine);
			lastRoutine = null;
		}
	}

	public void ResumeBoxSpawning ()
	{
		Debug.Log ("Again Spawning");
		lastRoutine=StartCoroutine(SpawnBoxes());
	}
}
