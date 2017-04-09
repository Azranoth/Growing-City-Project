using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGeneration : MonoBehaviour {

	public GameObject _GroundBlock;
	public GameObject _WaterBlock;
	public GameObject _ForestBlock;

	public GameObject _MapBlocksParent;

	//public float Scale;
	public int MAX_X;
	public int MAX_Y;
	public float Scale;

	// Use this for initialization
	void Start () {
		
		//Scale = this.GetComponent<Grid> ().Scale;

		/** EBAUCHE D'ALGO
		 * 1st tile -> toujours ground
		 * pour chaque tile(i,j) (parcours bas-gauche -> haut droite) :
		 * 		nbGround = 0; nbForest = 0; nbWater = 0;
		 * 		Pour chaque case adjacente: 
		 * 			[ON VERIFIE UNIQUEMENT TOUT K<I+1 et H<J <=> gauche & bas]
		 * 			si Ground alors nbGround++
		 * 			si Forest alors nbForest++
		 * 			si Water alors  nbWater++
		 * 		Si (nbGround+nbForest) > nbWater
		 * 			Alors 90% de chances pour que (i,j) soit Ground
		 * 			Si (i,j) est Ground
		 * 				alors (15+15*nbForest)% de chances pour que (i,j) soit Forest
		 * 		Sinon
		 * 			95% de chances pour que (i,j) soit Water
		**/



	}

	public int MapGeneration(){

		MAX_X = this.GetComponent<Grid> ().MAX_X;
		MAX_Y = this.GetComponent<Grid> ().MAX_Y;
		Scale = this.GetComponent<Grid> ().Scale;

		int nbGrounds = 0;
		int nbForests = 0;
		int nbWaters = 0;
		int rand = Random.Range (0,1);
		Debug.Log ("prout");
		GameObject RelativeTile;

		/* For each tile (i,j) */
		for (int i = 0; i < MAX_X; i++) {
			for (int j = 0; j < MAX_Y; j++) {
				// Initial tile is ground or forest
				if (i == 0 && j == 0) {
					if (rand == 0) {
						BlockInstantiation ("Ground", 0, 0);
					} else if (rand == 1) {
						BlockInstantiation ("Forest", 0, 0);
					}
				} else {

					/* ---Counting number of Ground/Forest/Water blocks around the current one--- */
					nbGrounds = 0;
					nbForests = 0;
					nbWaters = 0;
					//Debug.Log ("pouet" + i+j);
					for (int k = i; k >= i-1; k--) {
						for (int h = j+1; h >= j-1; h--) {

							RelativeTile = GameObject.Find ("GridTile(" + k + "," + h + ")");
							if (RelativeTile != null) {
								if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Ground")
									nbGrounds++;
								else if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Forest")
									nbForests++;
								else if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Water")
									nbWaters++;
							}
						}
					}
					/*---------------------------------------------------------------------------*/

					/* ---Determining which kind of block it will be--- */
					if ((nbGrounds + nbForests) >= nbWaters) { // MORE GROUND BLOCKS AROUND
						rand = Random.Range (0, 100);
						if (rand <= 90) {
							rand = Random.Range (0, 100);
							if (rand < (15 + 15 * nbForests)) {
								/* create forest block */
								BlockInstantiation ("Forest", i, j);
							} else {
								/* create ground block */
								BlockInstantiation ("Ground", i, j);
							}
						} else {
							/* create water block */
							BlockInstantiation ("Water", i, j);

						}
					}
					else{ // MORE WATER BLOCKS AROUND
						rand = Random.Range (0, 100);
						if(rand <= 95){
							/* create waterblock */
							BlockInstantiation ("Water", i, j);
						}
						else{
							rand = Random.Range (0, 100);
							if (rand < (15 + 15 * nbForests)) {
								/* create forest block */
								BlockInstantiation ("Forest", i, j);
							} else {
								/* create ground block */
								BlockInstantiation ("Ground", i, j);
							}
						}
					}
					/*-------------------------------------------------*/



				}
			}
		}
		return 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void BlockInstantiation(string type, int x, int z){
		GameObject RelativeTile = GameObject.Find ("GridTile(" + x + "," + z + ")");
		if (RelativeTile != null) {
			if (type == "Forest") {
				GameObject GeneratedBlock = (GameObject) Instantiate (_ForestBlock, new Vector3 (x*Scale, -1, z*Scale), Quaternion.identity);
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "Forest";
				//RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;
				GeneratedBlock.transform.parent = _MapBlocksParent.transform;
			}
			if (type == "Ground") {
				GameObject GeneratedBlock = (GameObject) Instantiate (_GroundBlock, new Vector3 (x*Scale, -1, z*Scale), Quaternion.identity);
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "Ground";
				//RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;
				GeneratedBlock.transform.parent = _MapBlocksParent.transform;
			}
			if (type == "Water") {
				GameObject GeneratedBlock = (GameObject) Instantiate (_WaterBlock, new Vector3 (x*Scale, -1, z*Scale), Quaternion.identity);
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "Water";
				//RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockedTile = true;
				GeneratedBlock.transform.parent = _MapBlocksParent.transform;
			}
		}

	}

}
