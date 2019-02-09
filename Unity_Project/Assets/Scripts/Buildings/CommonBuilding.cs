using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBuilding : MonoBehaviour {

	protected GameObject _buildLoadingBarBG;	// background of the building progression bar
	protected GameObject _buildLoadingBarFront;	// foreground of the building progression bar

	public bool _buildDone = false;			// Has the building been build yet?
	public bool _prodInitDone = false;		// Has the production been initialized yet?

	protected float _timer = 0.0f;			// Time elapsed since the creation
	protected float _buildTime = -1.0f;

	// Use this for initialization
	void Start () {
		initLoadingBar ();
	}
	
	// Update is called once per frame
	void Update () {
		// While the building is not built, it does not generate any ressources/services
		if (!_buildDone) {
			if (_buildTime > 0.0f && _timer < _buildTime) {
				_timer += 1.0f * Time.deltaTime;
				// Update the loading bar
				_buildLoadingBarFront.transform.localScale = new Vector3 (0.06f, 1.0f, (_timer / _buildTime) * 0.3f);

			} else {

				if (this.GetComponent<Farm> ()) {
					this.GetComponent<Farm> ().initProduction ();
				}
				if (this.GetComponent<WoodcutterCamp> ()) {
					this.GetComponent<WoodcutterCamp> ().initProduction ();
				}
				if (this.GetComponent<GoldMine> ()) {
					this.GetComponent<GoldMine> ().initProduction ();
				}
				if (this.GetComponent<TradingPost> ()) {
					this.GetComponent<TradingPost> ().initFunction ();
				}
				removeLoadingBar ();
				_buildDone = true;

			}
		}
	}

	/*
	 * function initLoadingBar()
	 * Initialize a progression bar upon the building -> get full when the farm is built & functionnal (ie producing ressources)
	 */
	public void initLoadingBar(){
		GameObject emptyParent = new GameObject ();
		emptyParent.transform.parent = this.transform;

		// Create the loading bar's background
		_buildLoadingBarBG = GameObject.CreatePrimitive (PrimitiveType.Plane);
		_buildLoadingBarBG.transform.localScale = new Vector3 (0.06f, 1.0f, 0.3f); 
		_buildLoadingBarBG.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
		_buildLoadingBarBG.transform.Rotate (new Vector3 (0.0f, -45.0f, 90.0f));

		_buildLoadingBarBG.transform.SetParent (emptyParent.transform);

		// Create the loading bar's progression object
		_buildLoadingBarFront = GameObject.CreatePrimitive (PrimitiveType.Plane);
		_buildLoadingBarFront.GetComponent<Renderer> ().material = new Material (Shader.Find ("Standard"));
		_buildLoadingBarFront.GetComponent<Renderer> ().material.color = new Color (1.0f, (float)(227.0f / 255.0f), 0.0f);
		_buildLoadingBarFront.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");
		_buildLoadingBarFront.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (1.0f, (float)(227.0f / 255.0f), 0.0f));



		_buildLoadingBarFront.transform.localScale = new Vector3 (0.06f, 1.0f, 0.0f); 
		_buildLoadingBarFront.transform.position = new Vector3 (this.transform.position.x - 0.01f, this.transform.position.y + 1.0f, this.transform.position.z - 0.01f);
		_buildLoadingBarFront.transform.Rotate (new Vector3 (0.0f, -45.0f, 90.0f));

		_buildLoadingBarFront.transform.SetParent (emptyParent.transform);

	}


	/*
	 * function removeLoadingBar()
	 * Remove the progression bar once the mine is built
	 */
	public void removeLoadingBar(){
		Destroy (_buildLoadingBarBG);
		Destroy (_buildLoadingBarFront);
		Destroy (this.transform.GetChild (this.transform.childCount-1).gameObject);

	}

	public void initBuildTime(float buildTime){
		_buildTime = buildTime;
	}

	void OnTriggerStay(Collider tree){
		if (tree.gameObject.CompareTag("Tree")) {
			Destroy (tree.gameObject);
			Debug.Log ("tree destroyed");
		}
	}
}
