using UnityEngine;
using System.Collections;

namespace Games.ThudwumpSmash
{

   public class ConveyorBelt : MonoBehaviour 
   {
		public Transform startPosition;
		public Transform endPosition;
		public static ConveyorBelt instance;
		public float speed=3;


		void Awake()
		{
			instance = this;
		}


		public void FetchObjectInConveyor(GameObject item,float offset)
		{
			GameObject currentRecycleItem = Instantiate (item, startPosition.position, Quaternion.identity) as GameObject;
			MoveItemToTable (currentRecycleItem,offset);
		}

		public void RemoveObjectInConveyor()
		{
			
		}

		void MoveItemToTable(GameObject item,float offset)
		{
			//float startX = startPosition.position.x;
			float endX = endPosition.position.x;
			iTween.MoveTo(item, iTween.Hash("x", endX+offset, "time", speed, "islocal", true, "easetype", iTween.EaseType.linear));
		}

   }
}