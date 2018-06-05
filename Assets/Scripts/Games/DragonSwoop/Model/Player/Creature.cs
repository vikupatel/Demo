using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
	public enum PlayerType
	{	
		Main,
		Trail
	}

public class Creature : MonoBehaviour
	{

	
		public PlayerType playerType = PlayerType.Main;		

		public virtual void OnTriggerEnter2D(Collider2D other)
		{
			
			Collectibles collectedItem = other.GetComponent<Collectibles> ();

			if (collectedItem != null)
			{							
				other.GetComponent<Collectibles> ().collectItem (other.gameObject, playerType);		
			}

		}


		void OnCollisionEnter2D(Collision2D coll)
		{
			
			if (DragonakSwoopGameManager.instance.currentLevel.existingPlayer.playerMode != PlayerMode.invincible)
			{
				if (coll.collider.gameObject.layer != 4)   // 3 is an layer of water level  
				{
					DragonakSwoopGameManager.instance.GameOver ();
					DestroyPlayer ();
				}
			}

		}

			
		public void DestroyPlayer()
		{
			if (playerType == PlayerType.Main)
			{
				Destroy (this.gameObject);
			}
			else if (playerType == PlayerType.Trail)
			{
				Destroy (transform.parent.gameObject);
			}
		}

	}

}
