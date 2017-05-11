using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreenController : Singleton<OptionsScreenController> {

	OptionsScreenReferences optionsScreenRef;

	public void ShowOptionScreen(OptionsScreenReferences optionsScreenReference)
	{
		optionsScreenRef = optionsScreenReference;
		optionsScreenRef.gameObject.SetActive (true);
	}

	public void HideOptionScreen()
	{
		optionsScreenRef.gameObject.SetActive (false);
	}
}
