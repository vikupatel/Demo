using UnityEngine;
using System.Collections;

namespace Games.SwampFishing
{
	public enum GameState
	{
		inMenu,
		inGame,
		paused
	}
	public enum GameOverReason
	{
		gameOverTimeOut,
		badItemCollected,
		allLiveLost
	}

	public class SwampFishingGameManager :  MonoBehaviour 
	{
		//times start delegate and event
        public delegate void OnTimerStartDelegate();
		public static event OnTimerStartDelegate OnTimerStart;

        //Spawning event and delegate
		public delegate void OnSpawnCollectible();
		public static event OnSpawnCollectible SpawnCollectible;

        //state of the game
		public GameState gameState;

        //scriptable leve data object
		public  Games.SwampFishing.Level existingLevel;

        //singleton instance
		public  static SwampFishingGameManager existingInstance;

        //pause trigger variable
		public static bool gamePaused=false;


        //setting only one instance
		void Awake()
		{
			existingInstance = this;
		}



		void OnEnable()
		{
			Timer.OnTimeOut += TimeOut;
		}

		void OnDisable()
		{
			Timer.OnTimeOut -= TimeOut;
		}

        /// <summary>
        /// Start the game
        /// </summary>
		public  void StartGame ()
		{					
			gameState = GameState.inGame;

			if(OnTimerStart!=null)
			OnTimerStart ();

            //spawning started #collectibleSpawner
			if (SpawnCollectible != null)
				SpawnCollectible ();
		}

        //load the level number
		public  void LoadLevel (Level level) 
		{
			existingLevel = level;
		}

        //unload and reset the level
		public void UnloadCurrentLevel()
		{
			existingLevel = null;
			existingLevel.Reset ();
		}

        //game over event and state handling
		public  void GameOver (GameOverReason reason)
		{			
				if (reason == GameOverReason.gameOverTimeOut)
				{
				}
			    else if (reason == GameOverReason.allLiveLost)
				{
				}
				else if (reason == GameOverReason.badItemCollected)
			    {
		        }
				gameState = GameState.inMenu;
				ViewGameOver.instance.PopulateGameOverUI ();
				CollectibleSpawner.instance.RemoveFishes ();		  
		}

        //pause the game
		public void Pause ()
		{
			gameState = GameState.paused;
			Timer.instance.PauseTimer ();
		}

        //resume the game
		public void Resume ()
		{
			gameState = GameState.inGame;
			Timer.instance.ResumeTimer ();
		}

        //timeout and gameove invoke
		public void TimeOut()
		{
			GameOver (GameOverReason.gameOverTimeOut);
		}


	}
}