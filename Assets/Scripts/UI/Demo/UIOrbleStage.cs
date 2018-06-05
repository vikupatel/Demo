using UnityEngine;
using System.Collections;
using DG.Tweening;


namespace Demo {
	public class UIOrbleStage : MonoBehaviour {

		public UIOrble orble;
		public Camera characterCamera;
		public delegate void PresentedDelegate();
		public PresentedDelegate presentedDelegate;
		public Transform shockwave;

		void Awake() {
			HideOrble();
		}
			

		public void HideOrble() {
			orble.transform.localScale = Vector2.zero;
			characterCamera.enabled = true;
			shockwave.localScale = new Vector3(0, 0, 1f);
		}


		public void PresentOrble (Vector3 screenSpaceCordinates, PresentedDelegate presentedDelegate) {

			this.presentedDelegate = presentedDelegate;

			characterCamera.enabled = true;
			Vector3 sourceWorldPosition = characterCamera.ScreenToWorldPoint(new Vector3(screenSpaceCordinates.x, screenSpaceCordinates.y, characterCamera.farClipPlane));
			orble.transform.position = sourceWorldPosition;
			Vector3 targetWorldPosition = characterCamera.ScreenToWorldPoint(new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, characterCamera.farClipPlane));

			orble.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
			orble.transform.DOMove(targetWorldPosition, 0.5f);
			shockwave.DOScale(new Vector3(100f, 100f, 100f), 1f).OnComplete(new TweenCallback(Presented));



		}

		public void Presented() {
			if (presentedDelegate != null) presentedDelegate();
		}


	}
}