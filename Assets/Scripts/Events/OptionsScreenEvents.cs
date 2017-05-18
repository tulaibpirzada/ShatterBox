using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreenEvents : MonoBehaviour {

	public void ThemesButtonTapped()
	{
		GameController.Instance.OpenThemeSelectionScreen ();
	}
}
