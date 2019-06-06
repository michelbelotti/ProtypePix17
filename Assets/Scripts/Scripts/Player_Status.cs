using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour {

    public FloatVariable currentHealth;

    public FloatReference maxHealth;

	void Start () {
        

    }

    private void OnEnable()
    {
        currentHealth.SetValue(maxHealth.Value);
    }

    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Damage_Dealer damage = other.gameObject.GetComponent<Damage_Dealer>();

        currentHealth.SetValue(currentHealth.Value - damage.damageAmount);

        Destroy(other.gameObject);

        if (currentHealth.Value <= 0)
        {
            Debug.Log("The PLayer is Dead!");
        }
    }
}
