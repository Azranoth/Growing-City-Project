using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationGrowth : MonoBehaviour {

	public static float GROWTH_DELAY = 3.0f;

	public int _Population = 5;

	public float _GrowthTimer = GROWTH_DELAY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_GrowthTimer > 0) {
			_GrowthTimer -= 1.0f * Time.deltaTime;
		} else {
			
			_Population += (1 + (int)(_Population / 10.0f));
			this.GetComponent<RessourcesManagement>()._RessourceUsedPerTick [0] = _Population;

			if (_Population >= this.GetComponent<CityEvolution> ()._RequiredPopulationToEvolve [this.GetComponent<CityEvolution> ()._levelATM-1]) {
				this.GetComponent<CityEvolution> ().EvolveCity ();
			}
				
			_GrowthTimer = GROWTH_DELAY;
		}
	}
}
