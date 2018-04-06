using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

	private static int _nbBuildings = 2;
	public GameObject[] _Buildings = new GameObject[_nbBuildings];
	public int _indexBuildings = 0;
	public int _buildingCost = 5;

	public GameObject _farms;
	public GameObject _woodcutters;
	public int _nbFarms 	  = 0;
	public int _nbWoodCutters = 0;


	// Use this for initialization
	void Start () {

		_farms 		 = GameObject.Find("Farms");
		_woodcutters = GameObject.Find("WoodCutters");

	}
	
	// Update is called once per frame
	void Update () {
		// Farm
		if (Input.GetKey (KeyCode.Alpha1)) {
			_indexBuildings = 0;
			_buildingCost = 5;
		}
		// Woodcutter
		if (Input.GetKey (KeyCode.Alpha2)) {
			_indexBuildings = 1;
			_buildingCost = 2;
		}

		if (Input.GetKey (KeyCode.Alpha3)) {
			_indexBuildings = 2;

		}
		if (Input.GetKey (KeyCode.Alpha4)) {
			_indexBuildings = 3;
		}
	}
}
