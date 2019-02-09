using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEvolution : MonoBehaviour {

	public static int _minDistanceToLimits = 5;
	public int _cityX = -1;
	public int _cityY = -1;
	public GameObject _cityModel;

	public static int _nbCityLevels = 3;

	public GameObject[] _cityLevels = new GameObject [_nbCityLevels];
	public int[] _RequiredPopulationToEvolve = new int[_nbCityLevels];

	public int _levelATM = 1;

	// Use this for initialization
	void Start () {
		_RequiredPopulationToEvolve [0] = 100;
		_RequiredPopulationToEvolve [1] = 400;
		_RequiredPopulationToEvolve [2] = 750;
		_levelATM = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (_levelATM < _nbCityLevels && this.GetComponent<PopulationGrowth> ()._Population > _RequiredPopulationToEvolve [_levelATM-1]) {
			EvolveCity ();
		}
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
		_cityX = rand_x;
		_cityY = rand_y;
		_cityModel = (GameObject) Instantiate (_cityLevels [0], new Vector3 (_cityX*this.GetComponent<GameGrid>().Scale, 0, _cityY*this.GetComponent<GameGrid>().Scale), Quaternion.identity);
		_cityModel.name = "City";
		GameObject Tile = GameObject.Find ("GridTile(" + _cityX + "," + _cityY + ")");
		Tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;

		if (Tile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Forest") {
			Transform trees = Tile.GetComponent<PlacingBuildingOnTile> ()._Block.transform.GetChild(0).transform;
			for (int i = 0; i < trees.childCount; ++i) {
				Destroy (trees.GetChild(i).gameObject);

			}
			Destroy (trees.gameObject);
		}
		GameObject player = GameObject.Find ("Player");
		player.transform.position = new Vector3(_cityX, _cityY, player.transform.position.z);
	}

	public void EvolveCity(){
		_levelATM++;
		/* MODEL EVOLVE */
		//GameObject CityModel = GameObject.Find ("City");

		// TODO : unlocking

	}
}
