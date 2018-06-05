using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
using DG.Tweening;



namespace Demo {
	public class ViewVillage : UIView {

		public UIVillageOrble orble;
		public UIVillageArea area1;
		public UIVillageArea area2;


		public override void Show() {
			base.Show();
			orble.Drop();
//			area1.Show();

			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(1f);
			sequence.AppendCallback(area1.Show);
			sequence.AppendInterval(1f);
			sequence.AppendCallback(area2.Show);
			sequence.Play();

		}


		public override void Hide() {
			base.Hide();
			area1.Hide();
			area2.Hide();
		}




	}
}