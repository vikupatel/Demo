using UnityEngine;
using System.Collections;

namespace Games {
	/// <summary>
	/// Base class for all games in the app
	/// </summary>


	public class GameManager : MonoBehaviour {

		public static GameManager instance;

		void Awake() {
			instance = this;

		}

		public Level currentLevel;

		public virtual void LoadLevel (Level level) {
			currentLevel = level;
		}
		public virtual void StartGame() 
		{
			
		}			
		public virtual void GameOver() 
		{
			
		}

		public virtual void Pause() {
			
		}

		public virtual void Resume() {

		}

	}
}