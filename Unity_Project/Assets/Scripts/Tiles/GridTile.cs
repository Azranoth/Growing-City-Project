using UnityEngine;
using System.Collections;

/*
 * Source code linked to every tile of the grid, highlighting it when the player's mouse is over it
 */
public class GridTile : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Color color = GetComponent<Renderer> ().material.color;
		color.a = 0.0f;
	}

	void OnMouseOver(){

		if (this.GetComponent<PlacingBuildingOnTile> ()._Coordinator.GetComponent<Buildings> ()._outOfMenu) {
			Color color = GetComponent<Renderer> ().material.color;
			color.a = 50.0f;

			GetComponent<Renderer> ().material.SetColor ("_Color", color);
		}
	}

	void OnMouseExit(){
		Color color = GetComponent<Renderer> ().material.color;
		color.a = 0.0f;

		GetComponent<Renderer> ().material.SetColor ("_Color", color);
	}
}

