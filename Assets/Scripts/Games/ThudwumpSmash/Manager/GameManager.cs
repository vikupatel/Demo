using UnityEngine;
using System.Collections;

namespace Games.ThudwumpSmash
{

	public class GameManager :  MonoBehaviour 
	{	

		public  static GameManager instance;
		public GameState gameState = GameState.inMenu;
		public Games.ThudwumpSmash.Level currentLevel;
		GameObject currentRecycleItem;

		void Awake()
		{
			instance = this;
		}

		void OnEnable()
		{
			
		}

		void OnDisable()
		{
			
		}

		public  void StartGame ()
		{					
			TimeManager.instance.ResetTimer (currentLevel.totalTime);
			TimeManager.instance.StartTimer ();
			GameManager.instance.gameState = GameState.inGame;
			currentLevel.LaunchItem ();
		}

		public  void LoadLevel (Level level) 
		{
			currentLevel = level;
		}

		public void UnloadCurrentLevel(Level level)
		{
			currentLevel = level;
		}

		public  void GameOver ()
		{			
				  
		}
		public void Pause ()
		{
			GameManager.instance.gameState = GameState.paused;
		}

		public void Resume ()
		{
			GameManager.instance.gameState = GameState.inGame;
		}

		public void TimeOut()
		{
			
		}

		void SetRecycleItem(GameObject item)
		{
			if (currentRecycleItem != null) 
			{
				Debug.Log ("one item is already exist");
			}
			currentRecycleItem = item;
		}


	}
}