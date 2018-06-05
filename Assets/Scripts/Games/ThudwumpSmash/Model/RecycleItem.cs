using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Games.ThudwumpSmash
{
	[SerializeField]
	public class DamagableArea
	{
		public List<Collectibles> collectibleItem;
	}

	public class RecycleItem:MonoBehaviour,IDamagable
	{

	public List<DamagableArea> damagableArea;
	public int totalCollectibleItem=3;
	public float shakeIntensity=3;
	private bool _smashable;
	public bool smashable{get {return _smashable;} set {_smashable=value;}}
	public float lengthOffset=.5f;//used to maintain distance between recycleItemobject and conveyor belt
    // Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}

	public void Damage()
	{
			if (_smashable)
			{
				//do something
			}
	}


	public void ShakeWhenHit()
	{
			
	}


	}

}
