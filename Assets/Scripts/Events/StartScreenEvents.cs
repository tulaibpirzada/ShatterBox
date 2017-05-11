using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenEvents : MonoBehaviour {

	public void PlayButtonTapped()
	{
		GameController.Instance.OpenShutterBoxScreen ();
	}

	public void ThemesButtonTapped()
	{
		GameController.Instance.OpenThemeSelectionScreen ();
	}

	public void OptionsButtonTapped()
	{
		GameController.Instance.OpenOptionsScreen ();
	}
}
