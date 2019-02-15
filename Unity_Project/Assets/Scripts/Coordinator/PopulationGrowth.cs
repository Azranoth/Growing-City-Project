using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationGrowth : MonoBehaviour {

	public static float GROWTH_DELAY = 5.0f;
	public static float PPL_POP_DELAY = 0.3f;
	public static float DENOM_POP_GROWTH = 6.0f;

	public int _Population = 10;
	public int _dayCount = 1;

	public float _GrowthTimer = GROWTH_DELAY;
	public float _PplPopTimer = PPL_POP_DELAY;

	public GameObject _pplModel;
	public Vector3 _spawnPoint;
	public int _nbModels;
	// Use this for initialization
	void Start () {

		_spawnPoint = new Vector3 (this.GetComponent<CityEvolution> ()._cityModel.transform.localPosition.x, -0.5f, 0.0f);
		_nbModels = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (_GrowthTimer > 0) {
			_GrowthTimer -= 1.0f * Time.deltaTime;

			if (_PplPopTimer > 0) {
				_PplPopTimer -= 1.0f * Time.deltaTime;
			} else {
				_PplPopTimer = PPL_POP_DELAY + Random.Range (-0.3f, 0.2f);
				if (_nbModels > 0) {
					GameObject model = (GameObject)Instantiate (_pplModel, _spawnPoint, Quaternion.identity);
					model.GetComponent<PplModel> ()._distToCity = this.GetComponent<CityEvolution> ()._cityModel.transform.localPosition.z;
					_nbModels--;
				}
			}
				
		} else {

			// If every single citizen died, game is lost
			if (_Population <= 0) {
				this.GameOver ();
			}
			_Population += (1 + (int)(_Population / DENOM_POP_GROWTH));
			this.GetComponent<RessourcesManagement>()._RessourceUsedPerTick [0] = _Population;

			if (this.GetComponent<CityEvolution> ()._levelATM <= CityEvolution._nbCityLevels
				&& _Population >= this.GetComponent<CityEvolution> ()._RequiredPopulationToEvolve [this.GetComponent<CityEvolution> ()._levelATM-1]) {

				this.GetComponent<CityEvolution> ().EvolveCity ();
			}

			_nbModels = (1 + (int)(_Population / DENOM_POP_GROWTH));
			_dayCount++;
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
