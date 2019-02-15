using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour
{
  
	//RessourcesManagement _rscManagementScript;		// Reference
	//Buildings _buildingsScript;						// Reference
	public GameObject _TradingPostsParent;
	public GameObject _FarmsParent;

	/*
	 * RoadsUpgrade()
	 * brief : boost city's max reacheable distances -> near buildings prod slightly upgraded && buildings buildable on further tiles
	 */
	public void RoadsUpgrade(int price){

		Debug.Log ("roads upgrade method called");
		this.GetComponent<RessourcesManagement> ()._Ressources [2].UseAmount (price);
		this.GetComponent<RessourcesManagement> ()._MaxDistance += 0.2f;

		// CURRENT PROD UPDATE
		GameObject[] existingBuildlings = GameObject.FindGameObjectsWithTag("Building");

		foreach (GameObject B in existingBuildlings) {

			B.SendMessage ("updateProductionRoads");
		}

	}

	/* 
	 * EfficiencyUpgrade()
	 * brief : Boost buildings' base production
	 */
	public void EfficiencyUpgrade(int price){

		Debug.Log ("efficiency upgrade method called");

		//Pay the upgrade
		this.GetComponent<RessourcesManagement> ()._Ressources [2].UseAmount (price);

		//Current production update
		GameObject[] existingBuildlings = GameObject.FindGameObjectsWithTag("Building");

		foreach (GameObject B in existingBuildlings) {

			B.SendMessage ("updateProductionEfficiency");
		}

		//Farm building base production
		if (this.GetComponent<Buildings> ()._Buildings [0] == null) {
			Debug.Log ("Upgrades - Efficiency upgrade error (missing prefab)");
			Application.Quit();

		}
		this.GetComponent<Buildings> ()._Buildings [0].GetComponent<Farm> ()._production = (int)(this.GetComponent<Buildings> ()._Buildings [0].GetComponent<Farm> ()._production * 1.2f);

		//Woodcutter building base production
		if (this.GetComponent<Buildings> ()._Buildings [1] == null) {
			Debug.Log ("Upgrades - Efficiency upgrade error (missing prefab)");
			Application.Quit();

		}
		this.GetComponent<Buildings> ()._Buildings [1].GetComponent<WoodcutterCamp> ()._production = (int)(this.GetComponent<Buildings> ()._Buildings [1].GetComponent<WoodcutterCamp> ()._production * 1.2f);

		//Goldmine building base production
		if (this.GetComponent<Buildings> ()._Buildings [2] == null) {
			Debug.Log ("Upgrades - Efficiency upgrade error (missing prefab)");
			Application.Quit();

		}
		this.GetComponent<Buildings> ()._Buildings [2].GetComponent<GoldMine> ()._production = (int)(this.GetComponent<Buildings> ()._Buildings [2].GetComponent<GoldMine> ()._production * 1.2f);
	}


	/*
	 * TaxesUpgrade()
	 * brief : Reduce trading taxes
	 */
	public void TaxesUpgrade(int price){

		Debug.Log ("taxes upgrade method called");

		//Pay the upgrade
		this.GetComponent<RessourcesManagement> ()._Ressources [2].UseAmount (price);

		this.GetComponent<RessourcesManagement> ()._tradingTax -= 0.1f;
	}

	/*
	 * CaravansUpgrade()
	 * brief : Increase trading posts trading capacity
	 */
	public void CaravansUpgrade(int price){
		Debug.Log ("Caravans upgrade method called");

		for (int i = 0; i < _TradingPostsParent.transform.childCount; ++i) {
			if (!_TradingPostsParent.transform.GetChild (i).GetComponent<TradingPost> ()) {
				Debug.Log ("Upgrades - Caravans upgrade error (missing script on object)");
				Application.Quit ();
			}
			_TradingPostsParent.transform.GetChild (i).GetComponent<TradingPost> ().updateTradingCapacity ();
		}

		if (this.GetComponent<Buildings> ()._Buildings [3] == null) {
			Debug.Log ("Upgrades - Caravans upgrade error (missing prefab)");
			Application.Quit();

		}
		this.GetComponent<Buildings> ()._Buildings [3].GetComponent<TradingPost> ()._tradingCapacity = (int)(this.GetComponent<Buildings> ()._Buildings [3].GetComponent<TradingPost> ()._tradingCapacity * 1.2f);
	}

	/*
	 * FarmUpgrade()
	 * brief : Increase farms productivity
	 */
	public void FarmUpgrade(int price){
		Debug.Log ("Farm upgrade method called");

		for (int i = 0; i < _FarmsParent.transform.childCount; ++i) {
			if (!_FarmsParent.transform.GetChild (i).GetComponent<Farm> ()) {
				Debug.Log ("Upgrades - Farm upgrade error (missing script on object)");
				Application.Quit ();
			}
			_FarmsParent.transform.GetChild (i).GetComponent<Farm> ().updateFarmProductivity ();
		}

		if (this.GetComponent<Buildings> ()._Buildings [0] == null) {
			Debug.Log ("Upgrades - Farm upgrade error (missing prefab)");
			Application.Quit ();

		}
		this.GetComponent<Buildings> ()._Buildings [0].GetComponent<Farm> ()._production = (int)(this.GetComponent<Buildings> ()._Buildings [0].GetComponent<Farm> ()._production * 1.4f);
	}		
}

