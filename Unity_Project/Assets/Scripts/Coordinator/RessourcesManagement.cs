using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManagement : MonoBehaviour {

	public class Ressource
	{

		public string _Name;
		public int _Amount = 0;
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
		}
	}
	public static int _nbRessources = 1;
	public Ressource[] _Ressources = new Ressource[_nbRessources];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
