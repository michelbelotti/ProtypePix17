using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    public FloatReference DamageAmount;

    public FloatReference maxTimeLife;

    void Start()
    {
        Destroy(gameObject, maxTimeLife);
    }

}
