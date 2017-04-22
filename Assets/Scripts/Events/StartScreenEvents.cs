using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenEvents : MonoBehaviour {

	public void PlayButtonTapped()
	{
		GameController.Instance.OpenShutterBoxScreen ();
	}
}
