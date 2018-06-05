using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Prime31;

public class UIModalManager : MonoBehaviour {
	
	public static UIModalManager instance;
	public UIAlert customAlert;
	public Camera cam;
	public EventSystem eventSystem;

	
	public enum ButtonPressed {
		Positive,
		Negative
	};
	
	void Awake() {
		instance = this;
		cam.enabled = false;
	}
	
	void OnEnable() {
		#if UNITY_IOS
		EtceteraManager.alertButtonClickedEvent += buttonPressed;
		#elif UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += buttonPressed;
		#else
		
		#endif
	}
	
	void OnDisable() {
		#if UNITY_IOS
		EtceteraManager.alertButtonClickedEvent -= buttonPressed;
		#elif UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= buttonPressed;
		#else
		Debug.LogWarning("UIPopup can only be called in iOS or Android", gameObject);
		#endif
	}
	
	public void buttonPressed(string button) {
		this._buttonPressed = button;
	}
	
	//Variables
	private string _buttonPressed = "none";
	
	public IEnumerator ShowPopup(string title, string message, string[] buttons, System.Action<string> pressed) {

		HideSpinner();

		this._buttonPressed = "none";
		if (buttons.Length > 2) {
			Debug.LogWarning("UIPopup.ShowPopup can only show 2 buttons.", gameObject);
			yield return null;
		}
		#if UNITY_IOS && !UNITY_EDITOR
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title, message, buttons);
		#elif UNITY_ANDROID && !UNITY_EDITOR
		if(buttons.Length < 2 )
			EtceteraAndroid.showAlert(title, message, buttons[0]);
		else 
			EtceteraAndroid.showAlert(title, message, buttons[0], buttons[1]);
		#else
		cam.enabled = true;
		customAlert.Show(title, message, buttons);
		#endif
		while(this._buttonPressed == "none")
			yield return new WaitForEndOfFrame();

		pressed(_buttonPressed);
		
	}


	public void ShowSpinner() {

		eventSystem.enabled = false;
#if UNITY_IOS
		EtceteraBinding.showActivityView();

#endif
#if UNITY_ANDROID
		EtceteraAndroid.showProgressDialog("", "");
#endif

	}



	public void HideSpinner() {
		
		#if UNITY_IOS
		EtceteraBinding.hideActivityView();
		
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif


		eventSystem.enabled = true;
	}





}
