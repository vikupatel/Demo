using UnityEngine;
using System.Collections;
using Games;

public class AppManager : MonoBehaviour {

	public static AppManager instance;
	public string buildNumber = "";
	private GameManager currentGameManager;

	void Awake()
	{
		instance = this;
	}



	/// <summary>
	/// Loads the game scene and signing in into events
	/// </summary>
	public void LoadGame (string sceneName) {
		
	}


	public void UnloadCurrentGame() {
		Destroy(currentGameManager.gameObject);
	}







}
