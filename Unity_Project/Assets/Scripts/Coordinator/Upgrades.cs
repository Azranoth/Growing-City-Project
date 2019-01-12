using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour
{

	//--- UPGRADES MAX LEVEL
	public int _roadsMaxLevel = 10;
	//----//



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	/*
	 * onRoadsUpgrade()
	 * brief : boost city's max reacheable distances -> near buildings prod slightly upgraded && buildings buildable on further tiles
	 */
	public void RoadsUpgrade(int price){

		Debug.Log ("roads upgrade method called");
		this.GetComponent<RessourcesManagement> ()._Ressources [2].UseAmount (price);
		this.GetComponent<RessourcesManagement> ()._MaxDistance += 0.2f;

		// CURRENT PROD UPDATE TODO
	}
}

