using UnityEngine;
using System.Collections;


namespace Games.ThudwumpSmash
{

	public class Collectibles :MonoBehaviour, IDamagable
   {
		
	public string name;
	CollectibleState collectibleState=CollectibleState.alive;
    
	public void Fadding()
	{
			
	}
	
	public void Damage()
	{
			
	}

	public void StartFadding(float time)
	{
			
	}
	
	public void RemoveCollectible()
	{
			//remove collectible from inventory list
			//decrease time
	}

    }
}