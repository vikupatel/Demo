using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Games.DragonakSwoop
{
	[System.Serializable]
	public struct Terrain
	{
		[Space(1)]
		[Header("Terrain with its length")]
		public GameObject terrain;
		public float length;
	}

	[System.Serializable]
	public class TerrainMode
	{
		[Space(5)]
		[Header("Terrain data for specific mode")]
		public float minDistance;
		public List<Terrain> terrainList;
	}

	[System.Serializable]
	public class TerrainSelect
	{
		[Space(10)]
		[Header("Total Specification of terrain")]
		public List<TerrainMode> terrainMode;

		public void SortData()
		{
			terrainMode = terrainMode.OrderByDescending (x=>x.minDistance).ToList();
		}

		public Terrain SelectTerrain()
		{
			SortData ();
			Terrain selectedTerrain=new Terrain();
			foreach (TerrainMode data in terrainMode)
			{
				float distanceCovered = DragonakSwoopGameManager.instance.currentLevel.distanceCovered;
				if (data.terrainList.Count != 0) 
				{	
				//	Debug.Log ("distance covered:"+distanceCovered+":min distance"+data.minDistance);				
					if (distanceCovered >= data.minDistance)
					{					
						selectedTerrain = data.terrainList [Random.Range (0, data.terrainList.Count )];
						return selectedTerrain;
					}
				}
			}
			return selectedTerrain;
		}
	}



	[CreateAssetMenu(fileName = "Level", menuName = "CreateLevelDragonakSwoop", order = 1)]
public class Level : ScriptableObject 
{
		
		public delegate void SetSpeed();
		public static event SetSpeed setSpeed;
		public int levelNumber;
		public GameObject player;
		[Range(10,100)]
		public int bonusChance;

		public float pointsMultiplier;

		GameObject _existingPlayer;   //reference to player when instantiated
		public Player existingPlayer{get { return _existingPlayer.GetComponent<Player>();}}

		Vector3 beginPosition; // used to calculate distance

		public int trailPlayer;
		int _currentScore;
		public int currentScore
		{
			get{ return _currentScore;}
		}
		public int levelScoreMultiplier;

		public float distanceOffset=0;  // used to set player position in screen  
		Vector2 distanceBetweenPlayer;  //  offset to adjest space between players. Uses distanceOffset to convert in vector3
		 int _distanceCovered = 0;
		public int distanceCovered
		{
			get{ return _distanceCovered;}
		}
		   public void LoadPlayer()
		  {
			distanceBetweenPlayer = new Vector3 (distanceOffset,0,0);			
		  }


		public TerrainSelect terrainSelect;   //  in future this should be converted into list of structure  because of various types of terrain and its size will be differ
		public float levelSpeed = 3;
		public float levelSpeedOffset = .2f;   //offset to increase speed



		public void UpdatePoint(int point)
		{			
			_currentScore += point*levelScoreMultiplier;
			ViewInGame.instance.UpdateCurrentScore (_currentScore);
		}
		public void UnloadPlayer()
		{
			Reset ();
		}

		void OnEnable()
		{
			Reset ();
		}

		void Reset()
		{
			_currentScore = 0;
			_distanceCovered = 0;
		}



		public void CreatePlayer()
		{
			GameObject currentPlayer =  Instantiate(player,ViewController.playerSpawnPoint+distanceBetweenPlayer, player.transform.rotation) as GameObject;
			//currentPlayer.transform.parent = Camera.main.transform;
			beginPosition = currentPlayer.transform.position;
			_existingPlayer = currentPlayer;
			existingPlayer.CreateTrailPlayer (trailPlayer);
			CameraMove.instance.SetSpeedAndTarget (existingPlayer.gameObject,existingPlayer.speed);
			currentPlayer.GetComponent<Rigidbody2D> ().Sleep ();
		}

		public void UpdateDistance()
		{
			_distanceCovered = (int)Vector2.Distance (existingPlayer.transform.position , beginPosition);
			ViewInGame.instance.SetDistanceCoveredUI (_distanceCovered);
		}


		public void CreateTerrain()
		{		
			Terrain terrain = terrainSelect.SelectTerrain ();
			DragonakSwoopGameManager.instance.CreateTerrain (terrain.terrain,terrain.length);
		}

	    

}
}
