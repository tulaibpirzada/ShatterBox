using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{

	public SpriteRenderer boxImage;
	private bool isForceApplied;
	private bool shouldAllowBoxDestruction;
	private bool checkDestroy;
	private bool isFreeze;
	Coroutine lastRoutine;
	Coroutine boxResumeRoutine;
	Coroutine slowRoutine;

	private enum BoxTypes
	{
		Simple = 0,
		Bomb = 1,
		TimeFreeze = 2,
		TimeSlow = 3
	}

	private BoxTypes boxType;
	// Use this for initialization
	void Start ()
	{

		lastRoutine = null;
		boxResumeRoutine = null;
		slowRoutine = null;
		ShutterBoxGameController.Instance.BoxCount++;
		isForceApplied = false;
		shouldAllowBoxDestruction = true;
		if (ShutterBoxGameController.Instance.BoxCount == 15) {
			boxType = BoxTypes.Bomb;
			string bombBoxImageName = GameModel.Instance.SelectedThemeChoice + "BoxBomb";
			boxImage.sprite = Resources.Load (bombBoxImageName, typeof(Sprite)) as Sprite;
			ShutterBoxGameController.Instance.BoxCount = 0;
		} else if (ShutterBoxGameController.Instance.BoxCount == 5) {
			boxType = BoxTypes.TimeFreeze;
			string freezeBoxImageName = GameModel.Instance.SelectedThemeChoice + "BoxFreeze";
			boxImage.sprite = Resources.Load (freezeBoxImageName, typeof(Sprite)) as Sprite;
		} else if (ShutterBoxGameController.Instance.BoxCount == 10) {
			boxType = BoxTypes.TimeSlow;
			string slowBoxImageName = GameModel.Instance.SelectedThemeChoice + "BoxSlow";
			boxImage.sprite = Resources.Load (slowBoxImageName, typeof(Sprite)) as Sprite;
		} else {
			boxType = BoxTypes.Simple;
			string simpleBoxImageName = GameModel.Instance.SelectedThemeChoice + "BoxSimple";
			boxImage.sprite = Resources.Load (simpleBoxImageName, typeof(Sprite)) as Sprite;
		}
		Debug.Log ("Box spawned count: " + ShutterBoxGameController.Instance.BoxCount.ToString ());
		Debug.Log ("Box Type: " + boxType.ToString ());
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (ShutterBoxGameController.Instance.ShouldAllowBoxMovement && !isForceApplied) {
			ShutterBoxGameController.Instance.ShouldAllowBoxDestruction = true;
			Rigidbody2D rb = gameObject.GetComponent < Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.AddForce (transform.up * 600.0f);
			isForceApplied = true;
			rb.gravityScale = 0.8f;
		}

		//When Touch is started
		if (((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) && ShutterBoxGameController.Instance.ShouldAllowBoxDestruction) {

			Vector3 worldPoint = Vector3.zero;
			#if UNITY_EDITOR
			worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//for touch device
			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
					worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			#endif
			if (gameObject.GetComponent<Collider2D> ().OverlapPoint (worldPoint)) {

				if (boxType == BoxTypes.Simple) {
					GameModel.Instance.Score++;
					ShutterBoxGameController.Instance.UpdateScore ();
					Destroy (gameObject);
				}
				if (boxType == BoxTypes.Bomb) {
					GameModel.Instance.Score++;
					ShutterBoxGameController.Instance.UpdateScore ();
					ShutterBoxGameController.Instance.IsBombDestroyed = true;
					Destroy (gameObject);
					
				}
				if (boxType == BoxTypes.TimeFreeze) {
					GameModel.Instance.Score++;
					ShutterBoxGameController.Instance.UpdateScore ();
					ShutterBoxGameController.Instance.IsFreezePressed = true;
					isFreeze = false;
					Destroy (gameObject);
				}
				if (boxType == BoxTypes.TimeSlow) {
					GameModel.Instance.Score++;
					ShutterBoxGameController.Instance.UpdateScore ();
					ShutterBoxGameController.Instance.IsSlowPressed = true;
					Destroy (gameObject);
				}
			}
		}

		if (ShutterBoxGameController.Instance.IsGamePaused) {

			ShutterBoxGameController.Instance.ShouldAllowBoxDestruction = false;
			Rigidbody2D rb = gameObject.GetComponent <Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
		} 

		if (ShutterBoxGameController.Instance.ResumeGame) {
			
			ShutterBoxGameController.Instance.ShouldAllowBoxDestruction = true;
			Rigidbody2D rb = gameObject.GetComponent < Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0.8f;
			print ("Gravity" + Physics2D.gravity);
			lastRoutine = StartCoroutine (BoxResume ());
		}

		if (ShutterBoxGameController.Instance.IsGameOver) {
			Destroy (gameObject);
		}

		if (ShutterBoxGameController.Instance.IsBombDestroyed) {
			lastRoutine = StartCoroutine (StopBoxDestruction (gameObject));
		}
		if (ShutterBoxGameController.Instance.IsFreezePressed && !isFreeze && boxType == BoxTypes.Simple) {
			Rigidbody2D rb = gameObject.GetComponent <Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
			isFreeze = true;
			ShutterBoxGameController.Instance.StopBoxSpawningWhileFreeze ();
			ShutterBoxGameController.Instance.EndBoxFreeze ();
		} else if (!ShutterBoxGameController.Instance.IsFreezePressed && isFreeze && boxType == BoxTypes.Simple) {
			Rigidbody2D rb = gameObject.GetComponent <Rigidbody2D> ();
			rb.gravityScale = 0.8f;
		}
			
		if (ShutterBoxGameController.Instance.IsSlowPressed) {
			Rigidbody2D rb = gameObject.GetComponent < Rigidbody2D> ();
			if (rb.velocity.y < 0) {
				rb.gravityScale = 0.2f;
				slowRoutine = StartCoroutine (StopSlowBoxMovement ());
			} else {
				
			}
		}
	}

	IEnumerator StopBoxDestruction (GameObject gameObject)
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
		ShutterBoxGameController.Instance.IsBombDestroyed = false;
	}

	IEnumerator BoxResume ()
	{
		yield return new WaitForSeconds (0.15f);
		ShutterBoxGameController.Instance.ResumeGame = false;
	}

	IEnumerator StopSlowBoxMovement ()
	{
		yield return new WaitForSeconds (3.0f);
		ShutterBoxGameController.Instance.IsSlowPressed = false;
	}

}
