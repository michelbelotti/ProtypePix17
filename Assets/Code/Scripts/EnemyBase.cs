using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public bool despawn = true;
    public FloatReference despawnDistance;

    public FloatReference health;

    private float currentHealth;

    [HideInInspector]
    public Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void OnEnable()
    {
        currentHealth = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileBase projectile = other.gameObject.GetComponent<ProjectileBase>();
        currentHealth -= projectile.DamageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
    public virtual void Update()
    {
        Debug.Log("despawn: " + despawn);
        if (despawn)
        {
            if ((transform.position.z - target.position.z) <= -despawnDistance)
            {
                Destroy(gameObject);
            }
        }
        
    }
    

    /*
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f && !dead)
        {
            Destroy(gameObject);
        }
    }
    */
}
