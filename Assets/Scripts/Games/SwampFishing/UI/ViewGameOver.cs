using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Games.SwampFishing
{
public class ViewGameOver : UIView
	{

	public Text uiBestScore;
	public Text uiCurrentScore;

	public static ViewGameOver instance;

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

		 void UpdateBestScore(int score)
		{
				uiBestScore.text = score.ToString ();
		}

		 void UpdateCurrentScore(int Score)
		{
				uiCurrentScore.text = Score.ToString ();
		}

		public void PopulateGameOverUI()
		{		
			
			Show ();
			ViewInGame.instance.Hide ();
			uiCurrentScore.text = SwampFishingGameManager.existingInstance.existingLevel.GetCurrentScore().ToString ();
		}

		public void RestartButtonTapped()
		{
			SceneManager.LoadScene("Game_SwampFishing");
		

		}

		public void HomeButtonTapped()
		{
			
		}

	}
}
