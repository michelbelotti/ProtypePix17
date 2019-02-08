using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour {

    public float Health;

    private float currentHealth;

	void Start () {
        

    }

    private void OnEnable()
    {
        currentHealth = Health;
    }

    void Update () {
		
	}

    public void TakeDamage(float dmg)
    {
        currentHealth = currentHealth - dmg;
        if(currentHealth <= 0)
        {
            Debug.Log("The PLayer is Dead!");
        }
    }
}
