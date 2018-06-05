using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Games.ThudwumpSmash
{
	public class ViewInGame : UIView {

		public static ViewInGame instance;
		public Text uiTimeNumber;

		public GameObject pausebutton;
		public override void Awake ()
		{
			base.Awake ();
			instance = this;
		}



		public override void Show() 
		{
			//enter any initializing code here		
			base.Show();
			// game should be start from here
			GameManager.instance.StartGame();
		}

		public override void Hide() 
		{
			//custom code here
			base.Hide();
		}

		void OnEnable()
		{
			TimeManager.instance.OnTimerChange += updateTimerUI;
		}

		void updateTimerUI(float time,float totalTime)
		{
			uiTimeNumber.text=((Mathf.CeilToInt(totalTime-time))).ToString();
		}

		void OnDisable()
		{
			TimeManager.instance.OnTimerChange -= updateTimerUI;
		}


	}
}