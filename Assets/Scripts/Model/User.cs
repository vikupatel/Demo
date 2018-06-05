using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class User: ITradingEntity {

	public string id = "";
	public string firstName = "";
	public string email = "";
	public string country = "";
	public string city = "";
	public Inventory inventory;

	public User() {
		
	}


}
