using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basics : MonoBehaviour {

    public float health;
    public bool despawn = true;
    public float despawnDistance = 30;

    private float currentHealth;
    private bool dead;

    [HideInInspector]
    public Transform target;

    private void Awake()
    {
        //target = GameObject.Find("Player").GetComponent<Transform>(); ;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start () {

    }

    void OnEnable()
    {
        currentHealth = health;
        dead = false;
    }

    public virtual void Update () {

        if (despawn)
        {
            if ((transform.position.z - target.position.z) <= -despawnDistance)
            {
                Destroy(gameObject);
            }
        }
	}

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f && !dead)
        {
            dead = true;
            Destroy(gameObject);
        }
    }
}
