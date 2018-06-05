using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
/// <summary>
/// used to switch mode of gameplay either flying or floating.
/// </summary>
public class ModeSwitcher : MonoBehaviour 

{
		public  PlayerMovementMode  switchMovementTo =PlayerMovementMode.floating;


		void OnTriggerEnter2D(Collider2D other) 
		{			

			Player player = other.GetComponent<Player> ();
			if (player != null)
			{				
				CameraMove.instance.SwitchMode (switchMovementTo);
				DragonakSwoopGameManager.instance.currentLevel.existingPlayer.SwitchMode (switchMovementTo);
			}

		}

//	public 
}
}
