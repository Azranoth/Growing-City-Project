using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacingBuildingOnTile : MonoBehaviour {

	public GameObject _Coordinator;
	public GameObject _Block;

	public bool _blockedTile = false;
	public string _blockType = "None";		// "Forest", "Ground" or "Water"



	// Use this for initialization
	void Start () {
		_Coordinator = GameObject.Find ("Coordinator");
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	void OnMouseOver(){
		if (Input.GetButtonDown ("mouse 1")) {

			// Place building only if 1. Player is not browsing menus 2. Tile is free to build & not water type 3. the mouse pointer is not over a UI object.
			if (_Coordinator.GetComponent<Buildings>()._outOfMenu && !_blockedTile && _blockType != "Water" && !EventSystem.current.IsPointerOverGameObject() ) {
				if (_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Amount >= _Coordinator.GetComponent<Buildings> ()._buildingCost) {
					

					if (_Coordinator.GetComponent<Buildings> ()._indexBuildings == 0 && !_Coordinator.GetComponent<Buildings> ()._isBuildingFarm) {

						this._Coordinator.GetComponent<Buildings> ()._nbFarms++;

						// Consider the tile as occupied
						_blockedTile = true;
						// Create the object
						GameObject _PlacedBuilding = (GameObject)Instantiate (_Coordinator.GetComponent<Buildings> ()._Buildings [0],
							new Vector3 (transform.position.x, -0.3f, transform.position.z),
							Quaternion.identity);
						_PlacedBuilding.transform.Rotate (new Vector3 (0.0f, 90.0f, 0.0f));
						_PlacedBuilding.name = _Coordinator.GetComponent<Buildings> ()._Buildings [0].name;


						this._Coordinator.GetComponent<Buildings> ()._isBuildingFarm = true;
						// Update block type + position in grid && number of corresponding buildings in the building script 
						_PlacedBuilding.GetComponent<Farm> ()._blockType = _blockType;
						_PlacedBuilding.GetComponent<Farm> ()._tile = this.gameObject;
					}

					else if (_Coordinator.GetComponent<Buildings> ()._indexBuildings == 1 && !_Coordinator.GetComponent<Buildings> ()._isBuildingCamp) {

						this._Coordinator.GetComponent<Buildings> ()._nbWoodCutters++;

						// Consider the tile as occupied
						_blockedTile = true;
						// Create the object
						GameObject _PlacedBuilding = (GameObject)Instantiate (_Coordinator.GetComponent<Buildings> ()._Buildings [1],
							new Vector3 (transform.position.x, transform.position.y * (4 / 3), transform.position.z),
							Quaternion.identity);
						_PlacedBuilding.name = _Coordinator.GetComponent<Buildings> ()._Buildings [1].name;


						this._Coordinator.GetComponent<Buildings> ()._isBuildingCamp = true;
						// Update block type + position in grid && number of corresponding buildings in the building script 
						_PlacedBuilding.GetComponent<WoodcutterCamp> ()._blockType = _blockType;
						_PlacedBuilding.GetComponent<WoodcutterCamp> ()._tile = this.gameObject;
					}

					else if (_Coordinator.GetComponent<Buildings> ()._indexBuildings == 2 && !_Coordinator.GetComponent<Buildings> ()._isBuildingMine) {

						this._Coordinator.GetComponent<Buildings> ()._nbGoldMines++;

						// Consider the tile as occupied
						_blockedTile = true;
						// Create the object
						GameObject _PlacedBuilding = (GameObject)Instantiate (_Coordinator.GetComponent<Buildings> ()._Buildings [2],
							new Vector3 (transform.position.x, transform.position.y * (4 / 3), transform.position.z),
							Quaternion.identity);
						_PlacedBuilding.name = _Coordinator.GetComponent<Buildings> ()._Buildings [2].name;

						this._Coordinator.GetComponent<Buildings> ()._isBuildingMine = true;
						// Update block type + position in grid && number of corresponding buildings in the building script 
						_PlacedBuilding.GetComponent<GoldMine> ()._blockType = _blockType;
						_PlacedBuilding.GetComponent<GoldMine> ()._tile = this.gameObject;
					}

					else if (_Coordinator.GetComponent<Buildings> ()._indexBuildings == 3 && !_Coordinator.GetComponent<Buildings> ()._isBuildingPost) {
						
						this._Coordinator.GetComponent<Buildings> ()._isBuildingPost = true;

						// Consider the tile as occupied
						_blockedTile = true;
						// Create the object
						GameObject _PlacedBuilding = (GameObject)Instantiate (_Coordinator.GetComponent<Buildings> ()._Buildings [3],
							new Vector3 (transform.position.x, transform.position.y * (4 / 3), transform.position.z),
							Quaternion.identity);
						_PlacedBuilding.name = _Coordinator.GetComponent<Buildings> ()._Buildings [3].name;

						// Update block type + position in grid && number of corresponding buildings in the building script 
						_PlacedBuilding.GetComponent<TradingPost> ()._blockType = _blockType;
						_PlacedBuilding.GetComponent<TradingPost> ()._tile = this.gameObject;

						_Coordinator.GetComponent<RessourcesManagement> ()._RessourceUsedPerTick [2] += 3;

						this._Coordinator.GetComponent<Buildings> ()._nbTradingPosts++;

						if (this._Coordinator.GetComponent<Buildings> ()._nbTradingPosts == 1) {
							this._Coordinator.GetComponent<Buildings> ().activateTradeButton ();
						}
					}

					// Update corresponding ressource production & consuption
					_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [1].UseAmount (_Coordinator.GetComponent<Buildings> ()._buildingCost);

					_Coordinator.GetComponent<RessourcesManagement> ()._RessourceUsedPerTick [1] += 2;

				}
			}
		}
	}

}
