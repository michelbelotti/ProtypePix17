using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    public GameObject[] enemyGroups;
    public int[] activationRegions;

    private int region;

    void Start () {
        
	}

    private void Awake()
    {
        for (int i = 0; i < enemyGroups.Length; i++)
        {
            enemyGroups[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        region = 0;

    }

    void Update () {
        
    }

    public void RegionCheckout()
    {
        region++;

        //Debug.Log("enemyGroups.Length " + enemyGroups.Length);
        //Debug.Log("activationRegions.Length " + activationRegions.Length);

        //Debug.Log("Region " + region);

        for (int i = 0; i < enemyGroups.Length; i++)
        {
            if(activationRegions[i] == region)
            {
                enemyGroups[i].SetActive(true);
            }
        }
    }
}
