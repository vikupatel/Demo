using UnityEngine;
using System.Collections;

namespace Games.DragonakSwoop
{
	public enum NonMovableBehaviourType
	{
		coin,orbit,invincibility,slowdown
	}

	public class NonMovableItem : Collectibles 
	{
		public override void OnEnable ()
		{		
		  base.OnEnable ();
		}

	
}
}
