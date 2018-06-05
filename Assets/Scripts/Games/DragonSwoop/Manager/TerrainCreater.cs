using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
public class TerrainCreater : MonoBehaviour {



	// Use this for initialization
	void Start ()
	{
	
	}

		void OnTriggerEnter2D(Collider2D other)
		{
			
			DragonakSwoopGameManager.instance.currentLevel.CreateTerrain ();
			Destroy (other.gameObject);		

		}
	
	


}
}
