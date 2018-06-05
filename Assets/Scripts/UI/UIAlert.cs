using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAlert : MonoBehaviour {

	public Text title;
	public Text message;
	public Button[] buttons;
	public Text[] buttonLabels;


//	void Awake() {
//		this.gameObject.SetActive(false);
//	}


	public void Show(string pTitle, string pMessage, string[] pButtons) {

		this.gameObject.SetActive(true);
		title.text = pTitle;
		message.text = pMessage;

		if (pButtons.Length == 1) {
			buttons[0].gameObject.SetActive(false);
		}
		else if (pButtons.Length == 2) {
			buttons[0].gameObject.SetActive(true);
			buttonLabels[0].text = pButtons[0];
			buttons[1].gameObject.SetActive(true);
			buttonLabels[1].text = pButtons[1];
		}
	}



	public void Button0Tapped() {
		UIModalManager.instance.buttonPressed(buttons[0].GetComponentInChildren<Text>().text);
		this.gameObject.SetActive(false);
		if (UIModalManager.instance.cam.enabled) UIModalManager.instance.cam.enabled = false;
	}

	public void Button1Tapped() {
		UIModalManager.instance.buttonPressed(buttons[1].GetComponentInChildren<Text>().text);
		this.gameObject.SetActive(false);
		if (UIModalManager.instance.cam.enabled) UIModalManager.instance.cam.enabled = false;
	}

}
