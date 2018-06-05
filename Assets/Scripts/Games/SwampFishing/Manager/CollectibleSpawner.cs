using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Games.SwampFishing
{
	public class CollectibleSpawner : MonoBehaviour
	{
		//spawn positions
        public Transform SpawnPool;

		//container for all spawned objects
		public GameObject[] collectibleObjects;
		public GameObject timer;
		
        //specialItem(clock) to spawn
		public GameObject treasure;

        //palyer object
		public GameObject player;
		//container for fishes who
		public static CollectibleSpawner instance;

        //time range
		public float startTimerRange;
		public  float endTimerRange;

        //struct contains the Fish game object
		List<Fish> sortedFishes;
        //fish face side
		FloatingSide floatingSide = FloatingSide.left;
		List<int> timerOccurance = new List<int> ();	

		private float timeToSpawn = 0;   // calculating efficient random time for appearance of clock
		private float currentTime = 0;  // offset to check difference of time1

		private bool stopPlaying = false;
		// used to put stop and resume instantiate objects

		protected float levelSpeedIncreaser;  // changing specified level speed
		void Awake ()
		{
            instance = this;
			PlayerPrefs.SetInt (Treasure.TreasureGameCount, PlayerPrefs.GetInt (Treasure.TreasureGameCount) + 1);   	
		}

		void OnEnable ()
		{
			SwampFishingGameManager.SpawnCollectible += spawnCollectible;
		}

		void OnDisable ()
		{
			SwampFishingGameManager.SpawnCollectible -= spawnCollectible;
		}


        //start the spawning
		void spawnCollectible ()
		{
			
            CreatePlayer ();
            //sets the sorted speed and level speed from level data
			sortedFishes = SwampFishingGameManager.existingInstance.existingLevel.fishes.OrderByDescending (Fish => Fish.occurancePossibility).ToList ();
			levelSpeedIncreaser=SwampFishingGameManager.existingInstance.existingLevel.levelSpeedIncreaser;
//			foreach (Fish fish in sortedFishes)
//			{
//				Debug.Log (fish.occurancePossibility.ToString());
//			}
			SetTimerOccurance ();//setting random timer occurance depends upon total time duration and number of timer
			CreateTreasure ();//setting treasure in screen
			currentTime = Time.time;
			timeToSpawn = 0;
			StartCoroutine (Instantiation ());//coroutine started
		}



		IEnumerator Instantiation ()
		{
			while (!stopPlaying) {
				yield return null;

				if (Time.time - currentTime > timeToSpawn) {
					if (SwampFishingGameManager.existingInstance.gameState != GameState.paused)
						InstantiatePriorityBase ();
					currentTime = Time.time;
					timeToSpawn = UnityEngine.Random.Range (startTimerRange/levelSpeedIncreaser, endTimerRange/levelSpeedIncreaser);
				}
			}
		}


        //stop the spawning 
		public void StopInstantiating ()
		{
			stopPlaying = true;
		}

        /// <summary>
        /// Creates the player.
        /// </summary>
		public void CreatePlayer ()
		{
			GameObject currentPlayer = (GameObject)Instantiate (player);
			currentPlayer.transform.position = new Vector3 (.4f, 2.68f, 0);
		}

		void InstantiatePriorityBase ()
		{
			
		

			#region check to spawn timer
			if (timerOccurance.Count != 0)
			{
				if (timerOccurance [0] < Timer.instance.TimeSpent ())
				{					
					GameObject currentObject = null;
					FloatingSide randomDirection = GetRandomEnum<FloatingSide> ();
					if (randomDirection == FloatingSide.left) {
						currentObject = (GameObject)Instantiate (timer);
						currentObject.transform.position = new Vector3 (ViewController.leftSpawnUpPoint.x, UnityEngine.Random.Range (ViewController.leftSpawnUpPoint.y, ViewController.leftSpawnDownPoint.y), 0);	
					} else {
						Vector3 rotation = new Vector3 (0, 180, 0);
						currentObject = (GameObject)Instantiate (timer, new Vector3 (ViewController.rightSpawnUpPoint.x, UnityEngine.Random.Range (ViewController.rightSpawnUpPoint.y, ViewController.rightSpawnDownPoint.y), 0), Quaternion.Euler (rotation));
					}
					currentObject.transform.parent = SpawnPool;
					timerOccurance.RemoveAt (0);
				}		
			#endregion

			else {
					InstantiateFish ();
				}
			}

			else
			{
			  	InstantiateFish ();  // called when no timer to spawn
			}

		}


		void InstantiateFish()
		{
			bool instantiated = false;
			float random = Random.Range (0f, 100f); 
			foreach (Fish fish in sortedFishes) {			
				
				//0 to 100% 

				if (fish.occurancePossibility <= random && !instantiated) {
					GameObject currentObject = null;
					instantiated = true;
					FloatingSide randomDirection = GetRandomEnum<FloatingSide> ();
					if (randomDirection == FloatingSide.left) {
						currentObject = (GameObject)Instantiate (fish.fishName);
						currentObject.transform.position = new Vector3 (ViewController.leftSpawnUpPoint.x, UnityEngine.Random.Range (ViewController.leftSpawnUpPoint.y, ViewController.leftSpawnDownPoint.y), 0);
					} else {
						Vector3 rotation = new Vector3 (0, 180, 0);
						currentObject = (GameObject)Instantiate (fish.fishName, new Vector3 (ViewController.rightSpawnUpPoint.x, UnityEngine.Random.Range (ViewController.rightSpawnUpPoint.y, ViewController.rightSpawnDownPoint.y), 0), Quaternion.Euler (rotation));
					}
					currentObject.transform.parent = SpawnPool;
				}

			}
		}


        //remove the all fishes
		public void  RemoveFishes ()
		{
			StopInstantiating ();
			foreach (Transform fish in SpawnPool) {
				Destroy (fish.gameObject);
			}
		}

		public static T GetRandomEnum<T> ()
		{
			System.Array values = System.Enum.GetValues (typeof(T));
			return  (T)values.GetValue (UnityEngine.Random.Range (0, values.Length));
		}

        //set occurence time of fish
		void SetTimerOccurance ()
		{
			//set the data from level data
            int totalLevelTime = (int)SwampFishingGameManager.existingInstance.existingLevel.totalLevelTime;
			int interval = totalLevelTime / SwampFishingGameManager.existingInstance.existingLevel.totalTimerOccurance;
			for (int count = 1; count <= SwampFishingGameManager.existingInstance.existingLevel.totalTimerOccurance; count++) {
				timerOccurance.Add (Random.Range ((count - 1) * interval, count * interval));
			}
		}



		void CreateTreasure ()  // called when playing 
		{
			// spawn points to  place treasure randomly in bottom of screen
			if (!SwampFishingGameManager.existingInstance.existingLevel.IsTreasureAvailable)
				return;
			Vector2 leftSpawnUpPoint = Camera.main.ScreenToWorldPoint (new Vector2 (0 + Screen.width * .1f, Screen.height - .9f * Screen.height)); 
			Vector2 rightSpawnUpPoint = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width - Screen.width * .1f, Screen.height - .9f * Screen.height)); 
			GameObject currentObject = (GameObject)Instantiate (treasure);
			currentObject.transform.position = new Vector2 (Random.Range (leftSpawnUpPoint.x, rightSpawnUpPoint.x), leftSpawnUpPoint.y);

		}


	}
}
