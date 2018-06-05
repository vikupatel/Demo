using UnityEngine;
using System.Collections;

public class TiltGravity : MonoBehaviour {

	public float sensitivity = 0.1f;

	void FixedUpdate () {

		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		dir.z = Input.acceleration.y;


		if (dir.sqrMagnitude > 1)
			dir.Normalize();


		Vector3 calibrated = Input.acceleration;

		Physics.gravity = new Vector3(dir.x, -0.5f, dir.z) * sensitivity;
		Debug.Log(dir);
	}
}
