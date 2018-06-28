using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationGrowth : MonoBehaviour {

	public static float GROWTH_DELAY = 3.0f;

	public int _Population = 10;

	public float _GrowthTimer = GROWTH_DELAY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_GrowthTimer > 0) {
			_GrowthTimer -= 1.0f * Time.deltaTime;
		} else {

			// If every single citizen died, game is lost
			if (_Population <= 0) {
				this.GameOver ();
			}
			_Population += (1 + (int)(_Population / 3.0f));
			this.GetComponent<RessourcesManagement>()._RessourceUsedPerTick [0] = _Population;

			if (this.GetComponent<CityEvolution> ()._levelATM <= CityEvolution._nbCityLevels
				&& _Population >= this.GetComponent<CityEvolution> ()._RequiredPopulationToEvolve [this.GetComponent<CityEvolution> ()._levelATM-1]) {
				this.GetComponent<CityEvolution> ().EvolveCity ();
			}
				
			_GrowthTimer = GROWTH_DELAY;
		}
	}

	/* function GameOver()
	 * Ultra basic gameover function -> stops the game. That's it.
	 */
	public void GameOver(){
		Debug.Log ("GAME OVER");
		Time.timeScale = 0.0f;
	}
}
