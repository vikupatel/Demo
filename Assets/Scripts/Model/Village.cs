using UnityEngine;
using System.Collections;

public class Village {

	public VillageArea wyzania;
	public VillageArea snorgitz;
	public VillageArea sylvada;
	public VillageArea dragonLan;
	public VillageArea thudwud;


	//new village constructor
	public Village() {
		wyzania = new VillageArea(VillageAreaType.wyzania);
		snorgitz = new VillageArea(VillageAreaType.snorgitz);
		sylvada = new VillageArea(VillageAreaType.sylvada);
		dragonLan = new VillageArea(VillageAreaType.dragonlan);
		thudwud = new VillageArea(VillageAreaType.thudwud);
	}

}
