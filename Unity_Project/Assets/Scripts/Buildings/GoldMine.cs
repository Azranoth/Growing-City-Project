using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour {

	public int 		_production = 5;	   // Ressources produced by tick

	[Header("Block related")]
	public string	_blockType = "Ground"; // Default
	public GameObject _tile;		       // Tile object on which the woodcutter is built
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

		transform.parent = GameObject.Find ("GoldMines").transform;

		this.GetComponent<CommonBuilding> ().initBuildTime (_buildTime);

	}
	
	// Update is called once per frame
	void Update () {

	}
		

	/*
	 * function initProduction()
	 * Initialize the ressources production of this mine & begin the production
	 */
	public void initProduction(){

		_City = GameObject.Find ("City");
		if (_City == null) {
			Debug.Log ("MISSING CITY OBJECT - Generation failed");
			Application.Quit ();
		}
		_Coordinator = GameObject.Find ("Coordinator");
		_CoordRsc = _Coordinator.GetComponent<RessourcesManagement> ();

		// Calculate real amount produced per tick according to the distance between the farm & the city
		_distanceToCity = (int) (Mathf.Sqrt( Mathf.Pow(_City.transform.position.x - this.transform.position.x,2)
			+ Mathf.Pow(_City.transform.position.z - this.transform.position.z,2))/2.0f);

		// Modify global coordinator food production on build
		_actualProd = (int)(this._production / (_distanceToCity/ _CoordRsc._MaxDistance));
		_CoordRsc._Ressources [2]._Production += _actualProd;
			

		_Coordinator.GetComponent<Buildings> ()._isBuildingMine = false;
		this.GetComponent<CommonBuilding> ()._prodInitDone = true;
	}

	/*
	 * function updateProductionRoads()
	 */
	void updateProductionRoads(){

		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			// Modify global coordinator food production on build
			float tmp = (this._production / (_distanceToCity / 0.2f));
			if (tmp > 0.0f) {
				if (tmp < 1.0f) {
					_CoordRsc._Ressources [2]._Production += 1;
					_actualProd++;
				} else {
					_CoordRsc._Ressources [2]._Production += (int)tmp;
					_actualProd += (int)tmp;
				}
			}
			Debug.Log ("MINE PROD ROADS NEW " + _tile.name + " - " + tmp);	
		}
		else{// If the mine is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeRoads ());
		}
	}

	public void updateProductionEfficiency(){
		if (this.GetComponent<CommonBuilding> ()._buildDone) {
			_CoordRsc._Ressources [2]._Production -= _actualProd;
			_production = (int)(_production * 1.3f);
			_actualProd = (int)(_production / (_distanceToCity / _CoordRsc._MaxDistance));
			_CoordRsc._Ressources [2]._Production += _actualProd;
			Debug.Log (" MINE PROD EFFI NEW " + _tile.name + " - " + _actualProd);
		}
		else{// If the mine is not done being built yet during the upgrade, start a coroutine so it gets upgraded once done
			StartCoroutine (waitForBuildToUpgradeEfficency ());
		}
	}


	/* ==========================================================================
	 * COROUTINES FOR UPGRADE FUNCTIONS
	 */

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
	 * Coroutine waitForBuildToUpgradeEfficency()
	 */
	private IEnumerator waitForBuildToUpgradeEfficency(){

		while (!this.GetComponent < CommonBuilding> ()._buildDone) {
			yield return null;
		}
		updateProductionEfficiency ();
		yield return 0;
	}

	// ==========================================================================

}
