using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
	public enum CollectibleMovementBehaviourType
	{
		movable,nonMovable
	}

	public enum CollectedItemBehaviour
	{
		IncreasePoint,
		slowDown,
		Invicibility,
		GameOver
	}
    
	public enum BulletBehaviour
	{
		blast,
		ignore
	}

	public class Collectibles : MonoBehaviour
	{
		public delegate void CollectItemDelegate(GameObject collidedItem,PlayerType playerType);
		public CollectItemDelegate collectItem;		
		//public CollectibleMovementBehaviourType movementBehaviourType=CollectibleMovementBehaviourType.movable;
		public CollectedItemBehaviour collectedItemBehaviour=CollectedItemBehaviour.IncreasePoint;
		public BulletBehaviour  bulletBehaviour=BulletBehaviour.blast;
		protected bool _caught;
		public bool caught{get{ return _caught;}}
		public int point;	

		public virtual void OnEnable()
		{
			if (collectedItemBehaviour == CollectedItemBehaviour.IncreasePoint) 
			{
				collectItem += CollectOrbit ;
			} else if (collectedItemBehaviour == CollectedItemBehaviour.slowDown) {
				collectItem += CollectSlowDown;
			} else if (collectedItemBehaviour == CollectedItemBehaviour.Invicibility) {
				collectItem+=  CollectInvicibility;
			} else if (collectedItemBehaviour == CollectedItemBehaviour.GameOver) {
				collectItem+=  CollectGameOver;
			}

		}

		void CollectOrbit(GameObject collidedItem,PlayerType playerType)
		{
			DragonakSwoopGameManager.instance.currentLevel.UpdatePoint (point);
			Destroy (collidedItem);
		}

		void CollectSlowDown(GameObject collidedItem,PlayerType playerType)
		{			
			DragonakSwoopGameManager.instance.currentLevel.existingPlayer.SlowDown ();
			Destroy (collidedItem);
		}

		void CollectInvicibility(GameObject collidedItem,PlayerType playerType)
		{
			DragonakSwoopGameManager.instance.currentLevel.existingPlayer.SetInvicibleMode ();
		}

		void CollectGameOver(GameObject collidedItem,PlayerType playerType)
		{
			if (DragonakSwoopGameManager.instance.currentLevel.existingPlayer.playerMode != PlayerMode.invincible && DragonakSwoopGameManager.instance.gameState == GameState.inGame)
			{
				if (playerType == PlayerType.Main)
				{
					DragonakSwoopGameManager.instance.GameOver ();
					Destroy (collidedItem);
				}
			}
		}


  
	}


}
