using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {

	private bool isForceApplied;
	private bool shouldAllowBoxDestruction;
	// Use this for initialization
	void Start () {

		isForceApplied = false;
		shouldAllowBoxDestruction = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (ShutterBoxGameController.Instance.ShouldAllowBoxMovement && !isForceApplied) {
//
//			if((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))){
//		if(!isForceApplied){
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
		if((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))){

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
				GameModel.Instance.Score++;
				ShutterBoxGameController.Instance.UpdateScore ();
				        Destroy (gameObject);
				        Debug.Log ("Destroyed");
					}
//					if (ShutterBoxGameController.Instance.IsBoxTouched) {
//						Destroy (gameObject.GetComponent<Collider2D>());
//						Debug.Log ("Box Touched");
//					}

//				}
			//}
		}

		if (ShutterBoxGameController.Instance.IsGamePaused) {

			Rigidbody2D rb = gameObject.GetComponent <Rigidbody2D> ();
			rb.bodyType = RigidbodyType2D.Dynamic;								
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
//			rb.isKinematic = false;
//			Physics2D.gravity = new Vector3 (0.0f, 0.0f, 0.0f);
		} 

		if (ShutterBoxGameController.Instance.ResumeGame) {

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
	}
}
