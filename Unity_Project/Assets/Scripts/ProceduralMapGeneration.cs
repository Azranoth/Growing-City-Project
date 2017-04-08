using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGeneration : MonoBehaviour {

	public GameObject _GroundBlock;
	public GameObject _WaterBlock;
	public GameObject _ForestBlock;

	//public float Scale;
	public int MAX_X;
	public int MAX_Y;

	// Use this for initialization
	void Start () {
		MAX_X = this.GetComponent<Grid> ().MAX_X;
		MAX_Y = this.GetComponent<Grid> ().MAX_Y;
		//Scale = this.GetComponent<Grid> ().Scale;

		int randomFirstTile = Random.Range (0,1);
		int nbGrounds = 0;
		int nbForests = 0;
		int nbWaters = 0;
		int rand;
		GameObject RelativeTile;

		/* For each tile (i,j) */
		for (int i = 0; i < MAX_X; i++) {
			for (int j = 0; j < MAX_Y; j++) {
				// Initial tile is ground or forest
				if (i == 0 && j == 0) {
					if (randomFirstTile == 0) {
						BlockInstantiation ("Ground", 0, 0);
					} else if (randomFirstTile == 1) {
						BlockInstantiation ("Forest", 0, 0);
					}
				} else {

					/* ---Count number of Ground/Forest/Water blocks around the current one--- */
					nbGrounds = 0;
					nbForests = 0;
					nbWaters = 0;

					for (int k = i+1; k >= i-1; i--) {
						for (int h = j; h >= j-1; j--) {

							RelativeTile = GameObject.Find ("GridTile(" + k + "," + h + ")");
							if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Ground")
								nbGrounds++;
							else if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Forest")
								nbForests++;
							else if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Water")
								nbWaters++;
						}
					}
					/*------------------------------------------------------------------------*/

					/* ---Determining which kind of block it will be--- */
					if ((nbGrounds + nbForests) > nbWaters) { // MORE GROUND BLOCKS AROUND
						rand = Random.Range (0, 100);
						if (rand <= 80) {
							rand = Random.Range (0, 100);
							if (rand < (15 + 15 * nbForests)) {
								/* create forest block */
							} else {
								/* create ground block */
							}
						} else {
							/* create water block */
						}
					}
					else{ // MORE WATER BLOCKS AROUND
						rand = Random.Range (0, 100);
						if(rand <= 75){
							/* create waterblock */
						}
						else{
							rand = Random.Range (0, 100);
							if (rand < (15 + 15 * nbForests)) {
								/* create forest block */
							} else {
								/* create ground block */
							}
						}
					}
					/*-------------------------------------------------*/



				}
			}
		}

		/* EBAUCHE D'ALGO
		 * 1st tile -> toujours ground
		 * pour chaque tile(i,j) (parcours bas-gauche -> haut droite) :
		 * 		nbGround = 0; nbForest = 0; nbWater = 0;
		 * 		Pour chaque case adjacente: 
		 * 			[ON VERIFIE UNIQUEMENT TOUT K<I+1 et H<J <=> gauche & bas]
		 * 			si Ground alors nbGround++
		 * 			si Forest alors nbForest++
		 * 			si Water alors  nbWater++
		 * 		Si (nbGround+nbForest) > nbWater
		 * 			Alors 80% de chances pour que (i,j) soit Ground
		 * 			Si (i,j) est Ground
		 * 				alors (15+15*nbForest)% de chances pour que (i,j) soit Forest
		 * 		Sinon
		 * 			75% de chances pour que (i,j) soit Water
		*/



	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BlockInstantiation(string type, int x, int y){

	}
}
