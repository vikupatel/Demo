using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
public class CameraMove : MonoBehaviour 
	{

		public delegate void Movement();
	    Movement movement;
		public static CameraMove instance;

		GameObject target;
		float speedOfPlayer; //this will help to synchronise speed when player is moving faster
		Vector3 positionOffset;
		public float xFactor=.5f;
		public BoxCollider2D inviciblePlatform;  // used when invicible mode is on to prevent player from going out of screen

		void OnEnable()
		{
			instance = this;
			movement += JumpMode;
		}


		public void SetSpeedAndTarget(GameObject targetPlayer,float speed)
		{
			target = targetPlayer;
			speedOfPlayer = speed;
			positionOffset.x = targetPlayer.transform.position.x+xFactor;    // setting space of dragon from middle of view 
			positionOffset.y = targetPlayer.transform.position.y;
			positionOffset.z = transform.position.z;
			transform.position = positionOffset;
		}

		void Update ()
		{  
			if (DragonakSwoopGameManager.instance.gameState == GameState.inGame) 
			{
				 movement();
			}
		}

		public void DisableMode()
		{
			movement = null;
		}

		public void SwitchMode(PlayerMovementMode movementMode)
		{
			if (movementMode == PlayerMovementMode.Jumping) 
			{
				movement = null;
				movement += JumpMode ;
			}
			else if (movementMode == PlayerMovementMode.floating)
			{
				movement = null;
				movement += FloatMode ;
			}
		}
		public void JumpMode()
		{
			positionOffset = Vector2.MoveTowards (transform.position, target.transform.position+Vector3.right*xFactor, DragonakSwoopGameManager.instance.currentLevel.existingPlayer.speed);
			positionOffset.y = ViewController.playerSpawnPoint.y;
			positionOffset.z = transform.position.z;
			transform.position=positionOffset;
		}

		public void FloatMode()
		{
			positionOffset = Vector2.MoveTowards (transform.position, target.transform.position+Vector3.right*xFactor, DragonakSwoopGameManager.instance.currentLevel.existingPlayer.speed);
			positionOffset.y = Vector2.MoveTowards (positionOffset, target.transform.position,DragonakSwoopGameManager.instance.currentLevel.existingPlayer.speed/2).y;
			positionOffset.z = transform.position.z;
			transform.position=positionOffset;
		}

		


}
}