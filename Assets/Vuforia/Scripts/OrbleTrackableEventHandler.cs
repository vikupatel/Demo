/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections;
using UnityEngine;
using DG.Tweening;
using Vuforia;





public class OrbleTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
	public delegate void OnAnyTrackableStateChangedHandler(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus);
	public static event OnAnyTrackableStateChangedHandler OnAnyTrackableStateChanged;

	public delegate void OnDeorbalizeHandler(Vector3 screenSpace, CatalogOrble catalogOrble);
	public static event OnDeorbalizeHandler OnDeorbalize;

	public Camera arCamera;
	public Renderer orbleSphere;
	public Transform orbleCenter;
	public ParticleSystem particles;

	private ARColorProbe colorProbe;
	private Coroutine deorbalizeCoroutine;
    private TrackableBehaviour mTrackableBehaviour;



	void Awake() {
		colorProbe = this.transform.GetComponentInChildren<ARColorProbe>();
	}


    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour) {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }

		if (OnAnyTrackableStateChanged != null) OnAnyTrackableStateChanged(previousStatus, newStatus);
    }



    private void OnTrackingFound()
    {
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

		if (deorbalizeCoroutine == null) {
			if (orbleSphere != null) orbleSphere.enabled = true;
			if (particles != null) particles.gameObject.SetActive(true);
			deorbalizeCoroutine = StartCoroutine(Deorbalize());
		}
			
		//activate color probe
		colorProbe.SetProbeActive(true);;
    }


    private void OnTrackingLost()
    {
		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

		if (orbleSphere != null) orbleSphere.enabled = false;

		if (particles != null) {
			particles.gameObject.SetActive(false);
			particles.Stop();
		}

		if (deorbalizeCoroutine != null) {
			StopCoroutine(deorbalizeCoroutine);
			deorbalizeCoroutine = null;
		}

		//deactivate color probe to avoid fake results
		colorProbe.SetProbeActive(false);;
    }





	IEnumerator Deorbalize() {
	
		if (particles != null)  particles.Play();
		yield return new WaitForSeconds(3f);

		//extend wait if color is not recognized yet
		while (!colorProbe.DidFoundCatalogOrble()) {
			//extend waiting
			Debug.Log("waiting for color recognition");
			yield return new WaitForEndOfFrame();
		}


		Vector3 screenSpacePosition = arCamera.WorldToScreenPoint(orbleCenter.position);

		Debug.Log("Deorbalized catalog number: " + colorProbe.CatalogOrble.catalogNumber);
		if (OnDeorbalize != null) {
			OnDeorbalize(screenSpacePosition, colorProbe.CatalogOrble);
		}
	}

}



