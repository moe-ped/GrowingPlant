using UnityEngine;
using System.Collections;

public class GrowPlant : MonoBehaviour {

	public float growSpeed = 0.5f;

	public float angleRandomness = 10;

	public int generationsToControl = 8;

	public int maximumBranches = 8;
	public float branchingChance = 0.2f;

	public ArrayList sprouts = new ArrayList();
	public ArrayList leafSprouts = new ArrayList();
	public ArrayList controlledBranches = new ArrayList();


	public GameObject branch;
	public GameObject leaf;

	// Use this for initialization
	void Start () {
		GameObject newBranch = (GameObject)Instantiate (branch, transform.position, Quaternion.identity);
		newBranch.transform.parent = transform;
		controlledBranches.Add(newBranch);
		doSprouting(newBranch);
		generationsToControl -= 1;
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
		// just to make sure ...
		sprouts.Remove(sprout.transform.parent.gameObject);

		Debug.Log(sprouts.Count);
		GameObject newBranch = (GameObject)Instantiate (branch, sprout.transform.position, Quaternion.identity);
		newBranch.transform.parent = sprout.transform;
		SpriteRenderer sproutSpriteRenderer = sprout.GetComponent<SpriteRenderer>();
		SpriteRenderer spriteRenderer = newBranch.GetComponent<SpriteRenderer>();
		newBranch.transform.position = sproutSpriteRenderer.bounds.center;
		newBranch.transform.position += sprout.transform.up * (sproutSpriteRenderer.bounds.extents.y);
		spriteRenderer.sortingOrder = sproutSpriteRenderer.sortingOrder - 1;
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
			sprouts.Remove(sprout);
			sprout.transform.localScale = Vector3.one;
			//randomly sprout/ branch
			if (Random.value < 1-branchingChance || sprouts.Count > maximumBranches) {
				doSprouting(sprout);
				if (generationsToControl > 0) {
					generationsToControl -= 1;
				}
				else {
					selectNextGen ();
				}
			}
			else {
				doBranching(sprout);
				if (generationsToControl > 0) {
					generationsToControl -= 1;
				}
				else {
					selectNextGen ();
				}
			}
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

	// update controlledBranches 
	void selectNextGen () {
		foreach (GameObject currentGen in controlledBranches) {
			controlledBranches.Remove(currentGen);
			foreach (Transform child in currentGen.transform) {
				controlledBranches.Add(child.gameObject);
			}
		}
	}


	public void destroyBranch (GameObject leaf) {
		foreach (Transform child in leaf.transform.parent.GetComponentsInChildren<Transform>()) {
			if (child.name.Contains("branch")) {
				sprouts.Remove(child.gameObject);
				controlledBranches.Remove(child.gameObject);
			}
			if (child.name.Contains("leaf")) {
				leafSprouts.Remove(child.gameObject);
			}
		}
		Destroy (leaf.transform.parent.gameObject);
	}
}
