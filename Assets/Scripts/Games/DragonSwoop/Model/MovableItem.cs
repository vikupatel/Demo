using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop

{
	public enum MovableBehaviourType
	{
		Straight,backAndForth,stopAndGo,wave
	}

	public enum MovableItemStatus
	{
		active,
		inActive
	}

	public class MovableItem : Collectibles
	{
		public delegate void MovableItemBehaviour();

		public  MovableItemBehaviour moveItem;
		public 	MovableBehaviourType movingBehaviourType=MovableBehaviourType.Straight;

		public float speed;

		MovableItemStatus movableItemStatus=MovableItemStatus.inActive;
		public float distanceToGetActive = 30;   // item becomes active when distance between current item and player below specified value;

		Vector3 beginPosition;

		public override void OnEnable ()
		{
			base.OnEnable ();
			if (movingBehaviourType == MovableBehaviourType.Straight)
			{
				moveItem += FloatStraight;
			}
			else if (movingBehaviourType == MovableBehaviourType.backAndForth)
			{
				moveItem += FloatBackAndForth;
			}

			else if (movingBehaviourType == MovableBehaviourType.Straight)
			{
				moveItem += FloatStopAndGo;
			}
			else if (movingBehaviourType == MovableBehaviourType.wave)
			{
				moveItem += FloatWave;
			}

		}

		void Update()
		{
			if (movableItemStatus == MovableItemStatus.inActive) 
			{		
				
				//Debug.Log (""+transform.position+""+Vector3.Distance (transform.position, Camera.main.transform.position));
				if (Vector3.Distance (transform.position,  Camera.main.transform.position) < distanceToGetActive)
					{
						movableItemStatus = MovableItemStatus.active;
						moveItem ();
					}

			}
		}




		 void FloatStraight()
		{
			StartCoroutine (FloatingStraightCoroutine());
		}

		IEnumerator FloatingStraightCoroutine()
		{
			while (!caught) 
			{
				yield return null;
				if(DragonakSwoopGameManager.instance.gameState!=GameState.paused)
					transform.Translate (Vector2.left * speed * Time.deltaTime);
			}
		}

		 void FloatBackAndForth()
		{
			
		}

		 void FloatStopAndGo()
		{
			
		}

		void FloatWave()
		{
			StartCoroutine (FloatWaveCoroutine());
		}
		IEnumerator FloatWaveCoroutine()
		{
			int degree=35;// degree to change wave length;
			while (!caught) 
			{
				yield return null;
				if (DragonakSwoopGameManager.instance.gameState != GameState.paused)
				{
					transform.localRotation = Quaternion.Slerp (Quaternion.Euler (0, 0, degree), Quaternion.Euler (0, 0, -degree), Mathf.PingPong (speed* Time.time * .6f, 1.0f));
					transform.Translate (Vector2.left * speed * Time.deltaTime*speed);
				}

			}			
		}
		void SetDistance()
		{
			
		}
	
	
 }

}