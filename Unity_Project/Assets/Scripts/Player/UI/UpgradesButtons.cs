using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Upgrade {

	public string _title;
	public string _desc;
	public int _price = -1;
	public Sprite _icon;
	public int _maxlevel = -1;

	public Upgrade (string t, string d, int p, Sprite i, int l){
		_title = t;
		_desc = d;
		_price = p;
		_icon = i;
		_maxlevel = l;
	}

}
	
	
public class UpgradesButtons : MonoBehaviour {

	public static int _nbUpgrades = 5;
	public GameObject _buttonPrefab;
	public Transform scrollListContent;

	public Sprite[] _icons_data = new Sprite[_nbUpgrades];

	public List<Upgrade> _upgradesList = new List<Upgrade> ();


	// Use this for initialization
	void Start () {


		string tmpTitle, tmpDesc;
		int tmpPrice, tmpMaxLevel;

		//---Road upgrade's datas
		tmpTitle = "Roads";
		tmpDesc  = "Better roads allow to reach further destinations. Buildings can be built on tiles that were unreachable until then, and production of already built buildings is slightly increased.";
		tmpPrice = 10;
		tmpMaxLevel = 10;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [0], tmpMaxLevel));
		//----//

		//---Efficiency upgrade's datas
		tmpTitle = "Efficiency";
		tmpDesc  = "Increases farms, woodcutter camps and goldmines productivity.";
		tmpPrice = 20;
		tmpMaxLevel = 10;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [1], tmpMaxLevel));
		//----//

		//---Trading taxes upgrade
		tmpTitle = "Taxes";
		tmpDesc  = "Lower taxes on local trades.";
		tmpPrice = 30;
		tmpMaxLevel = 4;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [2], tmpMaxLevel));
		//----//

		//---Trading capacity upgrade
		tmpTitle = "Caravans";
		tmpDesc = "Merchants manage to trade and move more goods, enhancing the trading posts productivity.";
		tmpPrice = 30;
		tmpMaxLevel = 8;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [3], tmpMaxLevel));
		//----//

		//---farm's productivity upgrade
		tmpTitle = "Farm";
		tmpDesc = "Improve farming methods to increase farms productivity.";
		tmpPrice = 20;
		tmpMaxLevel = 5;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [4], tmpMaxLevel));
		//----//

		AddButtons ();
	}


	private void AddButtons(){

		if (_buttonPrefab == null) {
			Debug.Log (" UpgradesButtons - UI generation error (missing prefab)");
			Application.Quit ();
		}

		for (int i = 0; i < _nbUpgrades; ++i) {

			Upgrade upgrade = _upgradesList[i];
			GameObject newButton = Instantiate (_buttonPrefab);
			newButton.transform.SetParent(scrollListContent, false);

			PrefabUpgradeButton sampleButton = newButton.GetComponent<PrefabUpgradeButton>();
			sampleButton.Setup(upgrade, this.gameObject);

		}

	}


}
