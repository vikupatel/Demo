using UnityEngine;
using System.Collections;
namespace Games.SwampFishing
{
	public class Timer : MonoBehaviour {
		
		public delegate  void OnTimeOutDelegate();
		public static event OnTimeOutDelegate OnTimeOut;

		public delegate void OnTimerChangeDelegate(float time,float totaltime);
		public static event OnTimerChangeDelegate OnTimerChange;

		public static bool isActive = false;

		public static Timer instance;
		float totalTime=60;
		float timeLeft;
		/// <summary>
		/// time when level starts
		/// </summary>
		float currentTime;   
		/// <summary>
		/// difference time between each frame
		/// </summary>
		float timerOffset=0;
		/// <summary>
		/// time spent till level
		/// </summary>
		float timeSpan=0;

		void Awake()
		{
			instance = this;
		}

		public float TimeSpent()
		{
			return timeLeft;
		}

		public void ResetTimer(float time)
		{
			totalTime = time;
			timeLeft = totalTime;
		}

		void OnEnable()
		{
			Games.SwampFishing.SwampFishingGameManager.OnTimerStart += StartTimer;
		}

		void OnDisable()
		{
			Games.SwampFishing.SwampFishingGameManager.OnTimerStart -= StartTimer ;
		}

		public void UpdateTimer(float time)
		{
			totalTime = time;
		}

		public void StartTimer()
		{			
			
			currentTime = Time.time;
			isActive = true;
		}

		public void ResumeTimer()
		{
			isActive = true;
			currentTime = Time.time-timerOffset;
		}

		public void PauseTimer()
		{
			isActive = false;
		}

		public void Update()
		{	
				if (isActive)
			   {				
					timerOffset = Time.time - currentTime;		    
					//currentTime += (int)(timerOffset);

					if (OnTimerChange != null) {	
						timeLeft = timerOffset;	
                        //rais the timechnage evert and updates GUI ingameview
						OnTimerChange (timerOffset, totalTime);
					}

					if (timerOffset > totalTime) {
						isActive = false;
						OnTimeOut ();
					}
				}

		}

		public void AddLevelTime(int addTime)
		{
			totalTime += addTime;
		}



	}
}
