using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

namespace Demo {
	public class UIVillageOrble : MonoBehaviour {

		public Image sprite;
		public Image shadow;
		public DOTweenPath path;



		public void Drop() {

			sprite.enabled = true;
			shadow.enabled = true;
			sprite.transform.localPosition = new Vector3(0, 1000f, 0);
			sprite.transform.DOLocalMoveY(sprite.rectTransform.rect.height * 0.5f, 2f).SetEase(Ease.OutBounce);

			shadow.transform.localScale = new Vector3(3f, 3f, 1f);
			shadow.transform.DOScale(1f, 2f).SetEase(Ease.OutBounce).OnComplete(PlayPath);

		}



		void Hide() {
			sprite.enabled = false;
			shadow.enabled = false;
		}



		void PlayPath() {
			path.DOPlay();
		}


	}
}
