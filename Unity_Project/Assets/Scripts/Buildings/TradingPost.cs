using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPost : MonoBehaviour {

	public GameObject _tile;					// Tile object on which the farm is built
	public string	_blockType = "Ground"; 		// Default

	public GameObject _City;					
	public GameObject _Coordinator;
				   		

	protected GameObject _buildLoadingBarBG;	// background of the building progression bar
	protected GameObject _buildLoadingBarFront;	// foreground of the building progression bar

	protected bool _buildDone = false;			// Has the building been build yet?
	static protected float _buildTime = 3.0f;	// Time required to build it
	protected float _timer = 0.0f;				// Time elapsed since the creation

	public int _tradingCapacity = 5;


	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find ("TradingPosts").transform;

		initLoadingBar ();

	}
	
	// Update is called once per frame
	void Update () {
		// While the post is not built, it does not generate any ressources
		if (!_buildDone) {
			if (_timer < _buildTime) {
				_timer += 1.0f * Time.deltaTime;
				// Update the loading bar
				if(_timer > _buildTime)
					_buildLoadingBarFront.transform.localScale = new Vector3 (0.06f, 1.0f, 0.3f);
				else
					_buildLoadingBarFront.transform.localScale = new Vector3 (0.06f, 1.0f, (_timer / _buildTime) * 0.3f);

			} else {
				_buildDone = true;
				removeLoadingBar ();
				initFunction ();
			}
		}
	}


	/*
	 * function initLoadingBar()
	 * Initialize a progression bar upon the building -> get full when the mine is built & functionnal
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
	 * Remove the progression bar once the farm is built
	 */
	void removeLoadingBar(){
		Destroy (_buildLoadingBarBG);
		Destroy (_buildLoadingBarFront);

	}

	/*
	 * function initFunction()
	 * Initialize the trading capacity of this post
	 */
	void initFunction(){

		_City = GameObject.Find ("City");
		if (_City == null) {
			Debug.Log ("MISSING CITY OBJECT - Generation failed");
			Application.Quit ();
		}

		_Coordinator = GameObject.Find ("Coordinator");

		// Calculate real amount produced per tick according to the distance between the farm & the city
		int _distanceToCity = (int) (Mathf.Sqrt( Mathf.Pow(_City.transform.position.x - this.transform.position.x,2)
			+ Mathf.Pow(_City.transform.position.z - this.transform.position.z,2))/2.0f);

		// If a trading post is near sea, efficiency++ 
		//TODO

		_Coordinator.GetComponent<RessourcesManagement>()._tradingCapacityPerTick += 
			(int)(this._tradingCapacity / (_distanceToCity / _Coordinator.GetComponent<RessourcesManagement> ()._MaxDistance));
		_Coordinator.GetComponent<Buildings> ()._isBuildingPost = false;
	}
}
