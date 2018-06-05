using UnityEngine;
using System.Collections;

namespace Games.SwampFishing
{
	public class FloatingCollectibles : Collectibles {
		delegate void FloatCollectibles();
        FloatCollectibles floatCollectibles;
		public float speed;
		public FloatingStyle floatingStyle=FloatingStyle.straight;
		//curve with the horizontal speed oscilation
		public AnimationCurve speedOscilation;
		//time it takes for one wave
		public float oscilationMagnitudeTime = 3f;
		private float oscilationTime = 0f;


		public override void OnEnable()
		{
			base.OnEnable ();
			//checked rotation in radian

			if (floatingStyle == FloatingStyle.straight)
				floatCollectibles += FloatStraight;
			else if (floatingStyle == FloatingStyle.wave)
				floatCollectibles += FloatWave;
			else if (floatingStyle == FloatingStyle.diagnal)
				floatCollectibles += FloatDiagonal;
			else if (floatingStyle == FloatingStyle.stopAndGo)
				floatCollectibles += FloatStopAndGo;
			else if (floatingStyle == FloatingStyle.random)
				floatCollectibles += FloatRandom;
		}


		public override void Start()
		{
			base.Start ();
			if(floatCollectibles!=null)
				floatCollectibles ();
		}

		public override void Update ()
		{
			base.Update ();
		
			//			if (!caught) {
			//				if (floatingStyle == FloatingStyle.stopAndGo) 
			//				{
			//					oscilationTime += Time.deltaTime;
			//					transform.Translate (Vector2.right * Time.deltaTime * (speed + speedOscilation.Evaluate(oscilationTime)));
			//					if (oscilationTime > oscilationMagnitudeTime) oscilationTime = 0;
			//				}
			//			}


		}


		public override void FloatStraight()
		{
			base.FloatStraight ();
			StartCoroutine (FloatingStraightCoroutine());

		}

		IEnumerator FloatingStraightCoroutine()
		{
			while (!caught) 
			{
				yield return null;
				if(SwampFishingGameManager.existingInstance.gameState!=GameState.paused)
					transform.Translate (Vector2.right * speed * Time.deltaTime*levelSpeedIncreaser);
			}
		}

		public override void FloatWave()
		{
			base.FloatWave ();
			StartCoroutine (FloatWaveCoroutine());
		}

		IEnumerator FloatWaveCoroutine()
		{
			int degree=35;// degree to change wave length;
			while (!caught) 
			{
				yield return null;
				if (SwampFishingGameManager.existingInstance.gameState != GameState.paused)
				{
					transform.localRotation = Quaternion.Slerp (Quaternion.Euler (0, (int)floatingSide, degree), Quaternion.Euler (0, (int)floatingSide, -degree), Mathf.PingPong (levelSpeedIncreaser* Time.time * .6f, 1.0f));
					transform.Translate (Vector2.right * speed * Time.deltaTime*levelSpeedIncreaser);
				}

			}			
		}


		public override void FloatStopAndGo()
		{
			base.FloatStopAndGo ();
			StartCoroutine (FloatStopAndGoCoroutine());
		}

		IEnumerator FloatStopAndGoCoroutine()
		{
			float stopOffset = .8f; //used to float fish during specified timeline
			float startTime = Time.time; //stores current time to calculate time difference
			float pauseTime = .5f;// pause time  for stopp time for fish 

			while (!caught) 
			{	
				yield return null;
				if (SwampFishingGameManager.existingInstance.gameState != GameState.paused) {
					oscilationTime += Time.deltaTime;
					transform.Translate (Vector2.right * Time.deltaTime * (speed * speedOscilation.Evaluate (oscilationTime))*levelSpeedIncreaser);
					if (oscilationTime > oscilationMagnitudeTime)
						oscilationTime = 0;
				}
			}
		}

		public override void  FloatDiagonal()
		{
			base.FloatDiagonal ();
			StartCoroutine (FloatDiagonalCoroutine());

		}
		IEnumerator FloatDiagonalCoroutine()
		{
			float diagonalAngle = 40;//used to set angle to go diagonal;
			Vector3 mainPosition=transform.position;  //saving the position where instantiated
			float distance = 2;
			Vector3 upAngle = new Vector3 (1,1,0);         //upwardAngle
			Vector3 downAngle = new Vector3 (0, -1, 0);     //downWardAngle
			bool upside = true;
			while (!caught)
			{
				yield return null;
				if (SwampFishingGameManager.existingInstance.gameState != GameState.paused) {
					if (Vector3.Distance (transform.position, mainPosition) < distance && upside) {
						//	transform.localRotation = Quaternion.Euler (0, (int)floatingSide, diagonalAngle);
						transform.Translate (upAngle * speed * Time.deltaTime*levelSpeedIncreaser);
					} else {
						upside = false;
						transform.Translate (downAngle * speed * Time.deltaTime*levelSpeedIncreaser);
						if (transform.position.y < mainPosition.y) {
							mainPosition = transform.position;
							upside = true;
						}
					}
				}

			}

		}

		public override void  FloatRandom()
		{
			base.FloatDiagonal ();
			StartCoroutine (FloatRandomCoroutine());

		}

		IEnumerator FloatRandomCoroutine()
		{
			int degree=35;// degree to change wave length;
			int startRange=2;  
			int endRange = 5;
			int randomDistance=Random.Range(startRange,endRange);  //random distance to move
			float stopSecondOffset=.5f;     //off set to stop fish a while
			Vector3 pastPosition = transform.position;
			bool reversePosition=false;
			while (!caught) 
			{
				yield return null;
				if (SwampFishingGameManager.existingInstance.gameState != GameState.paused)
				{
					if (Mathf.Abs (Vector3.Distance (transform.position, pastPosition)) < randomDistance) {
						if (!reversePosition)
							transform.Translate (Vector2.right * speed * Time.deltaTime*levelSpeedIncreaser);
						else
							transform.Translate (Vector2.left * speed * Time.deltaTime*levelSpeedIncreaser);	
					} else {
						yield return new WaitForSeconds (stopSecondOffset);
						reversePosition = !reversePosition;
						pastPosition = transform.position;
						if (reversePosition)
							randomDistance = Random.Range (startRange, endRange / 3); //movement of backward is 3 times lesser than forward. That will help to move fish to reach other side
					else
							randomDistance = Random.Range (startRange, endRange * 2);
					}
				}

			}	
		  }  




	}
}