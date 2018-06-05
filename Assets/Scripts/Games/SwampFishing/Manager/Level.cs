using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Games.SwampFishing
{
	[System.Serializable]
	public struct Fish
	{
		public GameObject fishName;
		public float   occurancePossibility;
	};

	public class Level : ScriptableObject
	{
		public delegate void OnScoreChangeDelegate(int score);
		public static event OnScoreChangeDelegate OnScoreChange;

		/// <summary>
		/// collection of collectibles
		/// </summary>
		public Fish []fishes;  
		public int levelNumber=0;
		/// <summary>
		/// total gameplay time for specific level
		/// </summary>
		public int totalLevelTime;
		public int targetScore;
		int currentScore;//current score
		List <GameObject> collectedItems= new List<GameObject>();

		private  int liveLost=0;
		public int totalLive=3;
		public int totalTimerOccurance=3;   //total timer collectible to occure during gameplay

		public bool IsTreasureAvailable=false;
		public int levelScoreMultiplier = 1;   // change level points specified for each level

		public float levelSpeedIncreaser=1;
		void OnEnable()
		{  
			Reset ();

		}

		void OnDisable()
		{
			
		}

		public int GetCurrentScore()
		{
			return currentScore;
		}
		public void Restart()
		{
			//reset remaining time 
			Timer.instance.ResetTimer(totalLevelTime);
		}

		public void AddCollectedItem(GameObject item)
		{
			collectedItems.Add (item);
		}

		public void UpdateScore(int score)
		{
			currentScore += score*levelScoreMultiplier;
			ViewInGame.instance.UpdateCurrentScore (currentScore);
		}

		public void Reset()
		{
			currentScore = 0;
			liveLost = 0;
		}

		public void LiveLost()
		{
			liveLost++;
			ViewInGame.instance.RemoveHeart (liveLost);
		}

		public int GetLiveLost()
		{
			return liveLost;
		}




	
   }

}
