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
			Debug.Log ("Produce ressource "+ _Name + ", produces " + _Production + ", Used " + amount + ", have " + _Amount);
		}
	}
	/* ----- STATIC VARS -----*/
	public static int _nbRessources = 2;
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
		_Ressources[1] = new Ressource("Wood");

		_RessourceUsedPerTick [0] = this.GetComponent<PopulationGrowth> ()._Population;
		_RessourceUsedPerTick [1] = 0;
		//_RessourceUsedPerTick [2] = 0;
		//_RessourceUsedPerTick [3] = 0;
	}
	
	// Update is called once per frame
	void Update () {

		// updating ressources amount every TIME_BETWEEN_TICKS seconds

		if (_timerTicks <= 0) {
			for (int i = 0; i < _nbRessources; i++) {
				// If food is unsufficient
				if (i == 0) {
					if (_Ressources [0]._Amount < _RessourceUsedPerTick [0]) {
						// kill people accordingly
						this.GetComponent<PopulationGrowth> ()._Population -= (_RessourceUsedPerTick [0] - _Ressources [0]._Amount);
						_Ressources [0]._Amount = 0;
					} else {
						_Ressources [0].UseAmount (_RessourceUsedPerTick [0] - _Ressources [0]._Production);
					}
				}
				// if wood is unsifficent
					// Destroy buildings

			_Ressources [i].UseAmount(_RessourceUsedPerTick [i] - _Ressources [i]._Production);
			}
			_timerTicks = TIME_BETWEEN_TICKS;
		} else {
			_timerTicks -= 1.0f * Time.deltaTime;
		}		
	}
}
