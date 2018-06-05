using UnityEngine;
using System.Collections;

[System.Serializable]
public class CatalogOrble : ScriptableObject {

	[SerializeField] public int catalogNumber = 0;
	[SerializeField] public Species species;
	[SerializeField] [Range(1,3)] public int typeId = 1;
	[SerializeField] [Range(1,3)] public int level = 1;
	[SerializeField] [Range(1,5)] public int colourId = 1;





	public string TypeStringName() {
		if (species == Species.Thudwump) {
			if (typeId == 1) return "Wacky";
			else if (typeId == 2) return "Sturdy";
			else if (typeId == 3) return "Mighty";
		}
		else if (species == Species.Wyzwyg) {
			if (typeId == 1) return "Kindly";
			else if (typeId == 2) return "Brainy";
			else if (typeId == 3) return "Devious";
		}
		else if (species == Species.Snorgit) {
			if (typeId == 1) return "Cheaky";
			else if (typeId == 2) return "Sneaky";
			else if (typeId == 3) return "Mean";
		}
		else if (species == Species.Sylvanite) {
			if (typeId == 1) return "Dreamy";
			else if (typeId == 2) return "Steely";
			else if (typeId == 3) return "Trendy";
		}
		else if (species == Species.Dragonak) {
			if (typeId == 1) return "Friendly";
			else if (typeId == 2) return "Speedy";
			else if (typeId == 3) return "Raging";
		}
		return "";
	}


	public string ColorStringName() {
		//TODO Add all colour names
		return "color " + colourId;
	}



}

