using UnityEngine;
using System.Collections;

namespace Games.SwampFishing {
	public class ViewController :MonoBehaviour {

        //singletone instance
		public static ViewController instance;

		//Refrences to all the view
        public UIView loadingView;
		public UIView mainMenuView;
		public UIView inGameView;
		public UIView gameOverView;

		[HideInInspector] public UIView currentView;

        //spawn point
		public static Vector3 leftSpawnUpPoint;
		public static Vector3 leftSpawnDownPoint;
		public static Vector3 rightSpawnUpPoint;
		public static Vector3 rightSpawnDownPoint;

		void  Awake()
		{
			instance = this;
			leftSpawnUpPoint    =  Camera.main.ScreenToWorldPoint (new Vector2(0 - Screen.width * .2f,Screen.height - .45f * Screen.height));
			leftSpawnDownPoint  =  Camera.main.ScreenToWorldPoint (new Vector2(0 - Screen.width * .2f,.15f * Screen.height));
			rightSpawnUpPoint   =  Camera.main.ScreenToWorldPoint (new Vector2(Screen.width + Screen.width * .2f,Screen.height - .45f * Screen.height));
			rightSpawnDownPoint =  Camera.main.ScreenToWorldPoint (new Vector2(Screen.width + Screen.width * .2f,.15f * Screen.height));
		}

        //hides all the views and set current view
		void Start()
		{
			loadingView.Hide();
			mainMenuView.Hide();
			inGameView.Hide();
			gameOverView.Hide ();
            ChangeView(mainMenuView);
		}


        //shows the current view
		public void ChangeView(UIView targetView) {
			if (currentView != null) currentView.Hide();
			currentView = targetView;
			currentView.Show();
		}





	}
}

