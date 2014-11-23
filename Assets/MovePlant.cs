using UnityEngine;
using System.Collections;

public class MovePlant : MonoBehaviour {

	public float sensitivity = 1;

	public GrowPlant growPlant;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouseDirection = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
		moveTree(mouseDirection);
	}

	void moveTree (Vector3 direction) {
		foreach (GameObject controlledBranch in growPlant.controlledBranches) {
			//Debug.DrawLine(controlledBranch.transform.position, controlledBranch.transform.position + Vector3.right, Color.red);
			foreach (Transform branch in controlledBranch.GetComponentsInChildren<Transform>()) {
				//Debug.Log(branch.gameObject.name);
				float step = 0.1f * sensitivity * Time.deltaTime;
				branch.transform.up = Vector3.RotateTowards(branch.transform.up, direction, step, 0.0f); 
			}
		}
	}
}
