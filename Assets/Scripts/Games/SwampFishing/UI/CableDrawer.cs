using UnityEngine;
using System.Collections;

namespace Games.SwampFishing
{
public class CableDrawer : MonoBehaviour {

		public Transform cableStartPoint;
		public Transform cableEndPoint;
		LineRenderer line;
		void Awake()
		{
			line = this.GetComponent<LineRenderer> ();
		}
		void Update()
		{
			line.SetPosition (0, cableStartPoint.position);
			line.SetPosition (1, cableEndPoint.position);
			line.sortingLayerID = this.GetComponent<SpriteRenderer>().sortingLayerID;
			line.sortingOrder= this.GetComponent<SpriteRenderer>().sortingOrder;
		}



	}
}