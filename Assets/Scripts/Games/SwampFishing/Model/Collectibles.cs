using UnityEngine;
using System.Collections;
namespace Games.SwampFishing
{

	public enum CollectibleBehaviourType
	{
		bad,good,special
	}

	/// <summary>
	/// used to select action timing  either before hook being pulled or after hook is pulled
	/// </summary>
	public enum CollectActionStatus
	{
		beforeHookPulled,
		afterHookPulled
	}


	public class Collectibles : MonoBehaviour {
		/// <summary>
		/// type or behaviour whether it is bad or good
		/// </summary>
		public CollectibleBehaviourType behaviourType=CollectibleBehaviourType.good;
		public CollectActionStatus collectActionStatus=CollectActionStatus.afterHookPulled;
		protected FloatingSide floatingSide = FloatingSide.left;
		protected bool caught;
		/// <summary>
		///  weight of collectible . useful for pulling speed.
		/// </summary>
		[Range (1,10)]
		public int weight;
		public int point;
		protected float levelSpeedIncreaser;  // changing specified level speed
		public void SetCaughtStatus(bool status)
		{
			caught = status;
		}

		public bool GetCaughtStatus()
		{
			return caught;
		}
		public virtual void OnEnable()
		{
			levelSpeedIncreaser=SwampFishingGameManager.existingInstance.existingLevel.levelSpeedIncreaser;
			if (this.transform.rotation.y == 1)
			{				
				floatingSide = FloatingSide.right;
			}
			else
			{
				floatingSide = FloatingSide.left;
			}
		}

		public virtual  void Start()
		{

		}

		public virtual void Update ()
		{
			if (floatingSide == FloatingSide.left) {
				if (transform.position.x > ViewController.rightSpawnUpPoint.x)
					Destroy (this.gameObject);
			} else {
				if (transform.position.x < ViewController.leftSpawnUpPoint.x)
					Destroy (this.gameObject);
			}
		}
		public virtual void FloatStraight()
		{
			
		}
		public virtual void  FloatWave()
		{

		}
		public virtual void FloatStopAndGo()
		{
			
		}

		public virtual void FloatDiagonal()
		{
			
		}

		public virtual void   FloatRandom()
		{
			

		}
					
	}
}
