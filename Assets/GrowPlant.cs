using UnityEngine;
using System.Collections;

public class GrowPlant : MonoBehaviour {

	public float growSpeed = 0.5f;

	public float angleRandomness = 10;

	public ArrayList sprouts = new ArrayList();
	public ArrayList leafSprouts = new ArrayList();

	public GameObject branch;
	public GameObject leaf;

	// Use this for initialization
	void Start () {
		GameObject newBranch = (GameObject)Instantiate (branch, transform.position, Quaternion.identity);
		newBranch.transform.parent = transform;
		doSprouting(newBranch);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject sprout in sprouts) {
			doGrowing (sprout);
		}
		foreach (GameObject leaf in leafSprouts) {
			growLeaves(leaf);
		}
	}

	GameObject doSprouting (GameObject sprout) {
		GameObject newBranch = (GameObject)Instantiate (branch, sprout.transform.position, Quaternion.identity);
		newBranch.transform.parent = sprout.transform;
		newBranch.transform.position += sprout.transform.up * sprout.GetComponent<SpriteRenderer>().bounds.size.y;
		newBranch.transform.localScale = Vector3.zero;
		// rotate by random angle
		newBranch.transform.Rotate(0, 0, ((Random.value-0.5f)*2)*angleRandomness);
		sprouts.Add(newBranch);
		growLeaf(sprout);
		return newBranch;
	}

	void doGrowing (GameObject sprout) {
		if (sprout.transform.localScale.y < 1) {
			sprout.transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
		}
		if (sprout.transform.localScale.y > 1) {
			sprout.transform.localScale = Vector3.one;
			//randomly sprout/ branch
			if (Random.value < 0.8f || sprouts.Count > 8) {
				doSprouting(sprout);
			}
			else {
				doBranching(sprout);
			}
			sprouts.Remove(sprout);
		}
	}

	// 2x dosprouting
	void doBranching (GameObject sprout) {
		GameObject branch1 = doSprouting (sprout);
		GameObject branch2 = doSprouting (sprout);
		branch1.transform.Rotate(0, 0, 30);
		branch2.transform.Rotate(0, 0, -30);
	}

	void growLeaf (GameObject sprout) {
		GameObject newLeaf = (GameObject)Instantiate (leaf, sprout.transform.position, Quaternion.identity);
		newLeaf.transform.parent = sprout.transform;
		newLeaf.transform.position += sprout.transform.up * sprout.GetComponent<SpriteRenderer>().bounds.size.y;
		newLeaf.transform.localScale = Vector3.zero;
		// rotate by random angle
		newLeaf.transform.Rotate(0, 0, Mathf.Sign(((Random.value-0.5f)*2))*35);
		newLeaf.transform.Rotate(0, 0, ((Random.value-0.5f)*2)*angleRandomness);
		leafSprouts.Add(newLeaf);
	}

	void growLeaves (GameObject leaf) {
		if (leaf.transform.localScale.y < 1) {
			leaf.transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
		}
		if (leaf.transform.localScale.y > 1) {
			leaf.transform.localScale = Vector3.one;
			leafSprouts.Remove(leaf);
		}
	}

	//whaaa!
	/*static public void destroyBranch (GameObject leaf) {
		foreach (Transform child in leaf.transform.parent.GetComponentsInChildren<Transform>()) {
			if (child.name.Contains("branch")) {
				this.sprouts.Remove(child);
			}
			if (child.name.Contains("leaf")) {
				this.leafSprouts.Remove(child);
			}
		}
		Destroy (leaf.parent.gameObject);
	}*/
}
