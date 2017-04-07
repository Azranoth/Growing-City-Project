using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGeneration : MonoBehaviour {

	public GameObject _EarthBlock;
	public GameObject _WaterBlock;
	public GameObject _ForestBlock;

	public int MAX_X;
	public int MAX_Y;

	// Use this for initialization
	void Start () {
		MAX_X = this.GetComponent<Grid> ().MAX_X;
		MAX_Y = this.GetComponent<Grid> ().MAX_Y;


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int InstantiateBlock(string type){

	}
}
