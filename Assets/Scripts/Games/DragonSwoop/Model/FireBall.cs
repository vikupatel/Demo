using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
public class FireBall : MonoBehaviour
{
		
		public float speed =5f;
		Vector2 bulletBeginPosition;//calculate bullet position where bullet is instantiated

		void OnEnable()
		{
			bulletBeginPosition = transform.position;
		}

		void Update()
		{
			if (DragonakSwoopGameManager.instance.gameState != GameState.paused)
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				if (Vector2.Distance (transform.position, bulletBeginPosition) > ViewController.distanceWithInScreen) {
					Destroy (this.gameObject);
				}
			}
		}

		public  void OnTriggerEnter2D(Collider2D other)
		{
			Collectibles collectibles = other.GetComponent<Collectibles> ();
			if (collectibles)
			{
				if (collectibles.bulletBehaviour == BulletBehaviour.blast) {
					Destroy (collectibles.gameObject);
					Destroy (this.gameObject);
				}
				else
				{
					
				}
			}
			else
			{
				Destroy (this.gameObject);
			}
		}

}
}
