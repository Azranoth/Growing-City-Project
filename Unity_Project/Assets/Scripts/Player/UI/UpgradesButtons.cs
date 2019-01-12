using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Upgrade {

	public string _title;
	public string _desc;
	public int _price = -1;
	public Sprite _icon;

	public Upgrade (string t, string d, int p, Sprite i){
		_title = t;
		_desc = d;
		_price = p;
		_icon = i;
	}

}
	
	
public class UpgradesButtons : MonoBehaviour {

	public static int _nbUpgrades = 2;
	public GameObject _buttonPrefab;
	public Transform scrollListContent;

	public Sprite[] _icons_data = new Sprite[_nbUpgrades];

	public List<Upgrade> _upgradesList = new List<Upgrade> ();


	// Use this for initialization
	void Start () {


		string tmpTitle, tmpDesc;
		int tmpPrice;

		//---Road upgrade's datas
		tmpTitle = "Roads";
		tmpDesc  = "Better roads allow to reach further destinations. Buildings can be built on tiles that were unreachable until then, and production of already built buildings is slightly increased.";
		tmpPrice = 10;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [0]));
		//----//

		//---Efficiency upgrade's datas
		tmpTitle = "Efficiency";
		tmpDesc  = "Better things.";
		tmpPrice = 10;
		_upgradesList.Add (new Upgrade (tmpTitle, tmpDesc, tmpPrice, _icons_data [0]));
		//----//




		AddButtons ();
	}


	private void AddButtons(){
		
		for (int i = 0; i < _nbUpgrades; ++i) {

			Upgrade upgrade = _upgradesList[i];
			GameObject newButton = Instantiate (_buttonPrefab);
			newButton.transform.SetParent(scrollListContent, false);

			PrefabUpgradeButton sampleButton = newButton.GetComponent<PrefabUpgradeButton>();
			sampleButton.Setup(upgrade, this.gameObject);

		}

	}


}
