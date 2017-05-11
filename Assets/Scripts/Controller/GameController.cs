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
		StartScreenController.Instance.HideStartScreen ();
		ThemeSelectionScreenController.Instance.ShowThemeSelectionScreen (gameRef.themeSelectionScreenRef);
	}

	public void SetTheme(int buttonIndex)
	{
		IsThemeChanged = true;
		GameModel.Instance.SelectedThemeChoice = ShutterGameConstants.THEMES[buttonIndex];
		PlayerPrefs.SetString ("ThemeChoice", GameModel.Instance.SelectedThemeChoice);
		PlayerPrefs.Save ();
		ThemeSelectionScreenController.Instance.HideThemeSelectionScreen ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
//		ShutterBoxGameController.Instance.ShowShutterBoxGameScreen (gameRef.shutterBoxGameRef);
	}

	public void BackToStartScreen ()
	{
		ThemeSelectionScreenController.Instance.HideThemeSelectionScreen ();
		StartScreenController.Instance.ShowStartScreen (gameRef.startScreenRef);
	}
}
