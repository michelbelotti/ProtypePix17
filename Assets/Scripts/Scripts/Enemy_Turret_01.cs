using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret_01 : Enemy_Basics {

    public float delay = 1f;
    public GameObject projectile;
    public float velocity;
    public Transform fireTransform;

    //private Transform target;
    private float time;

    void Start () {

        //target = GameObject.Find("Player").GetComponent<Transform>(); ;
	}

    private void OnEnable()
    {
        time = 0;
    }

    public override void Update () {

        base.Update();

        transform.LookAt(target);

        time += Time.deltaTime;
        if(time >= delay)
        {
            Shoot();
            time = 0;
        }
    }

    void Shoot()
    {
        //GameObject laserObj = Instantiate(projectile, fireTransform);

        GameObject laserObj = Instantiate(projectile, fireTransform.position, fireTransform.rotation);

        Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
        laserRB.velocity = fireTransform.forward * velocity;

        
    }
}
