using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Orble {

	public string firstName = "";
	public string villageName = "";
	public string fullName {
		get {
			return firstName + " of " + villageName;
		}
	}


	public CatalogOrble catalogOrble;

	public User owner = new User();
	public bool isOrbalized = true;
	public List<OrbleTrade> tradeHistory = new List<OrbleTrade>();
	public DateTime dateScanned;
	/// <summary>
	/// Unique id of the orble in the system
	/// </summary>
	public string id = "";


	//constructor
	public Orble() {
		
	}







}
