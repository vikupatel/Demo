using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop {
	public class ViewController :MonoBehaviour {

		public static ViewController instance;

		public UIView loadingView;
		public UIView mainMenuView;
		public UIView inGameView;
		public UIView gameOverView;

		[HideInInspector] public UIView currentView;

		public static Vector2 playerSpawnPoint;
	  
		Vector2 leftScreenBound;
		Vector2 rightScreenBound;
		public static float distanceWithInScreen;  // distance between leftside and right side of screenEdge
		void  Awake()
		{
			instance = this;

			playerSpawnPoint  =  Camera.main.ScreenToWorldPoint (new Vector2(Screen.width * .2f,Screen.height - .45f * Screen.height));
			leftScreenBound = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			rightScreenBound=Camera.main.ViewportToWorldPoint(new Vector2(1.4f,0));
			distanceWithInScreen = Vector2.Distance (rightScreenBound,leftScreenBound);
		}

		void Start()
		{
			loadingView.Hide();
			mainMenuView.Hide();
			inGameView.Hide ();
			gameOverView.Hide ();
			ChangeView(mainMenuView);
		}

		public void ChangeView(UIView targetView) {
			if (currentView != null) currentView.Hide();
			currentView = targetView;
			currentView.Show();
		}





	}
}

