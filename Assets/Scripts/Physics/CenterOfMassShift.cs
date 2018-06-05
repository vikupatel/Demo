using UnityEngine;
using System.Collections;

public class CenterOfMassShift : MonoBehaviour {

	public Vector3 com;
	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = com;
	}
}
