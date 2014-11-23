using UnityEngine;
using System.Collections;

public class evilthingy : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		GrowPlant growPlant = FindObjectOfType<GrowPlant>();
		growPlant.destroyBranch(other.gameObject);
		//if(other.transform.parent)Destroy(other.transform.parent.gameObject);
	}
}
