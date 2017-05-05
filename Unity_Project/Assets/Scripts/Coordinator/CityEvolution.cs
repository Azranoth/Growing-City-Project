using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEvolution : MonoBehaviour {

	public static int _nbCityLevels = 2;

	public GameObject _Player;
	public GameObject[] _cityLevels = new GameObject [_nbCityLevels];
	public int[] _RequiredPopulationToEvolve = new int[_nbCityLevels];

	public int _levelATM = 1;

	// Use this for initialization
	void Start () {
		_RequiredPopulationToEvolve [0] = 100;
		_RequiredPopulationToEvolve [1] = 400;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GeneratingCity(){
		
		int rand_x = (int) Random.Range(0,this.GetComponent<Grid> ().MAX_X);
		int rand_y = (int) Random.Range(0,this.GetComponent<Grid> ().MAX_Y);
		GameObject randomTile = GameObject.Find ("GridTile(" + rand_x + "," + rand_y + ")");

		while(randomTile.GetComponent<PlacingBuildingOnTile>()._blockType == "Water"){
			rand_x = (int) Random.Range(0,this.GetComponent<Grid> ().MAX_X);
			rand_y = (int) Random.Range(0,this.GetComponent<Grid> ().MAX_Y);
			randomTile = GameObject.Find ("GridTile(" + rand_x + "," + rand_y + ")");
		}

		GameObject CityModel = (GameObject) Instantiate (_cityLevels [0], new Vector3 (rand_x*this.GetComponent<Grid>().Scale, 0, rand_y*this.GetComponent<Grid>().Scale), Quaternion.identity);
		CityModel.name = "City";
		GameObject Tile = GameObject.Find ("GridTile(" + rand_x + "," + rand_y + ")");
		Tile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;

		//TODO
		// Déplacer la vue du joueur sur la ville
	}

	public void EvolveCity(){
		_levelATM++;
		/* MODEL EVOLVE */
		//GameObject CityModel = GameObject.Find ("City");

	}
}
