using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour {

	private static int _nbBuildings = 2;
	public GameObject[] _Buildings = new GameObject[_nbBuildings];
	public int _indexBuildings = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Alpha1)) {
			_indexBuildings = 0;
		}
		if (Input.GetKey (KeyCode.Alpha2)) {
			_indexBuildings = 1;
		}
		if (Input.GetKey (KeyCode.Alpha3)) {
			_indexBuildings = 2;
		}
		if (Input.GetKey (KeyCode.Alpha4)) {
			_indexBuildings = 3;
		}
	}
}
