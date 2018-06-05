using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {

	 SpriteRenderer  sprite;

	Vector3 currentPosition;
	Vector3 beginPosition;
	public void BeginDrag()
	{
		Debug.Log ("begin drag");
		Vector3 pos = Input.mousePosition;	
		beginPosition = Camera.main.ScreenToWorldPoint (pos);
	}

	public void EndDrag()
	{
		
	}

	public void OnDrag()
	{
		Debug.Log ("on drag");
		currentPosition = Input.mousePosition;
		float yScale = Mathf.Clamp ((beginPosition.y - Camera.main.ScreenToWorldPoint (currentPosition).y), 1, 3);
		this.transform.localScale = (new Vector3(this.transform.localScale.x, yScale,0));
	}

}
