using UnityEngine;
using System.Collections;
namespace Games.SwampFishing
{

	public enum BadItemType
	{
		lose_life,
		gameover
	}
	public class BadItems: FloatingCollectibles
	{
		public delegate void BadItemsCollectedDelegate();
		public  BadItemsCollectedDelegate BadCollectibles;
		public BadItemType badItemType;

		public override void OnEnable()
		{
			base.OnEnable ();

				if (this.badItemType == BadItemType.lose_life)
					BadCollectibles += LoseLifeItemCollected;
				else if (this.badItemType == BadItemType.gameover)
					BadCollectibles += GameOverItemCollected;
			
		}

		public override void Start()
		{
			base.Start ();
		}

		public override void Update ()
		{
			base.Update ();			
		}

		public override void FloatStraight()
		{
			base.FloatStraight ();			
		}

		public override void FloatWave()
		{
			base.FloatWave ();
		}

		public override void FloatStopAndGo()
		{
			base.FloatStopAndGo ();
		}

		public override void FloatDiagonal()
		{
			base.FloatDiagonal ();

		}
		public override void FloatRandom()
		{
			base.FloatRandom ();
		}



		public void LoseLifeItemCollected()
		{
			SwampFishingGameManager.existingInstance.existingLevel.LiveLost ();
			if (SwampFishingGameManager.existingInstance.existingLevel.GetLiveLost () == SwampFishingGameManager.existingInstance.existingLevel.totalLive)
			{
				SwampFishingGameManager.existingInstance.GameOver (GameOverReason.allLiveLost);
			}
		}

		public void GameOverItemCollected()
		{
			CollectibleSpawner.instance.player.GetComponent<PlayerHook> ().MoveHookUpwardsAfterHookFallen ();
		}

	}

}
