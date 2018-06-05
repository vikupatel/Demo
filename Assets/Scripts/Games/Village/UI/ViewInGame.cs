using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Games.Village
{
	public class ViewInGame : UIView {

		public static ViewInGame instance;



		public override void Awake ()
		{
			base.Awake ();
			instance = this;
		}
		public override void Show() 
		{
			//enter any initializing code here		
			base.Show();
		}

		public override void Hide() 
		{
			//custom code here
			base.Hide();
		}

		public void SwampFishingButtonTapped()
		{			
			SceneManager.LoadScene (1);
		}

		public void DragonakSwoopButtonTapped()
		{			
			SceneManager.LoadScene (2);
		}
	


	}
}