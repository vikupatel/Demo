using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;


namespace Games.SwampFishing {
public class CreateMenu : MonoBehaviour {

	[MenuItem("Assets/Create/CreateSwampFishingLevel")]
	public static void CreateAsset ()
	{
			CreateLevel.CreateAsset<Level> ();
	}
}
}
#endif