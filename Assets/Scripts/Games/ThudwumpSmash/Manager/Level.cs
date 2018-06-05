using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Games.ThudwumpSmash
{

	[CreateAssetMenu (fileName = "Level", menuName = "CreateLevelThudwumpSmash", order = 1)]
	public class Level : ScriptableObject
	{
		public int levelNumber;
		public List<RecycleItem> recycleItem;
		GameObject _existingPlayer;
		//reference to player when instantiated
		public Player existingPlayer{ get { return _existingPlayer.GetComponent<Player> (); } }
		public int destructibleSectionPerObject=3;
		public int targetScore = 500;
		public float collectibleFadeDuration=3;
		public int bonusScore=2;
		public int penaltyScore=1;
		public int scoreForCorrectPlace=100;
		public float totalTime=80;
		int _currentScore;
		public int currentScore 
		{
			get{ return _currentScore; }
		}

		public void UnloadPlayer ()
		{
			_existingPlayer = null;
			Reset ();
		}

		void OnEnable ()
		{
			Reset ();
		}

		public void UpdateScore(int Score)
		{
			_currentScore = Score;
		}


		void Reset ()
		{
			_currentScore = 0;
		}


		public void CreatePlayer()
		{
			
		}

		public void LaunchItem()
		{
			int totalItem = recycleItem.Count;

			if (totalItem != 0)
			{
				
				RecycleItem itemPicked = recycleItem [Random.Range (0, totalItem-1)];
				float lenghtOffset = itemPicked.lengthOffset;
				ConveyorBelt.instance.FetchObjectInConveyor(itemPicked.gameObject,lenghtOffset);
			}
			else
			{
				Debug.LogError ("no recycle items");
			}
		}
	
	
	}
}
