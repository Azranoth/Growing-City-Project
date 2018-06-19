using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDScript : MonoBehaviour {

	public Text _foodCount;
	public Text _woodCount;
	public Text _goldCount;
	public Text _popCount;

	public GameObject _selectedFarm;
	public GameObject _selectedWoodcutter;
	public GameObject _selectedGoldmine;

	public GameObject _coordinator;

	// Use this for initialization
	void Start () {

		_foodCount.text = "25";
		_woodCount.text = "25";
		_goldCount.text = "25";
		_popCount.text  = "5" ;
	}
	
	// Update is called once per frame
	void Update () {
		_foodCount.text = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [0]._Amount.ToString();
		_woodCount.text = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Amount.ToString();
		_goldCount.text = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [2]._Amount.ToString();
		_popCount.text  = _coordinator.GetComponent<PopulationGrowth> ()._Population.ToString ();

		updateBuildingSelection ();
	}

	void updateBuildingSelection(){
		if (Input.GetKey (KeyCode.Alpha1)) {
			_selectedFarm.SetActive (true);
			_selectedWoodcutter.SetActive (false);
			_selectedGoldmine.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			_selectedFarm.SetActive (false);
			_selectedWoodcutter.SetActive (true);
			_selectedGoldmine.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Alpha3)) {
			_selectedFarm.SetActive (false);
			_selectedWoodcutter.SetActive (false);
			_selectedGoldmine.SetActive (true);
		}
	}
}
