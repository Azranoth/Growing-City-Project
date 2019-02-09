using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPost : MonoBehaviour {

	public int _tradingCapacity = 15;

	[Header("Block related")]
	public GameObject _tile;					// Tile object on which the farm is built
	public string	_blockType = "Ground"; 		// Default
	[Space]

	[Header("Ref game objects")]
	public GameObject _City;					
	public GameObject _Coordinator;
	public RessourcesManagement _CoordRsc;
	[Space]		   		


	static protected float _buildTime = 3.0f;	// Time required to build it
	protected int _distanceToCity = 0;
	protected int _actualProd = 0;





	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find ("TradingPosts").transform;

		this.GetComponent<CommonBuilding> ().initBuildTime (_buildTime);

	}
	
	// Update is called once per frame
	void Update () {

	}
		

	/*
	 * function initFunction()
	 * Initialize the trading capacity of this post
	 */
	public void initFunction(){

		_City = GameObject.Find ("City");
		if (_City == null) {
			Debug.Log ("MISSING CITY OBJECT - Generation failed");
			Application.Quit ();
		}

		_Coordinator = GameObject.Find ("Coordinator");
		_CoordRsc = _Coordinator.GetComponent<RessourcesManagement> ();

		// Calculate real amount traded per tick according to the distance between the farm & the city
		_distanceToCity = (int) (Mathf.Sqrt( Mathf.Pow(_City.transform.position.x - this.transform.position.x,2)
			+ Mathf.Pow(_City.transform.position.z - this.transform.position.z,2))/2.0f);

		// If a trading post is near sea, efficiency++ 
		//TODO

		_CoordRsc._tradingCapacityPerTick += 
			(int)(this._tradingCapacity / (_distanceToCity / _CoordRsc._MaxDistance));
		_Coordinator.GetComponent<Buildings> ()._isBuildingPost = false;
		this.GetComponent<CommonBuilding> ()._prodInitDone = true;
	}

	/*
	 * function updateProductionRoads()
	 */
	void updateProductionRoads(){

		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			float tmp = (this._tradingCapacity / (_distanceToCity / 0.2f));
			if (tmp > 0.0f) {
				if (tmp < 1.0f) {
					_CoordRsc._Ressources [2]._Production += 1;
					_actualProd++;
				} else {
					_CoordRsc._Ressources [2]._Production += (int)tmp;
					_actualProd += (int)tmp;
				}
			}
			Debug.Log (" TRADING CAP ROADS NEW " + _tile.name + " - " + tmp);	
		} else {// If the farm is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeRoads ());
		}
	}

	/*
	 * Coroutine waitForBuildToUpgradeEfficency()
	 */
	private IEnumerator waitForBuildToUpgradeRoads(){

		while (!this.GetComponent < CommonBuilding> ()._buildDone) {
			yield return null;
		}
		updateProductionRoads ();
		yield return 0;
	}

	void updateProductionEfficiency(){
		// Deliberatly left blank -> global call to buildings, don't need to call farms, mines and woodcutters separatly that way
	}
		

	/*
	 * function updateTradingCapacity()
	 */
	public void updateTradingCapacity(){
		
		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			_CoordRsc._tradingCapacityPerTick -= _actualProd;
			_tradingCapacity = (int)(_tradingCapacity * 1.2f);
			_actualProd = (int)(_tradingCapacity / (_distanceToCity / _CoordRsc._MaxDistance));
			_CoordRsc._tradingCapacityPerTick += _actualProd;
			Debug.Log (" TRADE POST CAP UPG NEW " + _tile.name + " - " + _actualProd);

		} else {// If the post is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeCaravans ());
		}
	}

	/*
	 * Coroutine waitForBuildToUpgradeCaravans()
	 */
	private IEnumerator waitForBuildToUpgradeCaravans(){

		while (!this.GetComponent < CommonBuilding> ()._buildDone) {
			yield return null;
		}
		updateTradingCapacity ();
		yield return 0;
	}
}
