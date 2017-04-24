using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {

	private bool isForceApplied;
	// Use this for initialization
	void Start () {

		isForceApplied = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (ShutterBoxGameController.Instance.ShouldAllowBoxMovement && !isForceApplied) {

			if((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))){
				Rigidbody2D rb = gameObject.GetComponent < Rigidbody2D> ();
				rb.bodyType = RigidbodyType2D.Dynamic;
				//				rb.isKinematic = false;
				rb.velocity = Vector2.zero;
				rb.AddForce (transform.up * 700.0f);
				Debug.Log ("Force Added");
				isForceApplied = true;
			}
		}
	}
}
