using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles_Basics : MonoBehaviour {

    void Start () {
		
	}
	
	void Update () {
		
	}

    /*
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            Enemy_Basics targetHealth = other.GetComponent<Enemy_Basics>();

            if (targetHealth)
            {
                targetHealth.TakeDamage(1);
            }
                
        }
        else if (other.tag == "Player")
        {
            Player_Status targetHealth = other.GetComponent<Player_Status>();

            if (targetHealth)
            {
                targetHealth.TakeDamage(1);
            }

        }

        Destroy(gameObject);
    }
    */
}
