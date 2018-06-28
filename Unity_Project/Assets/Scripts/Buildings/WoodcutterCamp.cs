using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcutterCamp : MonoBehaviour {

	public int 		_production = 10;	   // Ressources produced by tick
	public string	_blockType = "Ground"; // Default
	public GameObject _tile;		       // Tile object on which the woodcutter is built

	public GameObject _City;
	public GameObject _Coordinator;

	protected GameObject _buildLoadingBarBG;	// background of the building progression bar
	protected GameObject _buildLoadingBarFront;	// foreground of the building progression bar

	protected bool _buildDone = false;			// Has the building been build yet?
	static protected float _buildTime = 2.0f;   // Time required to build it
	protected float _timer = 0.0f;				// Time elapsed since the creation

	// Use this for initialization
	void Start () {

		transform.parent = GameObject.Find ("WoodCutters").transform;

		initLoadingBar ();
	}

	// Update is called once per frame
	void Update () {
		// While the camp is not built, it does not generate any ressources
		if (!_buildDone) {
			if (_timer < _buildTime) {
				_timer += 1.0f * Time.deltaTime;
				// Update the loading bar
				_buildLoadingBarFront.transform.localScale = new Vector3 (0.06f, 1.0f, (_timer / _buildTime) * 0.3f);

			} else {
				_buildDone = true;
				removeLoadingBar ();
				initProduction ();
			}
		}
	}


	/*
	 * function initLoadingBar()
	 * Initialize a progression bar upon the building -> get full when the camp is built & functionnal (ie producing ressources)
	 */
	void initLoadingBar(){
		// Create the loading bar's background
		_buildLoadingBarBG = GameObject.CreatePrimitive (PrimitiveType.Plane);
		_buildLoadingBarBG.transform.localScale = new Vector3 (0.06f, 1.0f, 0.3f); 
		_buildLoadingBarBG.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
		_buildLoadingBarBG.transform.Rotate (new Vector3 (0.0f, -45.0f, 90.0f));
		_buildLoadingBarBG.transform.parent = this.transform;

		// Create the loading bar's progression object
		_buildLoadingBarFront = GameObject.CreatePrimitive (PrimitiveType.Plane);
		_buildLoadingBarFront.GetComponent<Renderer> ().material = new Material (Shader.Find ("Standard"));
		_buildLoadingBarFront.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (1.0f, (float)(227.0f / 255.0f), 0.0f));
		_buildLoadingBarFront.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");

		_buildLoadingBarFront.transform.localScale = new Vector3 (0.06f, 1.0f, 0.0f); 
		_buildLoadingBarFront.transform.position = new Vector3 (this.transform.position.x - 0.01f, this.transform.position.y + 1.0f, this.transform.position.z - 0.01f);
		_buildLoadingBarFront.transform.Rotate (new Vector3 (0.0f, -45.0f, 90.0f));
		_buildLoadingBarFront.transform.parent = this.transform;
	}


	/*
	 * function removeLoadingBar()
	 * Remove the progression bar once the camp is built
	 */
	void removeLoadingBar(){
		Destroy (_buildLoadingBarBG);
		Destroy (_buildLoadingBarFront);

	}

	/*
	 * function initProduction()
	 * Initialize the ressources production of this camp & begin the production
	 */
	void initProduction(){

		_City = GameObject.Find ("City");
		if (_City == null) {
			Debug.Log ("MISSING CITY OBJECT - Generation failed");
			Application.Quit ();
		}

		_Coordinator = GameObject.Find ("Coordinator");


		// Calculate real amount produced per tick according to the distance between the farm & the city
		int distanceToCity = (int) (Mathf.Sqrt( Mathf.Pow(_City.transform.position.x - this.transform.position.x,2)
			+ Mathf.Pow(_City.transform.position.z - this.transform.position.z,2))/2.0f);

		// Increased production if the woodcutter is located on a forest block
		if (this._blockType == "Forest")
			this._production = (int)((float)this._production*1.5);

		// Modify global coordinator food production on build
		_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Production += 
			(int)(this._production / (distanceToCity/_Coordinator.GetComponent<RessourcesManagement> ()._MaxDistance));

		_Coordinator.GetComponent<Buildings> ()._isBuildingCamp = false;
	}

}
