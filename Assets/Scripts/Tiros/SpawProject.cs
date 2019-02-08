﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawProjectiles : MonoBehaviour {

	public GameObject FirePoint;	
	public List<GameObject> vfx = new List<GameObject> ();
	public RotateToMouse rotateToMouse;
	
	private GameObject effectToSpawn;
	private float timeToFire = 0;


	void Start () {
		effectToSpawn = vfx [0];
		
	}
	
	
	void Update () {
		if(Input.GetMouseButton (0) && Time.time >= timeToFire){
			timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
			SpawnVFX ();
		}
	}

	void SpawnVFX (){
		GameObject vfx;

		if (FirePoint != null) {
			vfx = Instantiate (effectToSpawn, FirePoint.transform.position, Quaternion.identity);
			if (rotateToMouse != null) {
				vfx.transform.localRotation = rotateToMouse.GetRotation ();
			}

		} else {
			Debug.Log ("No Fire Point");

		}
					
	}
}
