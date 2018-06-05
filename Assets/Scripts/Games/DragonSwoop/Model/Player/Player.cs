using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.DragonakSwoop
{



	public enum PlayerMovementMode
	{
		Jumping,
		floating
	}

	public enum PlayerMode
	{
		normal,
		invincible
	}
	public class Player : Creature
    {
		public delegate void Movement();
		Movement movement;

		protected PlayerMovementMode playerMovementMode=PlayerMovementMode.Jumping;
		public float jumpSpeed=3;
		public float floatSpeed=.3f;
		Vector2 jumpFactor;
		Vector2 floatFactor;
		[HideInInspector]
	  	public Rigidbody2D rigidBody;
		[HideInInspector]
		public BoxCollider2D boxCollider;
		List<GameObject> trailPlayerList = new List<GameObject>();
		public float trailPlayerMovementDelay=.3f;

		public GameObject trailPlayer;
		public float trailSpace=0;   // adjest space between player and/or trail player

		PlayerMode _playerMode=PlayerMode.normal;
		public PlayerMode playerMode{get{return _playerMode;}}
		public GameObject fireBall;
		public Transform fireBallBeginPoint;
		public float shootFrequency=.3f;  // shooting interval between 2 fireballs
		public float maxShootWaveTime=1;
		public float maxShootWaitingTime=3;
		float lastShotTime=0;
		float shootWaveTime=0;
		#region EffectsOnCollectibleCatch
		public float InvicibleTime;
		/// <summary>
		/// slowing movement in specified percentage
		/// </summary>
		[Range(10, 100)]
		public float slowDownPercentage;
		#endregion


		SpriteRenderer spriteRenderer;



		float _speed=0;
		public float speed{get{ return _speed;}}
		float speedOffset=0;

		public void SetPlayerMovementMode(PlayerMovementMode mode)
		{
			playerMovementMode = mode;
		}

    	void OnEnable()
		{			
			movement += JumpMode;
			jumpFactor = new Vector2 (0, jumpSpeed);
			floatFactor = new Vector2 (0,floatSpeed);
			rigidBody = this.GetComponent<Rigidbody2D> ();
			boxCollider = this.GetComponent<BoxCollider2D> ();
			spriteRenderer = this.GetComponent<SpriteRenderer> ();
			Level.setSpeed+=SetSpeed;
			SetSpeed ();
		}

		void OnDisable()
		{
			Level.setSpeed -= SetSpeed;
		}

		void SetSpeed()
		{
			_speed = DragonakSwoopGameManager.instance.currentLevel.levelSpeed;	
			speedOffset = DragonakSwoopGameManager.instance.currentLevel.levelSpeedOffset;
		}

		public void CreateTrailPlayer(int totalTrailPlayer)
		{
			for (int counter = 1; counter <= totalTrailPlayer; counter++)
			{
				GameObject currentPlayer = Instantiate (trailPlayer, new Vector2(transform.position.x-boxCollider.size.x * this.transform.localScale.x * (1 + trailSpace) * counter,transform.position.y), this.transform.rotation) as GameObject;
				trailPlayerList.Add (currentPlayer);
				currentPlayer.transform.parent = transform;
				currentPlayer.GetComponent<Rigidbody2D> ().Sleep ();
			}
		}

		public void SlowDown()
		{			
			_speed = _speed * (slowDownPercentage/ 100);
		}


		void Update()
		{
    		if (DragonakSwoopGameManager.instance.gameState == GameState.inGame)
			{
				_speed += speedOffset * Time.deltaTime;
				transform.Translate (Vector2.right * _speed * Time.deltaTime);  
				DragonakSwoopGameManager.instance.currentLevel.UpdateDistance();
			}
			movement ();
	}



		void Jump(Rigidbody2D  rigidBodyJump)
		{
			#if UNITY_EDITOR

			if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			rigidBodyJump.velocity = jumpFactor;
			#else
			if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
			rigidBodyJump.velocity = jumpFactor;
			#endif

		}

		void Float(Rigidbody2D  rigidBodyFloat)
		{
			#if UNITY_EDITOR
			if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			rigidBodyFloat.velocity = -floatFactor;
			#else
			if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
			rigidBodyFloat.velocity = -floatFactor;
			#endif
		}





		public void SwitchMode(PlayerMovementMode movementMode)
		{
			if (movementMode == PlayerMovementMode.Jumping) 
			{
				playerMovementMode = PlayerMovementMode.Jumping;
				movement = null;
				movement += JumpMode ;
			}
			else if (movementMode == PlayerMovementMode.floating)
			{
				playerMovementMode = PlayerMovementMode.floating;
				movement = null;
				movement += FloatMode ;			
			}
		}

		public void JumpMode()
		{
			#if UNITY_EDITOR
			if (Input.GetMouseButtonDown (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () && playerType == PlayerType.Main) 
			{
				if (DragonakSwoopGameManager.instance.gameState == GameState.tapToPlay)
				{					
					ViewInGame.instance.TapToPlayButton ();
					rigidBody.isKinematic = false;
					Jump (rigidBody);
					TrailEffect (playerMovementMode);
				} 
				else
				{
					if (DragonakSwoopGameManager.instance.gameState != GameState.paused)       //checking game is paused or not for player jump
					Jump (rigidBody);
					TrailEffect (playerMovementMode);
				}
			}

			#else
			if (Input.GetMouseButtonDown (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (0) && playerType == PlayerType.Main) 
			{
			Debug.Log ("jump");
			if (DragonakSwoopGameManager.instance.gameState == GameState.tapToPlay)
			{					
			ViewInGame.instance.TapToPlayButton ();
			rigidBody.isKinematic = false;
			Jump (rigidBody);
			TrailEffect (playerMovementMode);
			} 
			else
			{
			if (DragonakSwoopGameManager.instance.gameState != GameState.paused)       //checking game is paused or not for player jump
			Jump (rigidBody);
			TrailEffect (playerMovementMode);
			}
			}
			#endif
		}


		public void FloatMode()
		{
			#if UNITY_EDITOR

			if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && playerType == PlayerType.Main) 
			{

				if (DragonakSwoopGameManager.instance.gameState == GameState.tapToPlay)
				{
					ViewInGame.instance.TapToPlayButton ();
					rigidBody.isKinematic = false;
					Float (rigidBody);
					TrailEffect (playerMovementMode);
				} 
				else
				{
					if (DragonakSwoopGameManager.instance.gameState != GameState.paused) //checking game is paused or not for player jump
					{       
						Float (rigidBody);
						TrailEffect (playerMovementMode);
					}
				}
			}
			#else
			if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0) && playerType == PlayerType.Main) 
			{

			if (DragonakSwoopGameManager.instance.gameState == GameState.tapToPlay)
			{
			ViewInGame.instance.TapToPlayButton ();
			rigidBody.isKinematic = false;
			Float (rigidBody);
			TrailEffect (playerMovementMode);
			} 
			else
			{
			if (DragonakSwoopGameManager.instance.gameState != GameState.paused) //checking game is paused or not for player jump
			{       
			Float (rigidBody);
			TrailEffect (playerMovementMode);
			}
			}
			}
			#endif
		}
		  

		public override void OnTriggerEnter2D(Collider2D other)
		{
			base.OnTriggerEnter2D (other);
		}

		#region Trail Effects

		/// <summary>
		/// gives effect for delay jump
		/// </summary>

		public void TrailEffect(PlayerMovementMode playerMovementMode)    // argument is declared for consistency movement between main player and trail.(It will remain following when continuously switching mode)
		{
			int count = 0;
			foreach (GameObject trailPlayer in trailPlayerList) 
			{
				count++;
				StartCoroutine (TrailEffectCoroutine ( trailPlayer, trailPlayerMovementDelay * count,playerMovementMode));				
			}
		}

		IEnumerator TrailEffectCoroutine(GameObject trailPlayer, float timeStamp,PlayerMovementMode currentPlayerMovementMode) // DelayJump
		{
			float timeOffset=0;


				while (timeOffset < timeStamp) 
				{
				    yield return null;

					if (DragonakSwoopGameManager.instance.gameState != GameState.paused)
					{
					
						timeOffset += Time.deltaTime;
						if (timeOffset > timeStamp) 
					    {
						if (currentPlayerMovementMode == PlayerMovementMode.Jumping)
						{
							Jump (trailPlayer.GetComponent<Rigidbody2D> ());
						}
						else if (currentPlayerMovementMode == PlayerMovementMode.floating)
						{
							Float (trailPlayer.GetComponent<Rigidbody2D> ());
						}
						}
					}
				}				
					timeOffset = 0;	
		}

		public void SleepTrailPlayersRigidBody()
		{
			foreach (GameObject trailPlayer in trailPlayerList) 
			{
				trailPlayer.GetComponent<Rigidbody2D> ().Sleep ();
			}
		}

		public void WakeUpTrailPlayersRigidBody()
		{
			foreach (GameObject trailPlayer in trailPlayerList) 
			{
				trailPlayer.GetComponent<Rigidbody2D> ().Sleep ();
			}
		}


		#endregion


		#region EffectsOnCollectibleCatch

		public void SetInvicibleMode()
		{
			if (_playerMode != PlayerMode.invincible) {
				_playerMode = PlayerMode.invincible;
				StartCoroutine (BlinkingMode (spriteRenderer));
				TrailInvicibility ();
			}
		}

		public void TrailInvicibility()
		{

			foreach (GameObject trailPlayer in trailPlayerList) 
			{
				StartCoroutine( BlinkingMode (trailPlayer.GetComponent<SpriteRenderer>()));
			}
		}

		IEnumerator BlinkingMode(SpriteRenderer spriteRenderer)
		{
			
			float timeCount = Time.time;
			float totalTime = InvicibleTime + Time.time;
			float BlinkingShowIntencity = .3f;     // during blinking mode creating different intencity of showing 
			float BlinkingHideIntencity = .3f;      // during blinking mode creating different intencity of hiding
			float blinkOffset = Time.time;
			bool showMode = false;   // indicates whether player is showing or not
			Physics2D.IgnoreLayerCollision(9,11,true);
			CameraMove.instance.inviciblePlatform.enabled = true;
			while (_playerMode == PlayerMode.invincible) 
			{
				yield return null;
				if(DragonakSwoopGameManager.instance.gameState!=GameState.paused)    //checking whether game is paused or not
				{

				if (showMode)
				{
					if (Time.time - blinkOffset > BlinkingShowIntencity) 
					{
						showMode = false;
						blinkOffset = Time.time;
						PlayerViewMode (true,spriteRenderer);
					}
				}

			    else
				{					
			       if (Time.time - blinkOffset > BlinkingHideIntencity)
				   {
						showMode = true;
						blinkOffset = Time.time;
							PlayerViewMode (false,spriteRenderer);
				   }

				}

				if (Time.time - timeCount > InvicibleTime)
				{
						PlayerViewMode (true,spriteRenderer);
						_playerMode = PlayerMode.normal;
				}

			    
				}
			}
			Physics2D.IgnoreLayerCollision(9,11,false);
			CameraMove.instance.inviciblePlatform.enabled = false;

		}


		void PlayerViewMode(bool ShowMode,SpriteRenderer spriteRenderer)
		{
			if (ShowMode)
			{
				spriteRenderer.enabled = true;				
			}
			else
			{
				spriteRenderer.enabled = false;
			}
		}
		#endregion


		public void ShootFireBall()
		{
			if (DragonakSwoopGameManager.instance.gameState == GameState.inGame)
			{
						lastShotTime = Time.time;
						GameObject fireball = Instantiate (fireBall, fireBallBeginPoint.position, Quaternion.identity) as GameObject;	
				        ViewInGame.instance.DisableShootButton ();
				         StartCoroutine (EnablingButton());
			}
		}


		IEnumerator EnablingButton()
		{
			float currentTime = Time.time;
			while (currentTime - lastShotTime < maxShootWaitingTime)
			{				
				yield return null;
				if (DragonakSwoopGameManager.instance.gameState == GameState.inGame) 
				{
					currentTime += Time.deltaTime;	
				}
			}
			ViewInGame.instance.EnableShootButton ();

		}
}


}
