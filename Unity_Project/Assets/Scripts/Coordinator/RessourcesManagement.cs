using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManagement : MonoBehaviour {

	/* @class Ressource
	 * @brief Contains production & stock datas of one kind of ressource in the current game*
	 */
	public class Ressource
	{

		public string _Name;
		public int _Amount = 25;
		public int _Production = 0;

		public Ressource(string name){
			_Name = name;
		}

		public void UseAmount(int amount){
			if (amount < 0) {
				_Amount -= 1;
				return;
			}
			if (_Amount < amount) {
				_Amount = 0;
			} else {
				_Amount -= amount;
			}
			Debug.Log ("Produce ressource "+ _Name + ", produces " + _Production + ", Used " + amount + ", have " + _Amount);
		}
	}

	/* ----- STATIC VARS -----*/
	public static int _nbRessources = 3;
	public static float TIME_BETWEEN_TICKS = 2.5f;

	public Ressource[] _Ressources = new Ressource[_nbRessources];

	public int[] _RessourceUsedPerTick = new int[_nbRessources];
	public float _timerTicks = TIME_BETWEEN_TICKS;

	public float _MaxDistance;

	/*
	 * 0 : food
	 * 1 : wood
	 * 2 : stone
	 * 3 : gold
	 */
	// Use this for initialization
	void Start () {

		_MaxDistance = 2.0f;

		// Creating ressources
		_Ressources[0] = new Ressource("Food");
		_Ressources[1] = new Ressource("Wood");
		_Ressources[2] = new Ressource("Gold");

		_RessourceUsedPerTick [0] = this.GetComponent<PopulationGrowth> ()._Population;
		_RessourceUsedPerTick [1] = 0;
		_RessourceUsedPerTick [2] = 0;
		//_RessourceUsedPerTick [3] = 0;
	}
	
	// Update is called once per frame
	void Update () {

		// updating ressources amount every TIME_BETWEEN_TICKS seconds
		if (_timerTicks <= 0) {

			for (int i = 0; i < _nbRessources; ++i) {
				_Ressources [i]._Amount += _Ressources [i]._Production;
			}
			// ----- FOOD
			// If food is unsufficient
			if ( _Ressources [0]._Amount < _RessourceUsedPerTick [0]) {
				// kill people accordingly
				this.GetComponent<PopulationGrowth> ()._Population -= (_RessourceUsedPerTick [0] - _Ressources [0]._Amount);
				_Ressources [0]._Amount = 0;


			// If food is sufficient
			} else {
				// Use the required amount to feed every citizen
				_Ressources [0].UseAmount (_RessourceUsedPerTick [0]);
			}


			// ----- WOOD
			// if wood is unsufficent
			if ( _Ressources [1]._Amount < _RessourceUsedPerTick [1]) {
				// Destroy buildings
				// Choose randomly the kind of building to destroy, then a random one of this kind

				List<int> buildingTypesExisting;
				buildingTypesExisting = new List<int> ();
				if (this.GetComponent<Buildings> ()._nbFarms > 0) {
					buildingTypesExisting.Add (0);
				}

				if (this.GetComponent<Buildings> ()._nbWoodCutters > 0) {
					buildingTypesExisting.Add(1);
				}

				if (this.GetComponent<Buildings> ()._nbGoldMines > 0) {
					buildingTypesExisting.Add(2);
				}

				if (buildingTypesExisting.Count > 0) {
					int rand = (int)buildingTypesExisting[Random.Range (0, buildingTypesExisting.Count)];
					if (rand == 0) {

						int randBuilding = (int)Random.Range (0, this.GetComponent<Buildings> ()._nbFarms - 1);
						GameObject buildingToDestroy = this.GetComponent<Buildings> ()._farms.transform.GetChild (randBuilding).gameObject;
						buildingToDestroy.GetComponent<Farm> ()._tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = false;
						this._Ressources [0]._Production -= buildingToDestroy.GetComponent<Farm> ()._production;
						this.GetComponent<Buildings> ()._nbFarms--;
						this._RessourceUsedPerTick [1] -= 2;

						DestroyObject (buildingToDestroy);
					}
					if (rand == 1) {
						
						int randBuilding = (int)Random.Range (0, this.GetComponent<Buildings> ()._nbWoodCutters - 1);
						GameObject buildingToDestroy = this.GetComponent<Buildings> ()._woodcutters.transform.GetChild (randBuilding).gameObject;
						buildingToDestroy.GetComponent<WoodcutterCamp> ()._tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = false;
						this._Ressources [1]._Production -= buildingToDestroy.GetComponent<WoodcutterCamp> ()._production;
						this.GetComponent<Buildings> ()._nbWoodCutters--;
						this._RessourceUsedPerTick [1] -= 2;

						DestroyObject (buildingToDestroy);
					}

					if (rand == 2) {

						int randBuilding = (int)Random.Range (0, this.GetComponent<Buildings> ()._nbGoldMines - 1);
						GameObject buildingToDestroy = this.GetComponent<Buildings> ()._goldmines.transform.GetChild (randBuilding).gameObject;
						buildingToDestroy.GetComponent<GoldMine> ()._tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = false;
						this._Ressources [2]._Production -= buildingToDestroy.GetComponent<GoldMine> ()._production;
						this.GetComponent<Buildings> ()._nbGoldMines--;
						this._RessourceUsedPerTick [2] -= 2;

						DestroyObject (buildingToDestroy);
					}
				}
				_Ressources [1]._Amount = 0;

			} else {
				// Use the required amount to feed every citizen
				_Ressources [1].UseAmount (_RessourceUsedPerTick [1]);
			}


			_timerTicks = TIME_BETWEEN_TICKS;
		} else {
			_timerTicks -= 1.0f * Time.deltaTime;
		}		
	}

}
		