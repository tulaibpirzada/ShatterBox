using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {

	public SpriteRenderer boxImage;
	private bool isForceApplied;
	private bool shouldAllowBoxDestruction;
	private bool checkDestroy;
	Coroutine lastRoutine;
	private enum BoxTypes {
		Simple = 0,
		Bomb = 1, 
		TimeFreeze = 2,
		TimeSlow = 3
	  }
	private BoxTypes boxType;
	// Use this for initialization
	void Start () {

		lastRoutine = null;
		ShutterBoxGameController.Instance.BoxCount++;
		isForceApplied = false;
//		shouldAllowBoxDestruction = true;
		if (ShutterBoxGameController.Instance.BoxCount % 10 == 0) {
			boxType = BoxTypes.Bomb;
			boxImage.sprite = Resources.Load ("bomb", typeof(Sprite)) as Sprite;
		} else {
			boxType = BoxTypes.Simple;
			boxImage.sprite = Resources.Load ("box", typeof(Sprite)) as Sprite;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (ShutterBoxGameController.Instance.ShouldAllowBoxMovement && !isForceApplied) {
//
//			if((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))){
//		if(!isForceApplied){
			ShutterBoxGameController.Instance.ShouldAllowBoxDestruction = true;
			Rigidbody2D rb = gameObject.GetComponent < Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.AddForce (transform.up * 700.0f);
			Debug.Log ("Force Added");
			isForceApplied = true;
			rb.gravityScale = 1;
			print ("Gravity" + Physics2D.gravity);
		}

				//When Touch is started
		if(((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0)))&& ShutterBoxGameController.Instance.ShouldAllowBoxDestruction){

				Debug.Log ("in touch");
					Vector3 worldPoint = Vector3.zero;
					#if UNITY_EDITOR
					worldPoint=Camera.main.ScreenToWorldPoint (Input.mousePosition);
					//for touch device
					#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
					worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
					#endif
			Debug.Log ("World Point: " + worldPoint);
					if(gameObject.GetComponent<Collider2D>().OverlapPoint(worldPoint)){
//						ShutterBoxGameController.Instance.IsBoxTouched = true;
						Debug.Log ("Box Touched");

				if (boxType == BoxTypes.Simple) {
					GameModel.Instance.Score++;
					ShutterBoxGameController.Instance.UpdateScore ();
					Destroy (gameObject);
					Debug.Log ("Destroyed");
				}
				if (boxType == BoxTypes.Bomb) {
					ShutterBoxGameController.Instance.IsBombDestroyed = true;
					Destroy (gameObject);
					
				}
					}
//					if (ShutterBoxGameController.Instance.IsBoxTouched) {
//						Destroy (gameObject.GetComponent<Collider2D>());
//						Debug.Log ("Box Touched");
//					}

//				}
			//}
		}

		if (ShutterBoxGameController.Instance.IsGamePaused) {

			ShutterBoxGameController.Instance.ShouldAllowBoxDestruction = false;
			Rigidbody2D rb = gameObject.GetComponent <Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
//			rb.isKinematic = false;
//			Physics2D.gravity = new Vector3 (0.0f, 0.0f, 0.0f);
		} 

		if (ShutterBoxGameController.Instance.ResumeGame) {
			
			ShutterBoxGameController.Instance.ShouldAllowBoxDestruction = true;
			Rigidbody2D rb = gameObject.GetComponent < Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.gravityScale = 1;
			print ("Gravity" + Physics2D.gravity);
			ShutterBoxGameController.Instance.ResumeGame = false;
			Debug.Log ("Resume Movement");
		}

		if(ShutterBoxGameController.Instance.IsGameOver) {
			Destroy(gameObject);
		}

		if(ShutterBoxGameController.Instance.IsBombDestroyed) {
			GameModel.Instance.Score++;
			ShutterBoxGameController.Instance.UpdateScore ();
//			Destroy(gameObject);
			lastRoutine=StartCoroutine(StopBoxDestruction(gameObject));
//			if (lastRoutine != null) {
//				StopCoroutine (StopBoxDestruction ());
//			}

		}
	}
	IEnumerator StopBoxDestruction(GameObject gameObject)
	{
		yield return new WaitForSeconds (0.5f);
		Destroy(gameObject);
		ShutterBoxGameController.Instance.IsBombDestroyed = false;
		Debug.Log ("Within bomb box destruction");
	}
}
