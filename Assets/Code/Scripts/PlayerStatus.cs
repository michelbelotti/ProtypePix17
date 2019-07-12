using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public FloatVariable currentHealth;
    public FloatReference maxHealth;

    void Start()
    {


    }

    private void OnEnable()
    {
        currentHealth.SetValue(maxHealth.Value);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ProjectileBase projectile = other.gameObject.GetComponent<ProjectileBase>();
        currentHealth.ApplyChange(-projectile.DamageAmount);

        if (currentHealth.Value <= 0)
        {
            Debug.Log("The PLayer is Dead!");
            Destroy(gameObject);
        }
    }
}