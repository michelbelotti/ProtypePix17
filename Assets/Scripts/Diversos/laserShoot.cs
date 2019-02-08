using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserShoot : MonoBehaviour {

    public float maxTimeLife;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, maxTimeLife);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
