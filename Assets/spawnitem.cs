using UnityEngine;
using System.Collections;

public class spawnitem : MonoBehaviour {
	float rand;
	float time = 0f;
	float cooldown = 0.5f;
	public GameObject Spawn;
	public float hoehe;
	// Use this for initialization
	void Start () {
		}
	
	// Update is called once per frame
	void Update () {
	

		Vector3 vec = new Vector3 (transform.position.x, hoehe, transform.position.z);  




		time -= Time.deltaTime;
			if (time <= 0){
				time = cooldown;
				rand = Random.value;
					if (rand > 0.5) {
						Instantiate(Spawn, this.transform.position, Quaternion.identity) ;
			}
		}
			

	}
}
