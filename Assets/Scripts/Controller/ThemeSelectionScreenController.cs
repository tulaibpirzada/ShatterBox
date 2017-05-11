using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelectionScreenController : Singleton<ThemeSelectionScreenController> {

	ThemeSelectionScreenReferences themeSelectionScreenRef;

	public void ShowThemeSelectionScreen(ThemeSelectionScreenReferences themeSelectionScreenReference)
	{
		themeSelectionScreenRef = themeSelectionScreenReference;
		themeSelectionScreenRef.gameObject.SetActive (true);
	}

	public void HideThemeSelectionScreen()
	{
		themeSelectionScreenRef.gameObject.SetActive (false);
	}
}
