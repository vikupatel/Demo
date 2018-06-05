using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIVillageArea : MonoBehaviour {


	public Image sprite;
	public ParticleSystem puffParticles;


	public void Show() {
		sprite.enabled = true;
		puffParticles.Play();
	}


	public void Hide() {
		sprite.enabled = false;
	}

}
