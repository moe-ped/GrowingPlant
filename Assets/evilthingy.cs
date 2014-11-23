using UnityEngine;
using System.Collections;

public class evilthingy : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		//GrowPlant.destroyBranch(other.gameObject);
		if(other.transform.parent)Destroy(other.transform.parent.gameObject);
	}
}
