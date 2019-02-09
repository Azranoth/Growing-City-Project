using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

	public int 		_production = 9;	   		// Ressources produced by tick

	[Header("Block related")]
	public string	_blockType = "Ground"; 		// Default
	public GameObject _tile;			   		// Tile object on which the farm is built
	[Space]

	[Header("Ref game objects")]
	public GameObject _City;					
	public GameObject _Coordinator;
	public RessourcesManagement _CoordRsc;
	[Space]

	static protected float _buildTime = 1.0f;	// Time required to build it

	protected int _distanceToCity = 0;
	protected int _actualProd = 0;


	// Use this for initialization
	void Start () {
		
		transform.parent = GameObject.Find ("Farms").transform;

		this.GetComponent<CommonBuilding> ().initBuildTime (_buildTime);

	}
	
	// Update is called once per frame
	void Update () {
		

	}

	/*
	 * function initProduction()
	 * Initialize the ressources production of this farm & begin the production
	 */
	public void initProduction(){

		_City = GameObject.Find ("City");
		_Coordinator = GameObject.Find ("Coordinator");

		if (_City == null) {
			Debug.Log ("MISSING CITY OBJECT - Generation failed");
			Application.Quit ();
		}

		// Calculate real amount produced per tick according to the distance between the farm & the city
		_distanceToCity = (int)(Mathf.Sqrt (Mathf.Pow (_City.transform.position.x - this.transform.position.x, 2)
		                     + Mathf.Pow (_City.transform.position.z - this.transform.position.z, 2)) / 2.0f);

		// Increased production if the farm is located on a ground block
		if (this._blockType == "Ground")
			this._production = (int)((float)this._production * 1.2);

		_CoordRsc = _Coordinator.GetComponent<RessourcesManagement> ();
		// Modify global coordinator food production on build
		_actualProd = (int)(this._production / (_distanceToCity / _CoordRsc._MaxDistance));
		_CoordRsc._Ressources [0]._Production += _actualProd;

		_Coordinator.GetComponent<Buildings> ()._isBuildingFarm = false;
		this.GetComponent<CommonBuilding> ()._prodInitDone = true;
		Debug.Log (" FARM PROD " + _tile.name + " - " + (int)(_production / (_distanceToCity / _CoordRsc._MaxDistance)));
	}


	/*
	 * function updateProductionRoads()
	 */
	public void updateProductionRoads(){

		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			// Modify global coordinator food production on build
			float tmp = (this._production / (_distanceToCity / 0.2f));
			if (tmp > 0.0f) {
				if (tmp < 1.0f) {
					_CoordRsc._Ressources [0]._Production += 1;
					_actualProd++;
				} else {
					_CoordRsc._Ressources [0]._Production += (int)tmp;
					_actualProd += (int)tmp;
				}
			}
			Debug.Log (" FARM PROD ROADS NEW " + _tile.name + " - " + tmp);	
		} else { // If the farm is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeRoads ());
		}
	}

	/*
	 * Coroutine waitForBuildToUpgradeRoads()
	 */
	private IEnumerator waitForBuildToUpgradeRoads(){

		while (!this.GetComponent < CommonBuilding> ()._buildDone) {
			yield return null;
		}
		updateProductionRoads ();
		yield return 0;
	}

	/*
	 * function updateProductionEfficiency()
	 */
	public void updateProductionEfficiency(){
		
		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			_CoordRsc._Ressources [0]._Production -= _actualProd;
			_production = (int)(_production * 1.3f);
			_actualProd = (int)(_production / (_distanceToCity / _CoordRsc._MaxDistance));
			_CoordRsc._Ressources [0]._Production += _actualProd;
			Debug.Log (" FARM PROD EFFI NEW " + _tile.name + " - " + _actualProd);

		} else {// If the farm is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeEfficency ());
		}
	}

	/*
	 * Coroutine waitForBuildToUpgradeEfficency()
	 */
	private IEnumerator waitForBuildToUpgradeEfficency(){

		while (!this.GetComponent < CommonBuilding> ()._buildDone) {
			yield return null;
		}
		updateProductionEfficiency ();
		yield return 0;
	}

	public void updateFarmProductivity(){
		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			_CoordRsc._Ressources [0]._Production -= _actualProd;
			_production = (int)(_production * 1.4f);
			_actualProd = (int)(_production / (_distanceToCity / _CoordRsc._MaxDistance));
			_CoordRsc._Ressources [0]._Production += _actualProd;
			Debug.Log (" FARM PROD UPGRADE NEW " + _tile.name + " - " + _actualProd);

		} else {// If the farm is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeFarm ());
		}
	}

	private IEnumerator waitForBuildToUpgradeFarm(){

		while (!this.GetComponent < CommonBuilding> ()._buildDone) {
			yield return null;
		}
		updateFarmProductivity ();
		yield return 0;
	}
}
