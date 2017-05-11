using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelectionScreenEvents : MonoBehaviour {

	public void BackgroundButtonPressed(int buttonIndex)
	{
		GameController.Instance.SetTheme(buttonIndex);
	}

	public void CrossButtonPressed()
	{
		GameController.Instance.BackToStartScreen ();
	}

}
