using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

	private static int _nbBuildings = 4;
	public GameObject[] _Buildings = new GameObject[_nbBuildings];	// List of each building usable in the game - PREFABS
	public int _indexBuildings = 0;									// Index of the current building type selected (init farm)
	public int _buildingCost = 5;									// Building cost of the current building type selected (init farm)

	[Header("Game objects")]
	public GameObject _farms;
	public GameObject _woodcutters;
	public GameObject _goldmines;
	public GameObject _tradingposts;
	[Space]

	[Header("Game objects number")]
	public int _nbFarms 	   = 0;			// Number of built farms
	public int _nbWoodCutters  = 0;			// Number of built camps
	public int _nbGoldMines    = 0;			// Number of built mines
	public int _nbTradingPosts = 0;			// Number of built posts
	[Space]

	[Header("Booleans")]
	public bool _isBuildingFarm = false;	// Is a farm being built right now?
	public bool _isBuildingCamp = false;	// Is a camp being built right now?
	public bool _isBuildingMine = false;	// Is a mine being built right now?
	public bool _isBuildingPost = false;	// Is a post being built right now?
	public bool _outOfMenu = true; 			// Displayed/hidden menus
	[Space]

	public GameObject _tradeButton;			// Trading menu button



	// Use this for initialization
	void Start () {
		// Refresh prefabs' prod
		if (_Buildings [0] == null) {
			Debug.Log ("Buildings - Farm prefab missing");
			Application.Quit ();
		}
		if(_Buildings [0].GetComponent<Farm> () == null){
			Debug.Log ("Buildings - Farm script missing on prefab");
			Application.Quit ();
		}
		_Buildings [0].GetComponent<Farm> ()._production = 9;

		if (_Buildings [1] == null) {
			Debug.Log ("Buildings - Woodcutter prefab missing");
			Application.Quit ();
		}
		if(_Buildings [1].GetComponent<WoodcutterCamp> () == null){
			Debug.Log ("Buildings - WoodcutterCamp script missing on prefab");
			Application.Quit ();
		}
		_Buildings [1].GetComponent<WoodcutterCamp> ()._production = 9;

		if (_Buildings [2] == null) {
			Debug.Log ("Buildings - Goldmine prefab missing");
			Application.Quit ();
		}
		if(_Buildings [2].GetComponent<GoldMine> () == null){
			Debug.Log ("Buildings - GoldMine script missing on prefab");
			Application.Quit ();
		}
		_Buildings [2].GetComponent<GoldMine> ()._production = 5;

		if (_Buildings [3] == null) {
			Debug.Log ("Buildings - Tradingpost prefab missing");
			Application.Quit ();
		}
		if(_Buildings [3].GetComponent<TradingPost> () == null){
			Debug.Log ("Buildings - TradingPost script missing on prefab");
			Application.Quit ();
		}
		_Buildings [3].GetComponent<TradingPost> ()._tradingCapacity = 15;
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
