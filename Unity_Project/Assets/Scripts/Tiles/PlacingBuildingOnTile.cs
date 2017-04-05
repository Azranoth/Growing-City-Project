using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingBuildingOnTile : MonoBehaviour {

	public GameObject _Coordinator;
	public bool _blockedTile = false;


	// Use this for initialization
	void Start () {
		_Coordinator = GameObject.Find ("Coordinator");
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnMouseOver(){
		if (Input.GetButtonDown ("mouse 1")) {
			if (!_blockedTile) {
				_blockedTile = true;
				GameObject _PlacedBuilding = (GameObject)Instantiate (_Coordinator.GetComponent<Buildings> ()._Buildings [_Coordinator.GetComponent<Buildings> ()._indexBuildings],
					                            new Vector3 (transform.position.x, transform.position.y * (4 / 3), transform.position.z),
					                            Quaternion.identity);
				_PlacedBuilding.name = _Coordinator.GetComponent<Buildings> ()._Buildings [_Coordinator.GetComponent<Buildings> ()._indexBuildings].name;
			}
		}
	}

}
