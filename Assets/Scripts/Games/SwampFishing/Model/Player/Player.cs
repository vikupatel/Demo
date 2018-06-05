using UnityEngine;
using System.Collections;

namespace Games.SwampFishing
{



	public class Player : MonoBehaviour 
	{

		public delegate void OnPlayerUpdateDelegate(Player player);
		public static event OnPlayerUpdateDelegate OnPlayerUpdate;

		public delegate void OnHookDownDelegate(float downSpeed,float upSpeed);
		public static event OnHookDownDelegate OnHookDown;

		public float speed;	
		public PlayerHook playerHook;

		[Range (0.0F,1.0F)]
		public float hookUpSpeedOffset=.3f;  //used to set speed variation for rope pulling in upward direction.
		float upSpeed;	
		float downSpeed;


		void OnEnable()
		{
			upSpeed  = hookUpSpeedOffset * speed;
			downSpeed=(1-hookUpSpeedOffset) * speed;
		}

		public Player()
		{
			if (OnPlayerUpdate != null)
				OnPlayerUpdate (this);
		}


		void Update()
		{
			if (Input.GetMouseButtonDown (0)&&!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{

				if (PlayerHook.currentHookStatus == hookMovingStatus.rotating&&SwampFishingGameManager.existingInstance.gameState==GameState.inGame)
				{			
					if (OnHookDown != null)
					{
						OnHookDown (downSpeed, upSpeed);
					}
				}
			}
		}




	}

}

