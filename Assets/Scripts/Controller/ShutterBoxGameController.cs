using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterBoxGameController : Singleton<ShutterBoxGameController> {

	ShutterBoxGameReferences shutterBoxGameRef;

	public bool ShouldAllowBoxMovement {
		get;
		set;
	}

	//Shows first game start screen
	public void ShowShutterBoxGameScreen(ShutterBoxGameReferences shutterBoxGameReferences)
	{
		shutterBoxGameRef = shutterBoxGameReferences;
		shutterBoxGameRef.gameObject.SetActive (true);
		ShouldAllowBoxMovement = true;
	}
	//Hide first game start screen
	public void HideShutterBoxScreen()
	{
		shutterBoxGameRef.gameObject.SetActive (false);
	}
}
