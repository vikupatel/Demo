using UnityEngine;
using System.Collections;

namespace Games.ThudwumpSmash
{

	public enum GameState
	{
		inMenu,
		tapToPlay,
		inGame,
		paused
	}

	public enum GameOverReason
	{
		timeOut,
		allObjectiveCompleted
	}
		
	//used when collectible appears
	public enum CollectibleState
	{
		alive,
		faded,
		collected
	}

}
