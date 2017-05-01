using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterBoxGameEvents : MonoBehaviour {

	public void PauseButtonPressed()
	{
		ShutterBoxGameController.Instance.GamePause ();
	}
}
