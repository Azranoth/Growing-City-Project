using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

	private static int _nbBuildings = 3;
	public GameObject[] _Buildings = new GameObject[_nbBuildings];	// List of each building usable in the game
	public int _indexBuildings = 0;									// Index of the current building type selected
	public int _buildingCost = 5;									// Building cost of the current building type selected

	public GameObject _farms;
	public GameObject _woodcutters;
	public GameObject _goldmines;
	public int _nbFarms 	  = 0;
	public int _nbWoodCutters = 0;
	public int _nbGoldMines   = 0;


	// Use this for initialization
	void Start () {

		_farms 		 = GameObject.Find("Farms");
		_woodcutters = GameObject.Find("WoodCutters");
		_goldmines   = GameObject.Find ("GoldMines");

	}
	
	// Update is called once per frame
	void Update () {
		// Farm
		if (Input.GetKey (KeyCode.Alpha1)) {
			_indexBuildings = 0;	// Farm
			_buildingCost = 5;
		}
		// Woodcutter
		if (Input.GetKey (KeyCode.Alpha2)) {
			_indexBuildings = 1;	// Woodcutter camp
			_buildingCost = 2;
		}

		if (Input.GetKey (KeyCode.Alpha3)) {
			_indexBuildings = 2;	// Goldmine
			_buildingCost = 8;
		}
		if (Input.GetKey (KeyCode.Alpha4)) {
			_indexBuildings = 3;	// Tradingpost
		}
	}
}
