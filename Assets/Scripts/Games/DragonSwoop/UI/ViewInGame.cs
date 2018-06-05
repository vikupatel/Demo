using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Games.DragonakSwoop
{
	
	public class ViewInGame : UIView 
	{
		public Text uiTapToPlayText;
		public Text uiCurrentScore;
		public Text DistanceCoveredText;
		public static ViewInGame instance;
		public Image pausePlayButtonImage; 
		public Sprite pauseImage;
		public Sprite playImage;
		public Image pushButtonImage;
		public BoxCollider pushButtonCollider;
		bool paused=false;
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

		void Update()
		{			

		}

		public void PauseButtonTapped()
		{
			paused = !paused;
			if (paused)
			{
				pausePlayButtonImage.sprite = playImage;
				DragonakSwoopGameManager.instance.Pause ();
			}
			else
			{
				pausePlayButtonImage.sprite = pauseImage;
				DragonakSwoopGameManager.instance.Resume ();
			}
		}

		public void DisableTapToPlay()
		{
			
		}

		public void TapToPlayButton()
		{
			uiTapToPlayText.enabled = false;
			DragonakSwoopGameManager.instance.StartGame ();
		}

		public void SetDistanceCoveredUI(int distanceCover)
		{
		 
			DistanceCoveredText.text = string.Concat (distanceCover.ToString (), "m");
		}

		public void UpdateCurrentScore(int score)
		{
			uiCurrentScore.text = score.ToString();
		}


		public void ShootButtonTapped()
		{
			DragonakSwoopGameManager.instance.ShootFireBall ();	
		}

		public void DisableShootButton()
		{
			pushButtonCollider.enabled = false;
			pushButtonImage.enabled = false;
		}

		public void EnableShootButton()
		{
			pushButtonCollider.enabled = true;
			pushButtonImage.enabled = true;
		}

	
    }
}
