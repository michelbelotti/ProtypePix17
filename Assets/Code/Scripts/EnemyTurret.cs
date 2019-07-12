using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EnemyBase {

    public FloatReference atkSpeed;
    public FloatReference speed;

    public GameObject projectile;
    public Transform cannonTransform;

    private float time;

    public override void Update()
    {
        base.Update();

        transform.LookAt(target);

        time += Time.deltaTime;
        if (time >= atkSpeed)
        {
            Shoot();
            time = 0;
        }
    }

    void Shoot()
    {
        //GameObject laserObj = Instantiate(projectile, fireTransform);
        GameObject laserObj = Instantiate(projectile, cannonTransform.position, cannonTransform.rotation);

        Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
        laserRB.velocity = cannonTransform.forward * speed;


    }
}
