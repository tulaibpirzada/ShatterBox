//Main Controller of all the games
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController> {

	GameReferences gameRef;
	GameObject gameContextObject;


	public bool IsUIOpened {
		get;
		set;
	}

	public bool IsThemeChanged {
		get;
		set;
	}
	//Loads the game
	public void LoadGame(GameObject gameObject)
	{
		gameContextObject = gameObject;
		IsUIOpened = false;
		gameRef = gameContextObject.GetComponent<GameReferences> ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
	}

	public void OpenShutterBoxScreen ()
	{
		StartScreenController.Instance.HideStartScreen ();
		ShutterBoxGameController.Instance.ShowShutterBoxGameScreen (gameRef.shutterBoxGameRef);
	}

	public void OpenGameOverScreen ()
	{
		if (GameModel.Instance.Score > GameModel.Instance.BestScore) {
			GameModel.Instance.BestScore = GameModel.Instance.Score;
			PlayerPrefs.SetInt ("BestScore", GameModel.Instance.BestScore);
		}
		ShutterBoxGameController.Instance.HideShutterBoxScreen ();
		GameOverScreenController.Instance.ShowGameOverScreen (gameRef.gameOverScreenRef);
	}

	public void ReloadShutterBoxGameScreen ()
	{
		GameOverScreenController.Instance.HideGameOverScreen ();
		ShutterBoxGameController.Instance.ShowShutterBoxGameScreen (gameRef.shutterBoxGameRef);
	}

	public void OpenThemeSelectionScreen ()
	{
		OptionsScreenController.Instance.HideOptionScreen ();
		ThemeSelectionScreenController.Instance.ShowThemeSelectionScreen (gameRef.themeSelectionScreenRef);
	}

	public void SetTheme(string selectedChoice)
	{
		IsThemeChanged = true;
		ThemeSelectionScreenController.Instance.LoadTheme (selectedChoice);
		ThemeSelectionScreenController.Instance.HideThemeSelectionScreen ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
	}

	public void BackToStartScreen ()
	{
		ThemeSelectionScreenController.Instance.HideThemeSelectionScreen ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
	}

	public void OpenOptionsScreen ()
	{
		StartScreenController.Instance.HideStartScreen ();
		OptionsScreenController.Instance.ShowOptionScreen (gameRef.optionsScreenRef);
	}

	public void BackToStartScreenFromOptionScreen ()
	{
		OptionsScreenController.Instance.HideOptionScreen ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
	}

	public void BackFromGameOverScreenToStartScreen ()
	{
		GameOverScreenController.Instance.HideGameOverScreen ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
	}

}
