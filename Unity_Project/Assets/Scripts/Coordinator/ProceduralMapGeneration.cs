using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGeneration : MonoBehaviour {

	[Header("Block prefabs")]
	public GameObject _GroundBlock;
	public GameObject _WaterBlock;
	public GameObject _ForestBlock;
	[Space]

	public GameObject _MapBlocksParent;

	public static int _nbTreesPrefab = 3;
	public GameObject[] _treePrefabsList = new GameObject[_nbTreesPrefab];


	//public float Scale;
	public int MAX_X;
	public int MAX_Y;
	public float Scale;

	// Use this for initialization
	void Start () {
		
		//Scale = this.GetComponent<Grid> ().Scale;

		// TODO Remodoler le système de gen' tout entier
 
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

	public void MapGeneration(){

		if (this.GetComponent<GameGrid> () == null) {
			Debug.Log (" MISSING GRID SCRIPT ");
		}
		MAX_X = this.GetComponent<GameGrid> ().MAX_X;
		MAX_Y = this.GetComponent<GameGrid> ().MAX_Y;
		Scale = this.GetComponent<GameGrid> ().Scale;

		int nbGrounds = 0;
		int nbForests = 0;
		int nbWaters = 0;
		int rand = Random.Range (0,1);

		GameObject RelativeTile;

		if (_ForestBlock == null) {
			Debug.Log ("MapGeneration - MISSING FOREST BLOCK (Prefab error)");
			Application.Quit ();
		}
		if (_WaterBlock == null) {
			Debug.Log ("MapGeneration - MISSING WATER BLOCK (Prefab error)");
			Application.Quit ();
		}
		if (_GroundBlock == null) {
			Debug.Log ("MapGeneration - MISSING GROUND BLOCK (Prefab error)");
			Application.Quit ();
		}
		if (_MapBlocksParent == null) {
			Debug.Log ("MapGeneration - MISSING PARENT OBJECT (Prefab error)");
			Application.Quit ();
		}

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
					if ((nbGrounds + nbForests) >= nbWaters) { // MORE LAND BLOCKS AROUND
						rand = Random.Range (0, 100);
						if (rand <= 95) {
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
		/* --- Removing extra water blocks --- */
		/*
		bool onlyGround = true;
		for (int i = 0; i < MAX_X; i++) {
			for (int j = 0; j < MAX_Y; j++) {

				RelativeTile = GameObject.Find ("GridTile(" + i + "," + j + ")");
				if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType == "Water") {
					onlyGround = true;
					for (int k = i + 1; k >= i - 1; k--) {
						for (int h = j + 1; h >= j - 1; h--) {

							if (GameObject.Find ("GridTile(" + k + "," + h + ")").GetComponent<PlacingBuildingOnTile> ()._blockType == "Water") {
								onlyGround = false;
							}
						}
					}
					if (onlyGround == true) {
						BlockInstantiation("Ground", i, j);
					}
				}
			}
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void BlockInstantiation(string type, int x, int z){
		GameObject RelativeTile = GameObject.Find ("GridTile(" + x + "," + z + ")");

		if (RelativeTile == null) {
			Debug.Log ("BlockInstantiation - MISSING RELATIVE TILE (Prefab instantiation error)");
			Application.Quit ();
		}
		else{
			if (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType != "None") {
				Destroy (RelativeTile.GetComponent<PlacingBuildingOnTile> ()._Block.gameObject);
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "None";
			}
				
			if (type == "Forest") {
				GameObject GeneratedBlock = (GameObject)Instantiate (_ForestBlock, new Vector3 (x * Scale, -1, z * Scale), Quaternion.identity);
				//GeneratedBlock.transform.Rotate (-90.0f, 0.0f, 90.0f*Random.Range(-3,3)); // Blender export's shit + random Z rotate for diversity

				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "Forest";
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._Block = GeneratedBlock;

				GeneratedBlock.transform.parent = _MapBlocksParent.transform;

				ForestGeneration(GeneratedBlock);
			}
			if (type == "Ground") {
				GameObject GeneratedBlock = (GameObject)Instantiate (_GroundBlock, new Vector3 (x * Scale, -1, z * Scale), Quaternion.identity); 
				GeneratedBlock.transform.Rotate (-90.0f, 0.0f, 90.0f*Random.Range(-3,3)); // Blender export's shit + random Z rotate for diversity

				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "Ground";
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._Block = GeneratedBlock;

				GeneratedBlock.transform.parent = _MapBlocksParent.transform;
			}
			if (type == "Water") {
				GameObject GeneratedBlock = (GameObject)Instantiate (_WaterBlock, new Vector3 (x * Scale, -1, z * Scale), Quaternion.identity);

				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._blockType = "Water";
				RelativeTile.GetComponent<PlacingBuildingOnTile> ()._Block = GeneratedBlock;

				GeneratedBlock.transform.parent = _MapBlocksParent.transform;
			}
		}
	}

	private void ForestGeneration(GameObject block){

		float blockPosX = block.transform.localPosition.x;
		float blockPosZ = block.transform.localPosition.z;
		float blockSclX = block.transform.localScale.x;
		float blockSclZ = block.transform.localScale.z;

		//int nbTreesToGenerate =  Random.Range(6, 9);
		float x,z;

		GameObject emptyChildTrees = new GameObject ();
		emptyChildTrees.transform.parent = block.transform;

		for (int i = -1; i <= 1; ++i) {
			for (int j = -1; j <= 1; ++j) {

				x = blockPosX + i*(blockSclX/3.0f);
				z = blockPosZ + j*(blockSclZ/3.0f);

				x = Random.Range (x - (blockSclX / 6.0f) + (blockSclX / 10.0f), x + (blockSclX / 6.0f) - (blockSclX / 10.0f));
				z = Random.Range (z - (blockSclZ / 6.0f) + (blockSclX / 10.0f), z + (blockSclZ / 6.0f) - (blockSclX / 10.0f));

				int idTree = Random.Range (0, _nbTreesPrefab);

				GameObject newTree = Instantiate (_treePrefabsList [idTree], new Vector3 (x, -0.5f, z),Quaternion.identity);

				newTree.transform.Rotate(new Vector3(0.0f, Random.Range (0.0f, 360.0f), 0.0f));
				newTree.transform.parent = emptyChildTrees.transform;

			}

		}
	}

}
