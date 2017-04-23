using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : Singleton<StartScreenController> {

	StartScreenReferences startSelectScreenRef;

	//Shows first game start screen
	public void ShowStartScreen(StartScreenReferences startScreenReferences)
	{
		startSelectScreenRef = startScreenReferences;
		startSelectScreenRef.gameObject.SetActive (true);
	}
	//Hide first game start screen
	public void HideStartScreen()
	{
		startSelectScreenRef.gameObject.SetActive (false);
	}
}
