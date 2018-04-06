using UnityEngine;
using System.Collections;

/*
 * Source code for the movements of the main camera
 */
public class CameraMovement : MonoBehaviour {

	private float edgeDistance = 50.0f;		// Maximum distance from any edge of the screen to make the camera moving.
	private float speed = 16.0f;			// Static speed of the camera movements.

	public GameObject CameraPivot;			// The main camera object, linked to the player object.

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Confined;	
	}
	
	// Update is called once per frame
	void Update () {
		
		/* Moving the camera on X && Y axies when the cursor reaches an edge of the screen */
		if (Input.mousePosition.x > Screen.width - edgeDistance){
			transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime,
				transform.position.y,
				transform.position.z);
		}

		if (Input.mousePosition.x < 0 + edgeDistance){
			transform.position = new Vector3 (transform.position.x - speed * Time.deltaTime,
				transform.position.y,
				transform.position.z);
		}

		if (Input.mousePosition.y > Screen.height - edgeDistance){
			transform.position = new Vector3 (transform.position.x,
				transform.position.y + speed * Time.deltaTime,
				transform.position.z);
		}

		if (Input.mousePosition.y < 0 + edgeDistance){
			transform.position = new Vector3 (transform.position.x,
				transform.position.y - speed * Time.deltaTime,
				transform.position.z);
		}
		//--------------------------------------------------------------------------------//

		/* Zooming/Dezooming with UpArrow && DownArrow keys */
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			CameraPivot.GetComponent<Camera> ().orthographicSize -= 4.0f*speed*Time.deltaTime;
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			CameraPivot.GetComponent<Camera> ().orthographicSize += 4.0f*speed*Time.deltaTime;
		}
		//-------------------------------------------------//	
	}
}
