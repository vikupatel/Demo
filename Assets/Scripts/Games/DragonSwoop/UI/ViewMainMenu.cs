using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
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
			
			DragonakSwoopGameManager.instance.LoadLevel(level);
			DragonakSwoopGameManager.instance.currentLevel.CreateTerrain();
			DragonakSwoopGameManager.instance.currentLevel.CreatePlayer();
			//some code to instantiate player levelwise
			DragonakSwoopGameManager.instance.gameState=GameState.tapToPlay;
			Games.DragonakSwoop.ViewInGame.instance.Show();
			Hide ();
		}

	}

}
