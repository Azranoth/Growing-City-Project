﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcutterCamp : MonoBehaviour {

	public int 		_production = 12;
	public string	_blockType = "Ground"; // Default
	public GameObject _tile;		       // Tile object on which the woodcutter is built

	public GameObject _City;
	public GameObject _Coordinator;

	// Use this for initialization
	void Start () {

		GameObject _City = GameObject.Find ("City");
		GameObject _Coordinator = GameObject.Find ("Coordinator");

		transform.parent = GameObject.Find ("WoodCutters").transform;

		// Calculate real amount produced per tick according to the distance between the farm & the city
		int distanceToCity = (int) (Mathf.Sqrt( Mathf.Pow(_City.transform.position.x - this.transform.position.x,2)
			+ Mathf.Pow(_City.transform.position.z - this.transform.position.z,2))/2.0f);

		// Increased production if the woodcutter is located on a forest block
		if (this._blockType == "Forest")
			this._production = (int)((float)this._production*1.5);

		// Modify global coordinator food production on build
		_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Production += 
			(int)(this._production / (distanceToCity/_Coordinator.GetComponent<RessourcesManagement> ()._MaxDistance));
	}

	// Update is called once per frame
	void Update () {

	}

}
