using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDScript : MonoBehaviour {

	public Text _foodCount;
	public Text _woodCount;
	public Text _goldCount;
	public Text _popCount;

	public GameObject _coordinator;

	// Use this for initialization
	void Start () {
		_coordinator = GameObject.Find ("Coordinator");
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
	}
}
