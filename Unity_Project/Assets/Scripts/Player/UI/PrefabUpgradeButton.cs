using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabUpgradeButton : MonoBehaviour {

	public Text _upTitle;
	public Text _upDesc;
	public Text _upPrice;
	public Text _upCurrentLevel;
	public Image _upIcon;
	public GameObject _coord;
	public int _CurrentLevelInt = 0;




	public void Setup(Upgrade up, GameObject coord){

		_upTitle.text = up._title;
		_upDesc.text = up._desc;
		_upPrice.text = up._price.ToString();
		_upCurrentLevel.text = "Lvl. " + _CurrentLevelInt.ToString ();
		_upIcon.sprite = up._icon;
		_coord = coord; 

		this.GetComponent<Button> ().onClick.AddListener (HandleClick);
	}

	public void HandleClick(){
		Debug.Log ("clicked " + _upTitle.text);
		if (_coord != null) {
			if (_coord.GetComponent<Upgrades> ()._roadsMaxLevel > _CurrentLevelInt) {

				int tmpPrice = int.Parse (_upPrice.text);
				if (_coord.GetComponent<RessourcesManagement> ()._Ressources [2]._Amount >= tmpPrice) {

					_coord.SendMessage (_upTitle.text + "Upgrade", tmpPrice );

					_CurrentLevelInt++;
					int newPrice = tmpPrice + (tmpPrice * _CurrentLevelInt / 3);

					_upPrice.text = newPrice.ToString();
					_upCurrentLevel.text = "Lvl. " + _CurrentLevelInt.ToString ();
				}
			}
		}
	}
		
}
