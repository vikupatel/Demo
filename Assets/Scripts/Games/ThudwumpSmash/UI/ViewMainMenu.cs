using UnityEngine;
using System.Collections;

namespace Games.ThudwumpSmash{
	public class ViewMainMenu : UIView
	{

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
			GameManager.instance.LoadLevel (level);
			Hide ();
			ViewInGame.instance.Show ();
		}



	}
}