using UnityEngine;
using System.Collections;

public class Extensions : MonoBehaviour {

	public static Color CombineColors(params Color[] aColors)
	{
		Color result = new Color(0,0,0,0);
		foreach(Color c in aColors)
		{
			result += c;
		}
		result /= aColors.Length;
		return result;
	}
}
