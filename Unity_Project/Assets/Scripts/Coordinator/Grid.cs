using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public float Scale = 4.0f;			// Size of the grid's tiles.
	public int MAX_X = 35;		// Maximum value of X on the grid
	public int MAX_Y = 35;		// Maximum value of Y on the grid

	public GameObject _Tile;	
	//public GameObject _ObjToRemove;



	// Use this for initialization
	void Start () {
		/* Generate a (MAX_X*MAX_Y) grid of (Scale*Scale) tiles */
		for (int i = 0; i < MAX_X; i++) {
			for (int j = 0; j < MAX_Y; j++) {
				Vector3 tileSpawnPosition = new Vector3 (i*Scale, 0.0f, j*Scale);
				GameObject Tile = Instantiate (_Tile, tileSpawnPosition, Quaternion.identity) as GameObject;
				Tile.name = "GridTile(" + (Tile.transform.position.x/Scale) + "," + (Tile.transform.position.z/Scale) + ")";
				Tile.transform.parent = GameObject.Find ("GridTiles").transform;

			}
		}
		this.GetComponent<ProceduralMapGeneration> ().MapGeneration ();
		this.GetComponent<CityEvolution> ().GeneratingCity ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
