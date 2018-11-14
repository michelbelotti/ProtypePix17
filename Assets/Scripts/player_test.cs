using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_test : MonoBehaviour {

    public float movementSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // transform.position += transform.forward * movementSpeed * Time.deltaTime;
        float distancy = 100f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * distancy);
        Debug.Log("world Poit : " + point);
        Debug.DrawLine(Camera.main.transform.position, point, Color.red);

        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, (vertical * -1f), 0f);

        Vector3 finalDirection = new Vector3(horizontal, (vertical * -1f), 1f);

        transform.position += direction * movementSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50f);
        */
    }
}
