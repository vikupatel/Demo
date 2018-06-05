using UnityEngine;
using System.Collections;

namespace Games {
	public class Level : MonoBehaviour {

		public int currentScore = 0;

		protected virtual void ResetLevel() {

		}

		protected virtual void StartLevel() {
			
		}

		protected virtual void LevelComleted() {
			
		}

		protected virtual void LevelFailed() {
			
		}

	}
}
