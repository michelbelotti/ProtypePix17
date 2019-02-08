using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region_Checkpoint : MonoBehaviour {

    public GameObject gameManager;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Game_Manager gmScript = gameManager.GetComponent<Game_Manager>();
            gmScript.RegionCheckout();
        }
    }
}
