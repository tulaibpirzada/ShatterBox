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
		ShutterBoxGameController.Instance.HideShutterBoxScreen ();
		GameOverScreenController.Instance.ShowGameOverScreen (gameRef.gameOverScreenRef);
	}
}
