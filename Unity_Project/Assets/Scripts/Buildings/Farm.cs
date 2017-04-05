using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

	public int _production = 10;

	public GameObject _City;
	public GameObject _Coordinator;

	// Use this for initialization
	void Start () {

		GameObject _City = GameObject.Find ("City");
		GameObject _Coordinator = GameObject.Find ("Coordinator");


		int distanceToCity = (int) (Mathf.Sqrt( Mathf.Pow(_City.transform.position.x - this.transform.position.x,2)
			+ Mathf.Pow(_City.transform.position.z - this.transform.position.z,2))/2.0f);
		_Coordinator.GetComponent<RessourcesManagement> ()._Ressources [0]._Production += (int)(this._production / distanceToCity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
