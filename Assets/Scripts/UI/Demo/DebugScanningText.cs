using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class DebugScanningText : MonoBehaviour {

	public Text status;


	void OnEnable() {
		OrbleTrackableEventHandler.OnAnyTrackableStateChanged += OnAnyTrackableStateChangedHandler;
		OrbleTrackableEventHandler.OnDeorbalize += OnDeorbalize;
	}

	void OnDisable() {
		OrbleTrackableEventHandler.OnAnyTrackableStateChanged -= OnAnyTrackableStateChangedHandler;
		OrbleTrackableEventHandler.OnDeorbalize -= OnDeorbalize;
	}




	public void OnAnyTrackableStateChangedHandler(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
		status.text = newStatus.ToString();
	}



	void OnDeorbalize (Vector3 screenSpace, CatalogOrble catalogOrble){
		status.text = "Orble found, catalog number: " + catalogOrble.catalogNumber;
	}
}
