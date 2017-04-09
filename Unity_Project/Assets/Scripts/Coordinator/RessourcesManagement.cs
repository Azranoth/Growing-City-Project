using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManagement : MonoBehaviour {

	public class Ressource
	{

		public string _Name;
		public int _Amount = 25;
		public int _Production = 0;

		public Ressource(string name){
			_Name = name;
		}

		public void UseAmount(int amount){
			if (_Amount < amount) {
				_Amount = 0;
			} else {
				_Amount -= amount;
			}
			Debug.Log ("Produce " + _Production + ", Used " + amount + ", have " + _Amount);
		}
	}
	/* ----- STATIC VARS -----*/
	public static int _nbRessources = 1;
	public static float TIME_BETWEEN_TICKS = 2.5f;

	public Ressource[] _Ressources = new Ressource[_nbRessources];

	public int[] _RessourceUsedPerTick = new int[_nbRessources];
	public float _timerTicks = TIME_BETWEEN_TICKS;

	public float _MaxDistance;

	/*
	 * 0 : food
	 * 1 : wood
	 * 2 : stone
	 * 3 : gold
	 */
	// Use this for initialization
	void Start () {

		_MaxDistance = 1.0f;

		// Creating ressources
		_Ressources[0] = new Ressource("Food");

		_RessourceUsedPerTick [0] = this.GetComponent<PopulationGrowth> ()._Population;
		//_RessourceUsedPerTick [1] = 0;
		//_RessourceUsedPerTick [2] = 0;
		//_RessourceUsedPerTick [3] = 0;
	}
	
	// Update is called once per frame
	void Update () {

		// updating ressources amount every TIME_BETWEEN_TICKS seconds

		if (_timerTicks <= 0) {
			for (int i = 0; i < _nbRessources; i++) {
			_Ressources [i].UseAmount(_RessourceUsedPerTick [i] - _Ressources [i]._Production);
			}
			_timerTicks = TIME_BETWEEN_TICKS;
		} else {
			_timerTicks -= 1.0f * Time.deltaTime;
		}		
	}
}
