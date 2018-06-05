using UnityEngine;
using System.Collections;

namespace Games.ThudwumpSmash {
	public class ViewController :MonoBehaviour {

		public static ViewController instance;

		public UIView viewLoading;
		public UIView viewIngame;
		public UIView viewMainMenu;
		[HideInInspector] public UIView currentView;



		void  Awake()
		{
			instance = this;

		}

		void Start()
		{
			viewLoading.Hide();
			viewIngame.Hide ();
			viewMainMenu.Hide ();
			ChangeView(viewMainMenu);
		}

		public void ChangeView(UIView targetView) {
			if (currentView != null) currentView.Hide();
			currentView = targetView;
			currentView.Show();
		}





	}
}