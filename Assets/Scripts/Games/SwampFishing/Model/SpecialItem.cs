using UnityEngine;
using System.Collections;
namespace Games.SwampFishing
{

	public enum SpecialItemType
	{
		clock,
		Treasure
	}

	public class SpecialItem : Collectibles
	{
		public SpecialItemType specialItemType;

		public delegate void SpecialItemDelegate();
		public  SpecialItemDelegate specialCollectibles;



		public override void OnEnable()
		{
			base.OnEnable ();	
			if (this.specialItemType == SpecialItemType.clock)
			{
				FloatStraight ();
				specialCollectibles += TimerCollected;
			}
			if (this.specialItemType == SpecialItemType.Treasure)
			{
				specialCollectibles += TreasureCollected;

			}
		}

		public override void Update ()
		{
			base.Update ();			
		}

		public void FloatStraight()
		{
			StartCoroutine (FloatingStraightCoroutine());
		}


		IEnumerator FloatingStraightCoroutine()
		{		
			float speed = 1.3f;
			while (!caught) 
			{
				yield return null;

				if(SwampFishingGameManager.existingInstance.gameState!=GameState.paused)
				transform.Translate (Vector2.right * speed * Time.deltaTime*levelSpeedIncreaser);
			}			
	    }


		public void TimerCollected()
		{
			int seconds=10; // add seconds to clock
			Timer.instance.AddLevelTime(seconds);
		}


		public void TreasureCollected()
		{
			
		}


	
	
	}
}

