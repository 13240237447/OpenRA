using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawnEffect : MonoBehaviour {

	public GameObject collisionEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnCollisionEnter(Collision collision) {

		GameObject p = Object.Instantiate(collisionEffect, transform.position, transform.rotation) as GameObject;
		p.transform.parent = GlobalVars.trash;
		p.SetActive(true);
		Destroy(p, 5);

	}
	
}
