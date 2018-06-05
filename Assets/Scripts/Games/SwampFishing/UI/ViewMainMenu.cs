using UnityEngine;
using System.Collections;

namespace Games.SwampFishing {
	public class ViewMainMenu : UIView {

		public static ViewMainMenu instance;

		public override void Awake ()
		{
			base.Awake ();
			instance = this;
		}

		public override void Show()
		{
			base.Show();

		}


		public override void Hide()
		{
			base.Hide();
		}


		public void StartButtonTapped(Level level)
		{
			//it will load the current level for the GameManager and reset the timer
            SwampFishingGameManager.existingInstance.LoadLevel (level);
			Timer.instance.ResetTimer (level.totalLevelTime);

            //hide the menuview and show the ingameView and start the game
            Games.SwampFishing.ViewInGame.instance.PopulateUIWhenLoad(level);
			Hide ();
			Games.SwampFishing.ViewController.instance.inGameView.Show ();
			SwampFishingGameManager.existingInstance.StartGame ();
		}

	}
}