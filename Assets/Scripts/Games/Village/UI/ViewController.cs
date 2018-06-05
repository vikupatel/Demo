using UnityEngine;
using System.Collections;

namespace Games.Village {
	public class ViewController :MonoBehaviour {

		public static ViewController instance;

		public UIView loadingView;
		public UIView inGameView;
		[HideInInspector] public UIView currentView;


		void  Awake()
		{
			instance = this;
		}

		void Start()
		{
			loadingView.Hide();
			inGameView.Hide ();
			ChangeView(inGameView);
		}

		public void ChangeView(UIView targetView) {
			if (currentView != null) currentView.Hide();
			currentView = targetView;
			currentView.Show();
		}





	}
}

