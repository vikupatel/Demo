using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Games.DragonakSwoop
{
public class ViewGameOver : UIView {


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
		base.Show();
	}


	public override void Hide()
	{
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
			int currentScore = DragonakSwoopGameManager.instance.currentLevel.currentScore * DragonakSwoopGameManager.instance.currentLevel.distanceCovered;
			uiCurrentScore.text =currentScore.ToString ();
	}

	public void RestartButtonTapped()
	{			
			SceneManager.LoadScene("Game_DragonakSwoop");
	}

	public void QuitButtonTapped()
	{
			Application.Quit ();
	}


}
}
