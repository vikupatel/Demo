using UnityEngine;
using System.Collections;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class ARColorProbeCamera : MonoBehaviour {


	void OnTriggerEnter(Collider other) {
		//check if probe
		if (other.tag == "ColorProbe") {
			other.transform.parent.parent.GetComponent<ARColorProbe>().insideArea = true;
			other.transform.parent.parent.GetComponent<ARColorProbe>().SetProbeActive(true);

		}

	}



	void OnTriggerExit(Collider other) {
		//check if probe
		if (other.tag == "ColorProbe") {
			//activate the probe 
			other.transform.parent.parent.GetComponent<ARColorProbe>().insideArea = false;
			other.transform.parent.parent.GetComponent<ARColorProbe>().SetProbeActive(false);

		}
	}


}
