using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewController : MonoBehaviour {

	//DO NOT EDIT THIS SCRIPT

	public static ViewController instance;

	public UIView viewLoading;

	public List<GameObject> queue = new List<GameObject> ();
	[HideInInspector] public UIView currentView;

	public UIView debugView;


	void Awake() {
		instance = this;
	}


	void Start()
	{
		viewLoading.Hide();


		if (debugView != null) ChangeView(debugView);
		else ChangeView(viewLoading);

	}



	void ChangeView(UIView targetView) {
		if (currentView != null) currentView.Hide();
		currentView = targetView;
		currentView.Show();
	}



	public void ToWelcomeView() 
	{
//		ChangeView(viewMainMenu);
	}



	//DO NOT EDIT THIS SCRIPT

}
