using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

	private static int _nbBuildings = 4;
	public GameObject[] _Buildings = new GameObject[_nbBuildings];	// List of each building usable in the game
	public int _indexBuildings = 0;									// Index of the current building type selected (init farm)
	public int _buildingCost = 5;									// Building cost of the current building type selected (init farm)

	public GameObject _farms;
	public GameObject _woodcutters;
	public GameObject _goldmines;
	public GameObject _tradingposts;

	public int _nbFarms 	   = 0;			// Number of built farms
	public int _nbWoodCutters  = 0;			// Number of built camps
	public int _nbGoldMines    = 0;			// Number of built mines
	public int _nbTradingPosts = 0;			// Number of built posts

	public bool _isBuildingFarm = false;	// Is a farm being built right now?
	public bool _isBuildingCamp = false;	// Is a camp being built right now?
	public bool _isBuildingMine = false;	// Is a mine being built right now?
	public bool _isBuildingPost = false;	// Is a post being built right now?

	public GameObject _tradeButton;			// Trading menu button

	public bool _outOfMenu = true; 			// Displayed/hidden menus

	// Use this for initialization
	void Start () {

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
			_buildingCost = 8;
		}
	}

	/*
	 * activateTradeButton()
	 * Called when the first trading post is built : unlock the access to the trade menu by displaying its button
	 */
	public void activateTradeButton(){
		_tradeButton.SetActive (true);
	}
}
