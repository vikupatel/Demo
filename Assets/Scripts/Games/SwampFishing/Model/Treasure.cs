using UnityEngine;
using System.Collections;

namespace Games.SwampFishing
{
public class Treasure : MonoBehaviour
{
	public SpriteRenderer leaf;
	public BoxCollider2D TreasureBox;
	public const string TreasureGameCount = "TresureGameCount";  // used as preference to check total gamplay count
	public int totalTimeToDisplayTreasure=5;
    bool openTreasureInGame;
	int  timerForShowingTreasure=0;
	public float gamePlayCount=3;

	void OnEnable()
	{
		int gameNumber = PlayerPrefs.GetInt (TreasureGameCount);
		
			if(gameNumber % gamePlayCount == 0)
		{
			gameNumber = 0;
			openTreasureInGame = true;
			PlayerPrefs.SetInt (TreasureGameCount, 0);
			Debug.Log ("trasuretime");
		}

		if (openTreasureInGame)
		{
				int time = SwampFishingGameManager.existingInstance.existingLevel.totalLevelTime;
				timerForShowingTreasure = Random.Range (0,time); 
		}

	}


		void Update()
		{
			if (openTreasureInGame)
			{
				if (Timer.instance.TimeSpent () > timerForShowingTreasure)
				{
					ShowTreasure ();

					openTreasureInGame = false;
				}
			}
		}


		 public void ShowTreasure()
		{

			TreasureBox.enabled = true;
			leaf.enabled = false;	
			StartCoroutine (WaitShowingTreasure());
		}

		IEnumerator WaitShowingTreasure()
		{
			yield return new WaitForSeconds (totalTimeToDisplayTreasure);
			if (!GetComponent<Collectibles> ().GetCaughtStatus())
			{
				TreasureBox.enabled = false;
				leaf.enabled = true;
			}
		}   


}

}

