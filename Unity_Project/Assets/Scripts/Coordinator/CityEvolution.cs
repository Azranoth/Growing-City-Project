using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEvolution : MonoBehaviour {

	public static int _nbCityLevels = 2;
	public GameObject[] _cityLevels = new GameObject [_nbCityLevels];
	public int _levelATM = 1;

	// Use this for initialization
	void Start () {
		GameObject CityModel = (GameObject) Instantiate (_cityLevels [0], new Vector3 (0, 0, 0), Quaternion.identity);
		CityModel.name = "City";
		GameObject Tile = GameObject.Find ("GridTile(" + CityModel.transform.position.x + "," + CityModel.transform.position.z);
		Tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EvolveCity(){
		_levelATM++;
		/* MODEL EVOLVE */
		//GameObject CityModel = GameObject.Find ("City");

	}
}
