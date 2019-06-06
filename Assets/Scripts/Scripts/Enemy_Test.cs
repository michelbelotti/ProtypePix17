using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test : Enemy_Basics {

	void Start () {
		
	}
	
	public override void Update () {
        base.Update();
	}

    public override void TakeDamage(float amount)
    {
        Debug.Log("Sou rebelde!");
    }

}
