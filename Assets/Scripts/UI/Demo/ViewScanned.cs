using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
using DG.Tweening;



namespace Demo {
	public class ViewScanned : UIView {

		public GameObject arCameraRoot;
		public Camera backgroundCamera;
		public UnityEngine.UI.Image background;
		public UIOrbleStage orbleStage;
		public UnityEngine.UI.Image title;
		public UnityEngine.UI.Image level;
		public CanvasGroup goTovillageGroup;


		public override void Show() {

			backgroundCamera.enabled = true;
			background.color = new Color(1f, 1f, 1f, 0);

			Sequence sequence = DOTween.Sequence();
			sequence.Append(background.DOFade(1f, 1f));
			title.transform.localScale = Vector3.zero;
			sequence.Append(title.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack));
			level.transform.localScale = new Vector3(0, 1f, 1f);
			sequence.Append(level.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack));
			sequence.AppendInterval(1f);
			goTovillageGroup.alpha = 0;
			sequence.Append(goTovillageGroup.DOFade(1f, 1f));
			sequence.AppendCallback(DisableAR);
			sequence.Play();



			base.Show();


		}


		public override void Hide() {
			base.Hide();
			orbleStage.HideOrble();
			background.color = new Color(1f, 1f, 1f, 0);
			backgroundCamera.enabled = false;
		}



		void DisableAR() {
			arCameraRoot.SetActive(false);
		}



	}
}