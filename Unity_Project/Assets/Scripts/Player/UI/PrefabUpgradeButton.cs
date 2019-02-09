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
	public int _currentLevelInt = 0;
	public int _maxLevelInt;




	public void Setup(Upgrade up, GameObject coord){

		_upTitle.text = up._title;
		_upDesc.text = up._desc;
		_upPrice.text = up._price.ToString();
		_upCurrentLevel.text = "Lvl. " + _currentLevelInt.ToString ();
		_upIcon.sprite = up._icon;
		_maxLevelInt = up._maxlevel;
		_coord = coord; 

		this.GetComponent<Button> ().onClick.AddListener (HandleClick);
	}

	public void HandleClick(){
		Debug.Log ("clicked " + _upTitle.text);
		if (_coord != null) {
			if (_maxLevelInt > _currentLevelInt) {

				int tmpPrice = int.Parse (_upPrice.text);
				if (_coord.GetComponent<RessourcesManagement> ()._Ressources [2]._Amount >= tmpPrice) {

					_coord.SendMessage (_upTitle.text + "Upgrade", tmpPrice);

					_currentLevelInt++;
					int newPrice = tmpPrice + (tmpPrice * _currentLevelInt / 3);

					_upPrice.text = newPrice.ToString ();
					_upCurrentLevel.text = "Lvl. " + _currentLevelInt.ToString ();
				}
			} else {
				if (_maxLevelInt == _currentLevelInt) {
					_upPrice.text = "";
					_upCurrentLevel.text = "Lvl. MAX";
				}
			}
		}
	}
		
}
