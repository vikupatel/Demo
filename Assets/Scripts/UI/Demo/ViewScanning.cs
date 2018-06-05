using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
using DG.Tweening;



namespace Demo {
	public class ViewScanning : UIView {

		public GameObject arCameraRoot;
		public Camera arCamera;
		public ParticleSystem screenFillParticles;
		public UIOrbleStage orbleStage;
		public CanvasGroup guidesGroup;



		public override void Show() {
			base.Show();

			arCameraRoot.SetActive(true);
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);


			//fade in guides
			guidesGroup.DOFade(1f, 1f).SetDelay(2f);



			OrbleTrackableEventHandler.OnDeorbalize += OnDeorbalize;
			OrbleTrackableEventHandler.OnAnyTrackableStateChanged += OnAnyTrackableStateChanged;
		}


		public override void Hide() {
			base.Hide();

			guidesGroup.alpha = 0;
			OrbleTrackableEventHandler.OnDeorbalize -= OnDeorbalize;
			OrbleTrackableEventHandler.OnAnyTrackableStateChanged -= OnAnyTrackableStateChanged;
		}




		public void OnAnyTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				//found some trackable
				//fade out guides
				guidesGroup.DOFade(0, 0.6f);
			}
			else
			{
				//lost
				guidesGroup.DOFade(1f, 0.6f);
			}
				
		}



		void OnDeorbalize(Vector3 screenPosition, CatalogOrble catalogOrble) {
			
			orbleStage.PresentOrble(screenPosition, StagePresented);
		}
	



		void StagePresented() {
			//move to next view
			ViewController.instance.ToScannedView();


		}


		public void BackbuttonTapped() {
			ViewController.instance.ToWelcomeView();
			arCameraRoot.SetActive(false);
		}

	}
}