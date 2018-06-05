using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.SwampFishing
{

	public enum hookMovingStatus
	{
		movingDown,
		movingUp,
		rotating,
		breaking
	}

	public class PlayerHook : MonoBehaviour {

		Vector3 rotateFactor;
		public Transform CableStartPoint;//starting point of cable
		public float speed=10;
		public float ropRotation=20;
		Vector3 targetdirection;
		float defaultLength;
		public static hookMovingStatus currentHookStatus=hookMovingStatus.rotating;
		bool collectibleCatched=false;
		GameObject collectedItemInHook;//refrence to collected fish or object
		private float weightMultiplier=1;   //used to change speed on pulling based on fish's weight 
		Vector3 leftBottonCorner,rightBottomCorner;
		public GameObject fallingHook;
		// Use this for initialization

		void Start ()
		{
			leftBottonCorner   = Camera.main.ScreenToWorldPoint (new Vector2(0,Screen.height));
			rightBottomCorner  = Camera.main.ScreenToWorldPoint (new Vector2(Screen.width,Screen.height));
			defaultLength = Vector3.Distance (transform.position, CableStartPoint.position);
			rotateFactor = new Vector3 (0, 0, .01f);
		}
		void OnEnable()
		{
			Player.OnHookDown += OnHookDown;
		}

		void OnDisable()
		{
			ResetHookData ();
			Player.OnHookDown -= OnHookDown;
		}

		void OnHookDown(float downSpeed,float upSpeed)
		{
			currentHookStatus = hookMovingStatus.movingDown;
			SetPosition();
			StartCoroutine(MovingHook(downSpeed,upSpeed));
		}


		IEnumerator MovingHook(float downSpeed,float upSpeed)
		{			
			while (-transform.position.x>leftBottonCorner.x&&-transform.position.x<rightBottomCorner.x&&-transform.position.y<leftBottonCorner.y)
			{
				yield return  null;
				if(SwampFishingGameManager.existingInstance.gameState!=GameState.paused)
				transform.Translate (targetdirection * downSpeed*Time.deltaTime);
			}
			currentHookStatus = hookMovingStatus.movingUp;
			while (Vector3.Distance (transform.position , CableStartPoint.position) > defaultLength)
			{				
				yield return  null;
				if(SwampFishingGameManager.existingInstance.gameState!=GameState.paused)
				transform.Translate (targetdirection * (-upSpeed/weightMultiplier)*Time.deltaTime);
			}
			UpdateScore ();
			weightMultiplier = 1;
			if(currentHookStatus !=hookMovingStatus.breaking)
			currentHookStatus = hookMovingStatus.rotating;
		}


		// Update is called once per frame
		void Update ()
		{		
			if (currentHookStatus==hookMovingStatus.rotating)
			{
				
				if (transform.rotation.z * Mathf.Rad2Deg > ropRotation) 
				{
				//	transform.eulerAngles = new Vector3(transform.rotation.x,transform.rotation.y,2*ropRotation);
					speed = -speed;
				} else if (transform.rotation.z * Mathf.Rad2Deg < -ropRotation)
				{
				//	transform.eulerAngles = new Vector3(transform.rotation.x,transform.rotation.y,-2*ropRotation);
					speed = -speed;
				}
				if(SwampFishingGameManager.existingInstance.gameState!=GameState.paused)
				transform.RotateAround (CableStartPoint.position, rotateFactor, speed * Time.deltaTime);
			}

		}


        //set the position of hook
		void SetPosition()
		{
			targetdirection = transform.localPosition - CableStartPoint.position;
			targetdirection.Normalize ();
		}


		void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log (other.gameObject.name);

			if (currentHookStatus == hookMovingStatus.movingUp&&!collectibleCatched)
			{
				if (other.GetComponent<Collectibles> ().collectActionStatus == CollectActionStatus.afterHookPulled) {
					other.transform.parent = this.transform;
					other.transform.GetComponent<Collectibles> ().SetCaughtStatus (true);
					collectibleCatched = true;
					collectedItemInHook = other.gameObject;
					weightMultiplier = other.transform.GetComponent<Collectibles> ().weight;
				}
				else
				{
					
					other.transform.GetComponent<Collectibles> ().SetCaughtStatus (true);
					collectibleCatched = true;
					//RemoveFish ();
					StartCoroutine(MoveHookUpwardsAfterHookFallen());
				}  
             }
		}

		public IEnumerator MoveHookUpwardsAfterHookFallen()
		{
			float hookSpeed=10;// speed to pull string when hook fallen down
			float waitingForGameover=2;
			collectibleCatched = false;
			currentHookStatus = hookMovingStatus.breaking;
			this.GetComponent<SpriteRenderer> ().enabled = false;
			this.GetComponent<BoxCollider2D> ().enabled = false;


			GameObject hook = (GameObject)Instantiate (fallingHook);
			hook.transform.position = transform.position;
			hook.transform.rotation = transform.rotation;
			Destroy (hook, 4);

			while (Vector3.Distance (transform.position , CableStartPoint.position) > defaultLength)
			{				
				yield return  null;
				transform.Translate (targetdirection * (-hookSpeed/weightMultiplier)*Time.deltaTime);	
			}
			yield return  new WaitForSeconds (waitingForGameover);
			SwampFishingGameManager.existingInstance.GameOver (GameOverReason.badItemCollected);
		}

		void UpdateScore()
		{			
			if (collectibleCatched)
			{
				RemoveFish ();
				//do something to update score
			}
		}
		void ResetHookData()
		{
			collectibleCatched = false;
			currentHookStatus=hookMovingStatus.rotating;
			weightMultiplier = 1;
			if (collectedItemInHook != null)
				Destroy (collectedItemInHook);
		}


		void RemoveFish()     
		{
			collectibleCatched = false;
			if (collectedItemInHook != null)
			{
				if (collectedItemInHook.GetComponent<Collectibles> ().behaviourType == CollectibleBehaviourType.good)
				{
					SwampFishingGameManager.existingInstance.existingLevel.UpdateScore (collectedItemInHook.GetComponent<Collectibles> ().point);
				} 

				else if (collectedItemInHook.GetComponent<Collectibles> ().behaviourType == CollectibleBehaviourType.bad)
				{
					if (collectedItemInHook.GetComponent<BadItems> ().BadCollectibles != null)
						collectedItemInHook.GetComponent<BadItems> ().BadCollectibles ();	
				}

				else if (collectedItemInHook.GetComponent<Collectibles> ().behaviourType == CollectibleBehaviourType.special)
				{
					if (collectedItemInHook.GetComponent<SpecialItem> ().specialCollectibles != null)
						collectedItemInHook.GetComponent<SpecialItem> ().specialCollectibles ();	
					SwampFishingGameManager.existingInstance.existingLevel.UpdateScore (collectedItemInHook.GetComponent<Collectibles> ().point);
				}				 
					Destroy (collectedItemInHook.gameObject);
			}
		 }


	}
}





