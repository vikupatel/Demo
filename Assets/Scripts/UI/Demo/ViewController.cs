using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Demo {
	public class ViewController : MonoBehaviour {

		public static ViewController instance;


		public UIView viewWelcome;
		public UIView viewScanning;
		public UIView viewScanned;
		public UIView viewVillage;


		[HideInInspector] public UIView currentView;

		public UIView debugView;


		void Awake() {
			instance = this;
		}


		void Start()
		{
			viewWelcome.Hide();
			viewScanning.Hide();
			viewScanned.Hide();
			viewVillage.Hide();

			if (debugView != null) ChangeView(debugView);
			else ChangeView(viewWelcome);

		}



		void ChangeView(UIView targetView) {
			if (currentView != null) currentView.Hide();
			currentView = targetView;
			currentView.Show();
		}



		public void ToWelcomeView() {
			ChangeView(viewWelcome);
		}


		public void ToScanningView() {
			ChangeView(viewScanning);
		}

		public void ToScannedView() {
			ChangeView(viewScanned);
		}



		public void ToVillageView() {
			ChangeView(viewVillage);
		}
	}
}