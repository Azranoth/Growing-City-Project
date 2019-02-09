using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PplModel : MonoBehaviour {

	public  float _delay = 5.0f;
	protected float _timer = 0.0f;
	public float _distToCity = 0.0f;

	// Use this for initialization


	void Start () {
		this.transform.localScale = new Vector3 (Random.Range (0.08f, 0.1f), Random.Range (0.3f, 1.0f), Random.Range (0.08f, 0.1f));
		this.transform.localPosition = new Vector3 (this.transform.localPosition.x + Random.Range (-0.15f, 0.15f), this.transform.localPosition.y, this.transform.localPosition.z);

	}

	// Update is called once per frame
	void Update () {
		if (_timer < _delay) {
			_timer += 1.0f * Time.deltaTime;
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + (_distToCity / _delay)*Time.deltaTime);
		} else {
			Destroy (this.gameObject);
		}
	}
}
