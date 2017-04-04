﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationGrowth : MonoBehaviour {

	public static float GROWTH_DELAY = 5.0f;

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
			_GrowthTimer = GROWTH_DELAY;
		}
	}
}
