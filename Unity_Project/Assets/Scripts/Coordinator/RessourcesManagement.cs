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
			//Debug.Log ("Produce ressource "+ _Name + ", produces " + _Production + ", Used " + amount + ", have " + _Amount);
		}
	}

	/* ----- STATIC VARS -----*/
	public static int _nbRessources = 3;
	public static float TIME_BETWEEN_TICKS = 2.5f;

	public Ressource[] _Ressources = new Ressource[_nbRessources];

	public int[] _RessourceUsedPerTick = new int[_nbRessources];
	public float _timerTicks = TIME_BETWEEN_TICKS;

	public float _MaxDistance;

	public bool _bankrupt = true;

	// Variables used to trade ressources
	public int _foodToWood = 0;				// Food amount to be traded for wood
	public int _woodToFood = 0;				// Wood amount to be traded for food
	public int _goldToFood = 0;				// gold amount to be traded for food
	public int _goldToWood = 0;				// gold amount to be traded for wood

	public float _tradingTax = 1.25f;		// Tax applied to ressources (mult *)
	public int _tradingCapacityPerTick = 0; // Amount of ressources that can be traded in a single tick; more posts -> more capacity


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

			// If the town still have some money, continue trading operations
			if (!_bankrupt) {
				trading ();
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

				if (this.GetComponent<Buildings> ()._nbTradingPosts > 0) {
					buildingTypesExisting.Add(3);
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
						this._RessourceUsedPerTick [1] -= 2;

						DestroyObject (buildingToDestroy);
					}

					if (rand == 3) {

						int randBuilding = (int)Random.Range (0, this.GetComponent<Buildings> ()._nbTradingPosts - 1);
						GameObject buildingToDestroy = this.GetComponent<Buildings> ()._tradingposts.transform.GetChild (randBuilding).gameObject;
						buildingToDestroy.GetComponent<TradingPost> ()._tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = false;
						//this._Ressources [2]._Production -= buildingToDestroy.GetComponent<GoldMine> ()._production;
						this.GetComponent<Buildings> ()._nbTradingPosts--;
						this._RessourceUsedPerTick [1] -= 2;
						this._RessourceUsedPerTick [2] -= 2;

						DestroyObject (buildingToDestroy);
					}
				}
				_Ressources [1]._Amount = 0;

			} else {
				// Use the required amount to maintain buildings in a good state
				_Ressources [1].UseAmount (_RessourceUsedPerTick [1]);
			}

			// ----- GOLD
			// if gold is unsufficent
			if (_Ressources [2]._Amount < _RessourceUsedPerTick [2]) {
				// The city goes bankrupt
				_bankrupt = true;
				_Ressources [2]._Amount = 0;

			} else {
				
				_bankrupt = false;
				_Ressources [2].UseAmount (_RessourceUsedPerTick [2]);
			}


			_timerTicks = TIME_BETWEEN_TICKS;
		} else {
			_timerTicks -= 1.0f * Time.deltaTime;
		}		
	}


	/*
	 * function trading()
	 * Manage trading operations in progress. Add obtained ressources & remove payed ressources (including taxes) for each kind of possible transaction
	 */
	void trading(){

		// If a trade between food & wood has been initialized
		if (_foodToWood > 0) {
			// If the trading posts are unable to trade the whole amount of ressources in a single transaction
			if (_foodToWood > _tradingCapacityPerTick) {

				_Ressources [0].UseAmount ( (int)(_tradingCapacityPerTick * _tradingTax) );
				_Ressources [1]._Amount += _tradingCapacityPerTick;
				_foodToWood -= _tradingCapacityPerTick;
			} else {

				_Ressources [0].UseAmount ( (int)(_foodToWood * _tradingTax) );
				_Ressources [1]._Amount += _foodToWood;
				_foodToWood = 0;
			}
		}

		// If a trade between wood & food has been initialized
		if (_woodToFood > 0) {
			// If the trading posts are unable to trade the whole amount of ressources in a single transaction
			if (_woodToFood > _tradingCapacityPerTick) {

				_Ressources [1].UseAmount ( (int)(_tradingCapacityPerTick * _tradingTax) );
				_Ressources [0]._Amount += _tradingCapacityPerTick;
				_woodToFood -= _tradingCapacityPerTick;
			} else {

				_Ressources [1].UseAmount ( (int)(_foodToWood * _tradingTax) );
				_Ressources [0]._Amount += _foodToWood;
				_woodToFood = 0;
			}
		}

		// If a trade between food & wood has been initialized
		if (_goldToFood > 0) {
			// If the trading posts are unable to trade the whole amount of ressources in a single transaction
			if (_goldToFood > _tradingCapacityPerTick) {

				_Ressources [2].UseAmount ( (int)(_tradingCapacityPerTick * _tradingTax) );
				_Ressources [0]._Amount += _tradingCapacityPerTick;
				_goldToFood -= _tradingCapacityPerTick;
			} else {

				_Ressources [2].UseAmount ( (int)(_foodToWood * _tradingTax) );
				_Ressources [0]._Amount += _foodToWood;
				_goldToFood = 0;
			}
		}

		if (_goldToWood > 0) {
			// If the trading posts are unable to trade the whole amount of ressources in a single transaction
			if (_goldToWood > _tradingCapacityPerTick) {

				_Ressources [2].UseAmount ( (int)(_tradingCapacityPerTick * _tradingTax) );
				_Ressources [1]._Amount += _tradingCapacityPerTick;
				_goldToWood -= _tradingCapacityPerTick;
			} else {

				_Ressources [2].UseAmount ( (int)(_foodToWood * _tradingTax) );
				_Ressources [1]._Amount += _foodToWood;
				_goldToWood = 0;
			}
		}
	}
			
}
		