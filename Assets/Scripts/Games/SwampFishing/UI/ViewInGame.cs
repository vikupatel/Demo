using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Games.SwampFishing 
{
	public class ViewInGame : UIView {

		public static ViewInGame instance;


        /// <summary>
        /// The text user interface
        /// </summary>
		public Text uiTargetScore;
		public Text uiCurrentScore;
		public Text uiLevelNumber;
		public Text uiTimeLeftText;

		public Transform Heart;//used to remove heart when collected bad item

        //UI sprites
		public Image pausePlayButtonImage; 
		public Sprite pauseImage;
		public Sprite playImage;
		bool paused=false;

        //base Awake is call
		public override void Awake ()
		{
			base.Awake ();
			instance = this;
		}

		public void OnEnable()
		{
			Timer.OnTimerChange += OnTimerChange;
		}

		public void OnDisable()
		{
			Timer.OnTimerChange -= OnTimerChange;
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

        /// <summary>
        /// Updates the current score.
        /// </summary>
        /// <param name="score">Score.</param>
		public void UpdateCurrentScore(int score)
		{
			uiCurrentScore.text = score.ToString();
		}

        /// <summary>
        /// Updates the target score.
        /// </summary>
        /// <param name="score">Score.</param>
		void UpdateTargetScore(int score)
		{
			uiTargetScore.text = score.ToString ();
		}

        /// <summary>
        /// Updates the target current score.
        /// </summary>
        /// <param name="score">Score.</param>
		void UpdateTargetCurrentScore(int score)
		{
			uiCurrentScore.text = score.ToString();
		}

        /// <summary>
        /// Updates the level number.
        /// </summary>
        /// <param name="score">Score.</param>
		void UpdateLevelNumber(int score)
		{
			uiLevelNumber.text = score.ToString();
		}

        /// <summary>
        /// Ons the timer change.(call by Timer class delegate)
        /// </summary>
        /// <param name="timeSpend">Time spend.</param>
        /// <param name="totalTime">Total time.</param>
		public void OnTimerChange(float timeSpend,float totalTime)
		{
			uiTimeLeftText.text=((int)(totalTime-timeSpend)).ToString();
		}

			/// <summary>
        /// Populate data when level is loaded(call by mainmenuView when game start)
		/// </summary>
		/// <param name="existingLevel">Existing level.</param>
		public void PopulateUIWhenLoad(Games.SwampFishing.Level existingLevel)
		{
			UpdateTargetScore (existingLevel.targetScore);
			UpdateLevelNumber (existingLevel.levelNumber);
			OnTimerChange (0, existingLevel.totalLevelTime);
		}

        //remove the heart
		public void RemoveHeart(int number)
		{
			Heart.transform.GetChild (number - 1).GetComponent<Image> ().enabled = false;
		}

        /// <summary>
        /// Pauses the button tapped ! this event call by user
        /// </summary>
		public void PauseButtonTapped()
		{
			//change the UI of pause button and change the Game state of Gamemanager
            paused = !paused;
			if (paused)
			{
				pausePlayButtonImage.sprite = playImage;
				SwampFishingGameManager.existingInstance.Pause ();
			}
			else
			{
				pausePlayButtonImage.sprite = pauseImage;
				SwampFishingGameManager.existingInstance.Resume ();
			}
			}


	}
}