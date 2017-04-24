using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyer : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Box") 
		{
			Destroy (collider.gameObject);
		}
	}
}
