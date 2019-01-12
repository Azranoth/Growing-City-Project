using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEvolution : MonoBehaviour {

	public static int _minDistanceToLimits = 5;

	public static int _nbCityLevels = 3;

	public GameObject[] _cityLevels = new GameObject [_nbCityLevels];
	public int[] _RequiredPopulationToEvolve = new int[_nbCityLevels];

	public int _levelATM = 1;

	// Use this for initialization
	void Start () {
		_RequiredPopulationToEvolve [0] = 100;
		_RequiredPopulationToEvolve [1] = 400;
		_RequiredPopulationToEvolve [2] = 750;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GeneratingCity(){
		
		int rand_x = (int) Random.Range(_minDistanceToLimits ,this.GetComponent<GameGrid> ().MAX_X - _minDistanceToLimits);
		int rand_y = (int) Random.Range(_minDistanceToLimits ,this.GetComponent<GameGrid> ().MAX_Y - _minDistanceToLimits);
		GameObject randomTile = GameObject.Find ("GridTile(" + rand_x + "," + rand_y + ")");

		while(randomTile.GetComponent<PlacingBuildingOnTile>()._blockType == "Water"){
			rand_x = (int) Random.Range(0,this.GetComponent<GameGrid> ().MAX_X);
			rand_y = (int) Random.Range(0,this.GetComponent<GameGrid> ().MAX_Y);
			randomTile = GameObject.Find ("GridTile(" + rand_x + "," + rand_y + ")");
		}

		GameObject CityModel = (GameObject) Instantiate (_cityLevels [0], new Vector3 (rand_x*this.GetComponent<GameGrid>().Scale, 0, rand_y*this.GetComponent<GameGrid>().Scale), Quaternion.identity);
		CityModel.name = "City";
		GameObject Tile = GameObject.Find ("GridTile(" + rand_x + "," + rand_y + ")");
		Tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;

		GameObject player = GameObject.Find ("Player");
		player.transform.position = new Vector3(rand_x, rand_y, player.transform.position.z);
	}

	public void EvolveCity(){
		_levelATM++;
		/* MODEL EVOLVE */
		//GameObject CityModel = GameObject.Find ("City");

		// TODO : unlocking

	}
}
