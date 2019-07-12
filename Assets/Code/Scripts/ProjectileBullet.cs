using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : ProjectileBase {

    public FloatReference speed;

    private void OnEnable()
    {
        Rigidbody bulletRB = gameObject.GetComponent<Rigidbody>();
        bulletRB.velocity = bulletRB.transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
