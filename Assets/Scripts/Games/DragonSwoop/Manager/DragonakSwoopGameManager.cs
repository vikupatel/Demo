using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.DragonakSwoop
{
	
public enum GameState
{
	inMenu,
	tapToPlay,
	inGame,
	paused
}

public enum GameOverReason
{
	badItemCollected

}


public class DragonakSwoopGameManager : MonoBehaviour
{

	public static DragonakSwoopGameManager instance;
	public GameState gameState;
	public Games.DragonakSwoop.Level currentLevel;

	
	Vector2 savedVelocity;
	float savedAngularVelocity=0;
	[HideInInspector]
	public float terrainLengthOffset=0;

	Queue<GameObject>terrainQueue = new Queue<GameObject>();
	   
	   void Awake()
	   {
		   		instance = this;
	   }

	   public  void LoadLevel (Games.DragonakSwoop.Level level)
	   {
	    	 	currentLevel = level;
		    	currentLevel.LoadPlayer ();
       }
	   
	   public void UnloadLevel()
	   {
			//operation to remove something from current level
			RemoveWorld();
			currentLevel.UnloadPlayer();
			currentLevel =null;
	   }


		public void RemoveWorld()
		{
			Destroy (currentLevel.existingPlayer.gameObject);
			while(terrainQueue.Count!=0)
			{
				GameObject terrain = terrainQueue.Dequeue ();
				Destroy (terrain);
			}
		}
 
	   public  void StartGame() 
	   {
			DragonakSwoopGameManager.instance.gameState = GameState.inGame;
	   }

	    public  void GameOver() 
	    {
			gameState = GameState.inMenu;
			ViewGameOver.instance.PopulateGameOverUI ();
			UnloadLevel ();
	    }

		public  void Pause()
		{
			gameState = GameState.paused;

			if (currentLevel.existingPlayer != null)
			{
				currentLevel.existingPlayer.rigidBody.Sleep();
				currentLevel.existingPlayer.SleepTrailPlayersRigidBody ();
			}
		}

		public  void Resume() 
		{
				gameState = GameState.inGame;
				if (currentLevel.existingPlayer != null)
				{
				currentLevel.existingPlayer.rigidBody.WakeUp();
				currentLevel.existingPlayer.WakeUpTrailPlayersRigidBody ();
				}
		}

		public void AddInQueue(GameObject terrain)
		{
			terrainQueue.Enqueue(terrain);
		}

		public GameObject RemoveInQueue()
		{
			if (terrainQueue.Count > 0)
				return terrainQueue.Dequeue();
			else
				return null;
		}

		public void  CheckStackStatus()
		{
			int maximumTerrainCount=2;     // maximum allowed to show in worldspace.
			if (terrainQueue.Count > maximumTerrainCount)
			{
				GameObject terrainToRemove = RemoveInQueue ();
				if (terrainToRemove != null)
				Destroy (terrainToRemove);
			}
		}

		public void CreateTerrain(GameObject terrain, float levelTerrainLength)
		{
			GameObject newTerrain;
			newTerrain =  Instantiate(terrain,new Vector2(terrainLengthOffset,0), this.transform.rotation) as GameObject;	
			terrainLengthOffset += levelTerrainLength;			
			AddInQueue (newTerrain);
			CheckStackStatus ();
		}


		public void ShootFireBall()
		{
			currentLevel.existingPlayer.ShootFireBall ();
		}

	}
}
