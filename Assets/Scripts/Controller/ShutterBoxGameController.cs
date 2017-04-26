using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterBoxGameController : Singleton<ShutterBoxGameController> {

	ShutterBoxGameReferences shutterBoxGameRef;

	public bool ShouldAllowBoxMovement {
		get;
		set;
	}
	public bool IsBoxTouched {
		get;
		set;
	}
	Coroutine lastRoutine=null;

	//Shows first game start screen
	public void ShowShutterBoxGameScreen(ShutterBoxGameReferences shutterBoxGameReferences)
	{
		shutterBoxGameRef = shutterBoxGameReferences;
		shutterBoxGameRef.gameObject.SetActive (true);
		shutterBoxGameRef.playerScoreLabel.gameObject.SetActive (true);
		GameModel.Instance.SetUpGameVariables ();
		UpdateScore ();
		ShouldAllowBoxMovement = true;
		IsBoxTouched = false;
		lastRoutine=StartCoroutine(SpawnBoxes());
	}
	//Hide first game start screen
	public void HideShutterBoxScreen()
	{
		shutterBoxGameRef.gameObject.SetActive (false);
		if(lastRoutine != null)
			StopCoroutine (lastRoutine);
	}
	IEnumerator SpawnBoxes()
	{
		yield return new WaitForSeconds(1.0f);
		while (true) {
			GameObject box = shutterBoxGameRef.box;
			float []x_values = { -2.3f,-2.0f,-1.5f,-1.0f,-0.5f,0.0f,0.5f,1.0f,1.5f,2.0f, 2.3f };
			float x_value = x_values [Random.Range(0, x_values.Length)];
			Vector3 spawnPosition = new Vector3(
				x_value,
				-5.5f,
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate(box, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(Random.Range(4.0f, 5.0f));
		}
	} 
	public void UpdateScore ()
	{
		shutterBoxGameRef.playerScoreLabel.text = "Score: " + GameModel.Instance.Score.ToString();
	}
}
