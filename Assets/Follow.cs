using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {


	public GrowPlant growplant;

	
	public ArrayList sprouts ;

	float hoehe = 0;
	float score = 0;
	float breite =0;



	

	
	
	// Use this for initialization
	void Start () {
		sprouts = growplant.sprouts;
	}
	void OnGUI () {
	GUI.Label(new Rect(10,10,100,30), "Score "+score);
	}
	
	// Update is called once per frame
	void Update () {


		score = Mathf.Round(hoehe); 



		foreach (GameObject sprout in sprouts)




		if (sprout.transform.position.y > hoehe){


			hoehe = sprout.transform.position.y;

			//Debug.Log("Höhe " + hoehe  );
		}




		Vector3 vec = new Vector3 (transform.position.x, hoehe, transform.position.z);  
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position , vec, 0.1f);
		
		foreach (GameObject sprout in sprouts)
						if (sprout.transform.position.x > breite) {
	


		
	}
	
	}
	}

