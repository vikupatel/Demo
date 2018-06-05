using UnityEngine;
using System.Collections;
using System;

namespace Games.SwampFishing
{

	public enum FloatingSide
	{
		left=0,right=180
	}

	public enum FloatingStyle
	{
		straight,
		wave,
		stopAndGo,
		diagnal,
		random
	}

	public class GoodItems : FloatingCollectibles
	{
		
		public override void OnEnable()
		{
			base.OnEnable ();
		}
		public override void Start()
		{
			base.Start ();
		}

		public override void Update ()
		{
			base.Update ();			
		}

		public override void FloatStraight()
		{
			base.FloatStraight ();			
		}

		public override void FloatWave()
		{
			base.FloatWave ();
		}

		public override void FloatStopAndGo()
		{
			base.FloatStopAndGo ();
		}

		public override void FloatDiagonal()
		{
			base.FloatDiagonal ();			
		}

		public override void FloatRandom()
		{
			base.FloatRandom ();
		}
					
	}
}

