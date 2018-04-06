using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			if (!_blockedTile && _blockType != "Water") {
				if (_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Amount >= _Coordinator.GetComponent<Buildings> ()._buildingCost) {
					// Consider the tile as occupied
					_blockedTile = true;
					// Create the object
					GameObject _PlacedBuilding = (GameObject)Instantiate (_Coordinator.GetComponent<Buildings> ()._Buildings [_Coordinator.GetComponent<Buildings> ()._indexBuildings],
						                             new Vector3 (transform.position.x, transform.position.y * (4 / 3), transform.position.z),
						                             Quaternion.identity);
					_PlacedBuilding.name = _Coordinator.GetComponent<Buildings> ()._Buildings [_Coordinator.GetComponent<Buildings> ()._indexBuildings].name;

					// Update block type + position in grid && number of corresponding buildings in the building script 
					if (_Coordinator.GetComponent<Buildings> ()._indexBuildings == 0) {
						_PlacedBuilding.GetComponent<Farm> ()._blockType = _blockType;
						_PlacedBuilding.GetComponent<Farm> ()._tile = this.gameObject;

						this._Coordinator.GetComponent<Buildings> ()._nbFarms++;
					}

					if (_Coordinator.GetComponent<Buildings> ()._indexBuildings == 1) {
						_PlacedBuilding.GetComponent<WoodcutterCamp> ()._blockType = _blockType;
						_PlacedBuilding.GetComponent<WoodcutterCamp> ()._tile = this.gameObject;

						this._Coordinator.GetComponent<Buildings> ()._nbWoodCutters++;
					}

					// Update corresponding ressource production & consuption
					_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [1].UseAmount (_Coordinator.GetComponent<Buildings> ()._buildingCost);
					_Coordinator.GetComponent<RessourcesManagement> ()._RessourceUsedPerTick [1] += 2;
				}
			}
		}
	}

}
