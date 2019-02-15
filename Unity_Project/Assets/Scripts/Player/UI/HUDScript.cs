using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDScript : MonoBehaviour {

	// --- Ressources & Pop objects
	[Header("Ressources values")]
	public Text _foodCount;
	public Text _woodCount;
	public Text _goldCount;
	public Text _popCount;
	public Text _dayCount;
	[Space]

	[Header("Tick texts")]
	public Text _foodPerTick;
	public Text _woodPerTick;
	public Text _goldPerTick;
	[Space]

	[Header("Select highlight")]
	// --- Building selection highlight objects
	public GameObject _selectedFarm;
	public GameObject _selectedWoodcutter;
	public GameObject _selectedGoldmine;
	public GameObject _selectedTradingpost;
	[Space]

	[Header("Trading menu objects")]
	// --- Trading menu objects
	public GameObject _tradeButton;
	public GameObject _tradeMenu;
	private bool _tradeMenuDisplay = false;
	public Text _foodToWoodAmount;
	public Text _woodToFoodAmount;
	public Text _goldToFoodAmount;
	public Text _goldToWoodAmount;
	public int _valueToAddTrade = 20;
	public GameObject _buttonValue20;
	public GameObject _buttonValue50;
	public GameObject _buttonValue100;
	public GameObject _buttonValue500;
	private Color _baseButtonCol;
	private Color _currentButtonCol;
	[Space]

	[Header("Menus Buttons")]
	public GameObject _upgradeButton;
	public GameObject _upgradeMenu;
	private bool _upgradeMenuDisplay = false;
	[Space]

	public GameObject _coordinator;

	// Use this for initialization
	void Start () {
		// init HUD texts
		_foodCount.text = "25";
		_woodCount.text = "25";
		_goldCount.text = "25";
		_popCount.text  = "5" ;

		_foodPerTick.text = "0";
		_woodPerTick.text = "0";
		_goldPerTick.text = "0";

		_baseButtonCol = new Color (1.0f, 1.0f, 1.0f);
		_currentButtonCol = new Color (0.65f, 0.65f, 0.65f);

	}
	
	// Update is called once per frame
	void Update () {
		// init HUD texts
		_foodCount.text = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [0]._Amount.ToString();
		_woodCount.text = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Amount.ToString();
		_goldCount.text = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [2]._Amount.ToString();

		_foodToWoodAmount.text = _coordinator.GetComponent<RessourcesManagement> ()._foodToWood.ToString () + "(" + _coordinator.GetComponent<RessourcesManagement> ()._tradingCapacityPerTick + ")";
		_woodToFoodAmount.text = _coordinator.GetComponent<RessourcesManagement> ()._woodToFood.ToString () + "(" + _coordinator.GetComponent<RessourcesManagement> ()._tradingCapacityPerTick + ")";
		_goldToFoodAmount.text = _coordinator.GetComponent<RessourcesManagement> ()._goldToFood.ToString () + "(" + _coordinator.GetComponent<RessourcesManagement> ()._tradingCapacityPerTick + ")";
		_goldToWoodAmount.text = _coordinator.GetComponent<RessourcesManagement> ()._goldToWood.ToString () + "(" + _coordinator.GetComponent<RessourcesManagement> ()._tradingCapacityPerTick + ")";

		_popCount.text  = _coordinator.GetComponent<PopulationGrowth> ()._Population.ToString ();

		_dayCount.text = "Day " + _coordinator.GetComponent<PopulationGrowth> ()._dayCount.ToString ();


		updateBuildingSelection ();
		updateProdDisplay ();
	}

	/*
	 * function updateBuildingSelection()
	 * Highlight the current building's icon
	 */
	void updateBuildingSelection(){

		if (Input.GetKey (KeyCode.Alpha1)) {
			_selectedFarm.SetActive (true);
			_selectedWoodcutter.SetActive (false);
			_selectedGoldmine.SetActive (false);
			_selectedTradingpost.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			_selectedFarm.SetActive (false);
			_selectedWoodcutter.SetActive (true);
			_selectedGoldmine.SetActive (false);
			_selectedTradingpost.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Alpha3)) {
			_selectedFarm.SetActive (false);
			_selectedWoodcutter.SetActive (false);
			_selectedGoldmine.SetActive (true);
			_selectedTradingpost.SetActive (false);
		}

		if (Input.GetKey (KeyCode.Alpha4)) {
			_selectedFarm.SetActive (false);
			_selectedWoodcutter.SetActive (false);
			_selectedGoldmine.SetActive (false);
			_selectedTradingpost.SetActive (true);
		}
	}

	/*
	 * function updateProdDisplay()
	 * Update HUD's ressources production texts
	 */
	void updateProdDisplay(){

		int prodFood = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [0]._Production - _coordinator.GetComponent<RessourcesManagement> ()._RessourceUsedPerTick [0];
		int prodWood = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [1]._Production - _coordinator.GetComponent<RessourcesManagement> ()._RessourceUsedPerTick [1];
		int prodGold = _coordinator.GetComponent<RessourcesManagement> ()._Ressources [2]._Production - _coordinator.GetComponent<RessourcesManagement> ()._RessourceUsedPerTick [2];

		int tradeCap = _coordinator.GetComponent<RessourcesManagement> ()._tradingCapacityPerTick;
		float tradeTax = _coordinator.GetComponent<RessourcesManagement> ()._tradingTax;
		if (_coordinator.GetComponent<RessourcesManagement> ()._foodToWood > 0) {			
			prodFood -= (int)(tradeCap*tradeTax);
			prodWood += tradeCap;
		}
		if (_coordinator.GetComponent<RessourcesManagement> ()._woodToFood > 0) {
			prodFood += tradeCap;
			prodWood -= (int)(tradeCap*tradeTax);
		}
		if (_coordinator.GetComponent<RessourcesManagement> ()._goldToFood > 0) {
			prodFood += tradeCap;
			prodGold -= (int)(tradeCap*tradeTax);
		}

		if (_coordinator.GetComponent<RessourcesManagement> ()._goldToWood > 0) {
			prodWood += tradeCap;
			prodGold -= (int)(tradeCap*tradeTax);
		}



		// For each ressource : if the production is lower than the consumption, display it in red color
		if (prodFood < 0) {			
			_foodPerTick.color = new Color (1.0f, 0.0f, 0.0f);
			_foodPerTick.text = prodFood.ToString ();
		} else {
			if (_foodPerTick.color.b < 1.0f) // If the text is displayed in red
				_foodPerTick.color = new Color (1.0f, 1.0f, 1.0f);
			_foodPerTick.text = "+" + prodFood.ToString ();
		}

		if (prodWood < 0) {			
			_woodPerTick.color = new Color (1.0f, 0.0f, 0.0f);
			_woodPerTick.text = prodWood.ToString ();
		} else {
			if (_woodPerTick.color.b < 1.0f) // If the text is displayed in red
				_woodPerTick.color = new Color (1.0f, 1.0f, 1.0f);
			_woodPerTick.text = "+" + prodWood.ToString ();
		}

		if (prodGold < 0) {			
			_goldPerTick.color = new Color (1.0f, 0.0f, 0.0f);
			_goldPerTick.text = prodGold.ToString ();
		} else {
			if (_goldPerTick.color.b < 1.0f) // If the text is displayed in red
				_goldPerTick.color = new Color (1.0f, 1.0f, 1.0f);
			_goldPerTick.text = "+" + prodGold.ToString ();
		}

	}

	/*
	 * onTradeMenuButtonClicked()
	 * Display/hide the trade menu
	 */
	public void onTradeMenuButtonClicked(){
		if (_tradeMenuDisplay) {
			_tradeMenu.SetActive (false);
			_coordinator.GetComponent<Buildings> ()._outOfMenu = true;
			_tradeMenuDisplay = false;
			this.transform.parent.GetComponent<CameraMovement> ().menuLock (false);
		} else {
			_tradeMenu.SetActive (true);
			_coordinator.GetComponent<Buildings> ()._outOfMenu = false;
			_tradeMenuDisplay = true;
			this.transform.parent.GetComponent<CameraMovement> ().menuLock (true);
		}
	}

	// -------- TRADE MENU BUTTONS
	public void onFoodToWoodClicked(){

		_coordinator.GetComponent<RessourcesManagement> ()._foodToWood += _valueToAddTrade;
	}

	public void onWoodToFoodClicked(){

		_coordinator.GetComponent<RessourcesManagement> ()._woodToFood += _valueToAddTrade;
	}

	public void onGoldToFoodClicked(){

		_coordinator.GetComponent<RessourcesManagement> ()._goldToFood += _valueToAddTrade;
	}

	public void onGoldToWoodClicked(){

		_coordinator.GetComponent<RessourcesManagement> ()._goldToWood += _valueToAddTrade;
	}

	public void on20Clicked(){
		_valueToAddTrade = 20;
		_buttonValue20.GetComponent<Image>().color = _currentButtonCol;
		_buttonValue50.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue100.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue500.GetComponent<Image>().color = _baseButtonCol;
	}
	public void on50Clicked(){
		_valueToAddTrade = 50;
		_buttonValue20.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue50.GetComponent<Image>().color = _currentButtonCol;
		_buttonValue100.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue500.GetComponent<Image>().color = _baseButtonCol;
	}
	public void on100Clicked(){
		_valueToAddTrade = 100;
		_buttonValue20.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue50.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue100.GetComponent<Image>().color= _currentButtonCol;
		_buttonValue500.GetComponent<Image>().color = _baseButtonCol;
	}
	public void on500Clicked(){
		_valueToAddTrade = 500;
		_buttonValue20.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue50.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue100.GetComponent<Image>().color = _baseButtonCol;
		_buttonValue500.GetComponent<Image>().color = _currentButtonCol;
	}
	// --------

	// -------- UPGRADE BUTTON & MENU
	public void onUpgradeButtonClicked(){

		if (_upgradeMenuDisplay) {
			_upgradeMenu.SetActive (false);
			_upgradeButton.SetActive (true );
			_coordinator.GetComponent<Buildings> ()._outOfMenu = true;
			_upgradeMenuDisplay = false;
			this.transform.parent.GetComponent<CameraMovement> ().menuLock (false);
		} else {
			_upgradeMenu.SetActive (true);
			_upgradeButton.SetActive (false);
			_coordinator.GetComponent<Buildings> ()._outOfMenu = false;
			_upgradeMenuDisplay = true;
			this.transform.parent.GetComponent<CameraMovement> ().menuLock (true);
		}
	}


}
