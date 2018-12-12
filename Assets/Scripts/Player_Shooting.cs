using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shooting : MonoBehaviour {

    public GameObject laserPrefab;
    public Transform fireTransform;
    public float velocity;
    public float atkSpeed;

    private bool fired;
    private float atkTime;

	// Use this for initialization
	void Start () {
        fired = false;
        atkTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        atkTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            fired = true;
            atkTime = 0;
            GameObject laserObj = Instantiate(laserPrefab,fireTransform.position, fireTransform.rotation);
            Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
            laserRB.velocity = fireTransform.forward * velocity;
        }

        if(atkTime > atkSpeed)
        {
            fired = false;
        }
	}

}
