using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageArea {

	public VillageAreaType areaType;
	public int level = 1;
	public List<Orble> orbles = new List<Orble>();


	public VillageArea(VillageAreaType areaType) {
		this.areaType = areaType;
	}
}
