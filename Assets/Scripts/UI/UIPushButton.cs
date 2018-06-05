using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;


public class UIPushButton : Button {


	public float pushScale = 0.95f;
	public float pushSpeed = 0.2f;



	public override void OnPointerDown (PointerEventData eventData) {

		Sequence sequence = DOTween.Sequence();
		sequence.Append(this.transform.DOScale(new Vector3(pushScale, pushScale, 1f), pushSpeed*0.5f));
		sequence.AppendCallback(()=>ButtonPressed(eventData));
		sequence.Append(this.transform.DOScale(Vector3.one, pushSpeed*0.5f));
		sequence.Play();
	}


	void ButtonPressed(PointerEventData eventData) {
		base.OnPointerDown(eventData);
	}


	public override void OnPointerUp (PointerEventData eventData) {
		base.OnPointerUp(eventData);
	}
}
