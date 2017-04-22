using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour {

	void Start()
	{
		GameModel.Instance.SetUpGameVariables ();
		GameController.Instance.LoadGame (gameObject);
	}
}
