using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelectionScreenEvents : MonoBehaviour {

	public void CrossButtonPressed()
	{
		GameController.Instance.BackToStartScreen ();
	}

}
